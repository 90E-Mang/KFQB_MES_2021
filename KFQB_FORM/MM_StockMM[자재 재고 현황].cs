using DC00_assm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using DC_POPUP;

namespace KFQB_Form
{
    public partial class MM_StockMM : DC00_WinForm.BaseMDIChildForm
    {
        private UltraGridUtil _GridUtil = new UltraGridUtil();
        public MM_StockMM()
        {
            InitializeComponent();
        }

        private void MM_StockMM_Load(object sender, EventArgs e)
        {
            try
            {
                #region < 그리드 셋팅 >
                // 그리드 세팅을 위한 명령문
                _GridUtil.InitializeGrid(this.grid1, false, true, false, "", false);
                _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",   "공장",           true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",    "품목",           true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME",    "품명",           true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "MATLOTNO",    "LOTNO",          true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "WHCODE",      "창고",           true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "STOCKQTY",    "재고수량",       true, GridColDataType_emu.Double,       130, 200, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE",    "단위",           true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "CUSTCODE",    "거래처",         true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "CUSTNAME",    "거래처명",       true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "INDATE",      "입고일자",       true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKER",       "등록자",         true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",    "등록일시",       true, GridColDataType_emu.DateTime24,   130, 200, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.SetInitUltraGridBind(grid1);
                #endregion

                Common _Common = new Common();
                DataTable dtTemp = new DataTable();
                // 공장 정보를 데이터 테이블에 등록한다.
                dtTemp = _Common.Standard_CODE("PLANTCODE");
                Common.FillComboboxMaster(this.cboPlantCode, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName,dtTemp.Columns["CODE_NAME"].ColumnName,"ALL","");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", dtTemp, "CODE_ID", "CODE_NAME");

                // 창고
                dtTemp = _Common.Standard_CODE("WHCODE");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "WHCODE",     dtTemp, "CODE_ID", "CODE_NAME");

                // 품목 정보 콤보박스 세팅
                dtTemp = _Common.GET_ItemCodeFERT_Code("ROH");
                Common.FillComboboxMaster(this.cboItemCode, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");

                // 공장 코드 고정

                cboPlantCode.Value = LoginInfo.PlantCode;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public override void DoInquire()
        {
            // 조회 버튼 클릭
            DBHelper helper = new DBHelper();
            try
            {
                string sPlantCode   = Convert.ToString(cboPlantCode.Value);
                string sItemCode    = Convert.ToString(cboItemCode.Value);
                string sLotNo       = Convert.ToString(txtLotNo.Text);

                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("15MM_StockMM_S1", CommandType.StoredProcedure, 
                                 helper.CreateParameter("PLANTCODE", sPlantCode,  DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("ITEMCODE",  sItemCode,   DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("LOTNO",     sLotNo,      DbType.String, ParameterDirection.Input)); // 스토어 프로시저 이름을 만듦.

                if (dtTemp.Rows.Count == 0)
                {
                    _GridUtil.Grid_Clear(grid1); // 그리드의 내용을 초기화한다.
                    ShowDialog("조회할 데이터가 없습니다. ", DC00_WinForm.DialogForm.DialogType.OK);
                    return;
                }
                
                this.grid1.DataSource = dtTemp;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                helper.Close();
            }
        }

        private void ultraButton1_Click(object sender, EventArgs e)
        {
            // 자재 재고 조회 화면에서 선택된 행이 없을 경우 종료
            if (grid1.ActiveRow == null)
            {
                return;
            }

            DataRow drRow = ((DataTable)this.grid1.DataSource).NewRow(); // 테이블 조회한 형식만 가지고 있는 빈 행을 하나 만듦.
            drRow["ITEMCODE"] = Convert.ToString(this.grid1.ActiveRow.Cells["ITEMCODE"].Value); // ActiveRow가 지금 선택한 행을 나타냄.
            drRow["ITEMNAME"] = Convert.ToString(this.grid1.ActiveRow.Cells["ITEMNAME"].Value);
            drRow["STOCKQTY"] = Convert.ToString(this.grid1.ActiveRow.Cells["STOCKQTY"].Value);
            drRow["MATLOTNO"] = Convert.ToString(this.grid1.ActiveRow.Cells["MATLOTNO"].Value);
            drRow["UNITCODE"] = Convert.ToString(this.grid1.ActiveRow.Cells["UNITCODE"].Value);

            // 바코드 디자인 선언
            Report_LotBacode reportLotBacode = new Report_LotBacode();
            // 레포트 북 선언
            Telerik.Reporting.ReportBook reportBook = new Telerik.Reporting.ReportBook();
            // 바코드 디자인에 데이터 소스 바인딩
            reportLotBacode.DataSource = drRow;
            // 레포트 북에 바코드 디자인 추가
            reportBook.Reports.Add(reportLotBacode);
            // 레포트 북 미리보기 창에 표현
            ReportViewer ReportBarcodeViewer = new ReportViewer(reportBook);
            ReportBarcodeViewer.ShowDialog();
        }
    }
}
