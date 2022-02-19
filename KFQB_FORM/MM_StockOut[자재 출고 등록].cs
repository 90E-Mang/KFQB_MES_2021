using DC00_assm;
using System;
using System.Data;

namespace KFQB_Form
{
    public partial class MM_StockOut : DC00_WinForm.BaseMDIChildForm
    {
        private UltraGridUtil _GridUtil = new UltraGridUtil();
        public MM_StockOut()
        {
            InitializeComponent();
        }

        private void MM_StockOut_Load(object sender, EventArgs e)
        {
            try
            {
                #region < 그리드 셋팅 >
                // 그리드 세팅을 위한 명령문
                _GridUtil.InitializeGrid(this.grid1, false, true, false, "", false);
                _GridUtil.InitColumnUltraGrid(grid1, "CHK",         "출고등록", true, GridColDataType_emu.CheckBox, 100, 120, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",   "공장",     true, GridColDataType_emu.VarChar, 130, 200, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",    "품목",     true, GridColDataType_emu.VarChar, 130, 200, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME",    "품명",     true, GridColDataType_emu.VarChar, 130, 200, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "MATLOTNO",    "LOTNO",    true, GridColDataType_emu.VarChar, 130, 200, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "WHCODE",      "창고",     true, GridColDataType_emu.VarChar, 130, 200, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "STOCKQTY",    "재고수량", true, GridColDataType_emu.Double, 130, 200, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE",    "단위",     true, GridColDataType_emu.VarChar, 130, 200, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "CUSTCODE",    "거래처",   true, GridColDataType_emu.VarChar, 130, 200, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "CUSTNAME",    "거래처명", true, GridColDataType_emu.VarChar, 130, 200, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "INDATE",      "입고일자", true, GridColDataType_emu.VarChar, 130, 200, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKER",       "등록자",   true, GridColDataType_emu.VarChar, 130, 200, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",    "등록일시", true, GridColDataType_emu.DateTime24, 130, 200, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.SetInitUltraGridBind(grid1);
                #endregion

                Common _Common = new Common();
                DataTable dtTemp = new DataTable();
                // 공장 정보를 데이터 테이블에 등록한다.
                dtTemp = _Common.Standard_CODE("PLANTCODE");
                Common.FillComboboxMaster(this.cboPlantCode, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", dtTemp, "CODE_ID", "CODE_NAME");

                // 창고
                dtTemp = _Common.Standard_CODE("WHCODE");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "WHCODE", dtTemp, "CODE_ID", "CODE_NAME");

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
                string sPlantCode = Convert.ToString(cboPlantCode.Value);
                string sItemCode  = Convert.ToString(cboItemCode.Value);
                string sLotNo     = Convert.ToString(txtLotNo.Text);
                string sStartDate = string.Format("{0:yyyy-MM-dd}", dtStart.Value);
                string sEndDate   = string.Format("{0:yyyy-MM-dd}", dtEnd.Value);

                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("15MM_StockOut_S1", CommandType.StoredProcedure,
                                 helper.CreateParameter("PLANTCODE", sPlantCode, DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("ITEMCODE", sItemCode, DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("LOTNO", sLotNo, DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("STARTDATE", sStartDate, DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("ENDDATE", sEndDate, DbType.String, ParameterDirection.Input));

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
            DBHelper helper = new DBHelper("", true);
            try
            {
                if (ShowDialog("선택하신 원자재 LOT를 생산출고 등록 하시겠습니까?") == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                for (int i = 0; i < this.grid1.Rows.Count; i++)
                {
                    if (Convert.ToString(this.grid1.Rows[i].Cells["CHK"].Value) == "0")
                    {
                        continue;
                    }
                    helper.ExecuteNoneQuery("15MM_StockOut_I1", CommandType.StoredProcedure,
                                                     helper.CreateParameter("PLANTCODE", Convert.ToString(this.grid1.Rows[i].Cells["PLANTCODE"].Value),DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("LOTNO",     Convert.ToString(this.grid1.Rows[i].Cells["MATLOTNO"].Value), DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("ITEMCODE",  Convert.ToString(this.grid1.Rows[i].Cells["ITEMCODE"].Value), DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("STOCKQTY",  Convert.ToString(this.grid1.Rows[i].Cells["STOCKQTY"].Value). Replace(",", ""), DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("UNITCODE",  Convert.ToString(this.grid1.Rows[i].Cells["UNITCODE"].Value), DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("WORKERID",  LoginInfo.UserID,                                             DbType.String, ParameterDirection.Input)
                                                     );
                }
                helper.Commit();
                ShowDialog("등록이 완료되었습니다. ");
            }
            catch (Exception ex)
            {
                helper.Rollback();
                ShowDialog(ex.ToString());
            }
            finally
            {
                helper.Close();
            }


        }
    }
}
