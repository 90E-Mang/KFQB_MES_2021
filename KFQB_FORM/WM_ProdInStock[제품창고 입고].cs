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

namespace KFQB_Form
{
    public partial class WM_ProdInStock : DC00_WinForm.BaseMDIChildForm
    {
        private UltraGridUtil _GridUtil = new UltraGridUtil();
        public WM_ProdInStock()
        {
            InitializeComponent();
        }

        private void WM_ProdInStock_Load(object sender, EventArgs e)
        {
            try
            {
                #region < 그리드 셋팅 >
                // 그리드 세팅을 위한 명령문
                _GridUtil.InitializeGrid(this.grid1, false, true, false, "", false);
                _GridUtil.InitColumnUltraGrid(grid1, "CHK",       "선택",     true, GridColDataType_emu.CheckBox,    80, 200, Infragistics.Win.HAlign.Left,  true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE", "공장",     true, GridColDataType_emu.VarChar,    130, 200, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "LOTNO",     "LOTNO",    true, GridColDataType_emu.VarChar,    130, 200, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",  "품목",     true, GridColDataType_emu.VarChar,    130, 200, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME",  "품명",     true, GridColDataType_emu.VarChar,    130, 200, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ITEMTYPE",  "품목구분", true, GridColDataType_emu.VarChar,    130, 200, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "WHCODE",    "창고",     true, GridColDataType_emu.VarChar,    130, 200, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "STOCKQTY",  "LOT수량",  true, GridColDataType_emu.Double,     130, 200, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE",  "단위",     true, GridColDataType_emu.VarChar,    130, 200, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",  "등록일시", true, GridColDataType_emu.DateTime24, 130, 200, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKER",     "등록자",   true, GridColDataType_emu.VarChar,    130, 200, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.SetInitUltraGridBind(grid1);
                #endregion

                Common _Common = new Common();
                DataTable dtTemp = new DataTable();
                // 공장 정보를 데이터 테이블에 등록한다.
                dtTemp = _Common.Standard_CODE("PLANTCODE");
                Common.FillComboboxMaster(this.cboPlantCode, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName,dtTemp.Columns["CODE_NAME"].ColumnName,"ALL","");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", dtTemp, "CODE_ID", "CODE_NAME");

                dtTemp = _Common.Standard_CODE("UNITCODE");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "UNITCODE",   dtTemp, "CODE_ID", "CODE_NAME");

                // 창고
                dtTemp = _Common.Standard_CODE("WHCODE");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "WHCODE",     dtTemp, "CODE_ID", "CODE_NAME");

                

                // 품목 정보 콤보박스 세팅
                dtTemp = _Common.Get_ItemForCus(LoginInfo.PlantCode);
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
                string sStartDate   = string.Format("{0:yyyy-MM-dd}",dtStart.Value);
                string sEndDate     = string.Format("{0:yyyy-MM-dd}",dtEnd.Value);

                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("15WM_ProdInStock_S1", CommandType.StoredProcedure, 
                                 helper.CreateParameter("PLANTCODE", sPlantCode,  DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("ITEMCODE",  sItemCode,   DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("LOTNO",     sLotNo,      DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("STARTDATE", sStartDate,  DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("ENDDATE",   sEndDate,    DbType.String, ParameterDirection.Input)); // 스토어 프로시저 이름을 만듦.

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
        public override void DoSave()
        {
            // 그리드에 바뀐 데이터가 있는 행을 테이블에 등록
            DataTable dt = grid1.chkChange();
            if (dt == null) return;                     // 바뀐 데이터행이 없으면 저장 종료
            DBHelper helper = new DBHelper("", true);    // 트랜잭션 설정 및 DB Open

            try
            {
                //  저장 여부 메세지로 결정
                if (ShowDialog("데이터를 등록 하시겠습니까 ? ") == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                // 변경된 데이터가 담긴 행을 하나씩 DataRow 에 담는다.
                foreach (DataRow drrow in dt.Rows)
                {
                    switch (drrow.RowState)
                    {
                        case DataRowState.Deleted:
                            break;
                        case DataRowState.Added:
                            break;
                        case DataRowState.Modified:
                            if (Convert.ToString(drrow["PLANTCODE"]) == "0")
                            {
                                continue;
                            }
                            helper.ExecuteNoneQuery("00WM_ProdInStock_U1", CommandType.StoredProcedure
                                                    , helper.CreateParameter("PLANTCODE",   Convert.ToString(drrow["PLANTCODE"]), DbType.String, ParameterDirection.Input)
                                                    , helper.CreateParameter("LOTNO",       Convert.ToString(drrow["LOTNO"]),     DbType.String, ParameterDirection.Input)
                                                    , helper.CreateParameter("ITEMCODE",    Convert.ToString(drrow["ITEMCODE"]),  DbType.String, ParameterDirection.Input)
                                                    , helper.CreateParameter("INOUTQTY",    Convert.ToString(drrow["STOCKQTY"]),  DbType.String, ParameterDirection.Input)
                                                    , helper.CreateParameter("UNITCODE",    Convert.ToString(drrow["UNITCODE"]),  DbType.String, ParameterDirection.Input)
                                                    , helper.CreateParameter("MAKER",       LoginInfo.UserID, DbType.String,                     ParameterDirection.Input)
                                                    );
                            break;
                    }
                    if (helper.RSCODE != "S") break;
                }
                if (helper.RSCODE == "S")
                {
                    helper.Commit();
                    ShowDialog("데이터 저장에 성공하였습니다. ", DC00_WinForm.DialogForm.DialogType.OK);
                }
                else
                {
                    helper.Rollback();
                    ShowDialog("데이터 저장에 실패하였습니다. " + helper.RSMSG, DC00_WinForm.DialogForm.DialogType.OK);
                }
            }
            catch (Exception ex)
            {
                helper.Rollback();
                ShowDialog(ex.ToString());

            }
            finally
            {

            }
        }

    }
}
