using DC00_assm;
using System;
using System.Data;
using DC_POPUP;

namespace KFQB_Form
{
    public partial class WM_StockShip : DC00_WinForm.BaseMDIChildForm
    {
        private UltraGridUtil _GridUtil = new UltraGridUtil();
        public WM_StockShip()
        {
            InitializeComponent();
        }

        private void WM_StockShip_Load(object sender, EventArgs e)
        {
            try
            {
                #region < 그리드 셋팅 >
                // 그리드 세팅을 위한 명령문
                _GridUtil.InitializeGrid(this.grid1, false, true, false, "", false);
                _GridUtil.InitColumnUltraGrid(grid1, "CHK",         "상차선택", true, GridColDataType_emu.CheckBox,   130, 200, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",   "공장",     true, GridColDataType_emu.VarChar,    130, 200, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "SHIPFLAG",    "상차여부", true, GridColDataType_emu.VarChar,    130, 200, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",    "품목",     true, GridColDataType_emu.VarChar,    130, 200, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME",    "품목명",   true, GridColDataType_emu.VarChar,    130, 200, Infragistics.Win.HAlign.Left, true, false);              
                _GridUtil.InitColumnUltraGrid(grid1, "LOTNO",       "LOTNO",    true, GridColDataType_emu.VarChar,    130, 200, Infragistics.Win.HAlign.Left, true, false);                
                _GridUtil.InitColumnUltraGrid(grid1, "WHCODE",      "입고창고", true, GridColDataType_emu.VarChar,    130, 200, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "STOCKQTY",    "재고수량", true, GridColDataType_emu.Double,     130, 200, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE",    "단위",     true, GridColDataType_emu.VarChar,    130, 200, Infragistics.Win.HAlign.Right, true, false);                
                _GridUtil.InitColumnUltraGrid(grid1, "INDATE",      "입고일자", true, GridColDataType_emu.VarChar,    130, 200, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",    "등록일시", true, GridColDataType_emu.DateTime24, 160, 200, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKER",       "등록자",   true, GridColDataType_emu.VarChar,    100, 200, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.SetInitUltraGridBind(grid1);
                #endregion

                Common _Common = new Common();
                DataTable dtTemp = new DataTable();
                // 공장 정보를 데이터 테이블에 등록한다.
                dtTemp = _Common.Standard_CODE("PLANTCODE");  // 사업장
                Common.FillComboboxMaster(this.cboPlantCode, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", dtTemp, "CODE_ID", "CODE_NAME");

                dtTemp = _Common.Standard_CODE("UNITCODE");     //단위
                UltraGridUtil.SetComboUltraGrid(this.grid1, "UNITCODE", dtTemp, "CODE_ID", "CODE_NAME");

                dtTemp = _Common.Standard_CODE("YESNO");     //상차 여부
                UltraGridUtil.SetComboUltraGrid(this.grid1, "SHIPFLAG", dtTemp, "CODE_ID", "CODE_NAME");

                dtTemp = _Common.Standard_CODE("WHCODE");     //입고 창고
                UltraGridUtil.SetComboUltraGrid(this.grid1, "WHCODE", dtTemp, "CODE_ID", "CODE_NAME");

                BizTextBoxManager btbManager = new BizTextBoxManager();
                btbManager.PopUpAdd(txtItemCode_H, txtItemName_H, "ITEM_MASTER",   new object[] { "1000", "" });
                btbManager.PopUpAdd(txtWorkerCode, txtWorkerName, "WORKER_MASTER", new object[] { "", "", "", "", "" });
                btbManager.PopUpAdd(txtCustCode,   txtCustName,   "CUST_MASTER",   new object[] { "", "", "", "" });
                 
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
                _GridUtil.Grid_Clear(grid1);
                string sPlantCode = Convert.ToString(cboPlantCode.Value);
                string sLotNo     = Convert.ToString(txtLotNo.Text);
                string sItemCode  = Convert.ToString(txtItemCode_H.Value);
                string sStartDate = string.Format("{0:yyyy-MM-dd}",dtStart.Value);
                string sEndDate   = string.Format("{0:yyyy-MM-dd}",dtEnd.Value);

                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("15WM_StockShip_S1", CommandType.StoredProcedure,
                                 helper.CreateParameter("PLANTCODE", sPlantCode, DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("ITEMCODE",  sItemCode,  DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("LOTNO",     sLotNo,     DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("STARTDATE", sStartDate, DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("ENDDATE",   sEndDate,   DbType.String, ParameterDirection.Input)
                                 );


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
                if (ShowDialog("선택하신 제품 LOT의 상차 등록을 진행 하시겠습니까?") == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
                else
                {

                    string SHIPNO = string.Empty;

                for (int i = 0; i < this.grid1.Rows.Count; i++)
                {
                    // Validation Check
                    if (Convert.ToString(txtCarNo.Text) == "")
                    {
                        ShowDialog("차량번호를 입력해주세요.");
                        return;
                    }
                    if (Convert.ToString(txtWorkerCode.Text) == "" || Convert.ToString(txtWorkerName.Text) == "")
                    {
                        ShowDialog("작업자를 정확히 입력해주세요.");
                        return;
                    }
                    if (Convert.ToString(txtCustCode.Text) == "" || Convert.ToString(txtCustName.Text) == "")
                    {
                        ShowDialog("거래처를 정확히 입력해주세요.");
                        return;
                    }
                    
                    // 체크가 된 항목만 프로시저 수행
                    if (Convert.ToString(this.grid1.Rows[i].Cells["CHK"].Value) == "1")
                    {
                            string sCarNo = Convert.ToString(txtCarNo.Value);
                            string sWorkerid = Convert.ToString(txtWorkerCode.Text);
                            string sCustCode = Convert.ToString(txtCustCode.Text);
                        
                        helper.ExecuteNoneQuery("15WM_StockShip_U1", CommandType.StoredProcedure,
                                                         helper.CreateParameter("PLANTCODE", Convert.ToString(this.grid1.Rows[i].Cells["PLANTCODE"].Value),                 DbType.String, ParameterDirection.Input),
                                                         helper.CreateParameter("CARNO",     sCarNo,                                                                        DbType.String, ParameterDirection.Input),
                                                         helper.CreateParameter("CUSTCODE",  sCustCode,                                                                     DbType.String, ParameterDirection.Input),
                                                         helper.CreateParameter("STOCKQTY",  Convert.ToString(this.grid1.Rows[i].Cells["STOCKQTY"].Value).Replace(",", ""), DbType.String, ParameterDirection.Input),
                                                         helper.CreateParameter("SHIPNO",    SHIPNO,                                                                        DbType.String, ParameterDirection.Input),
                                                         helper.CreateParameter("LOTNO",     Convert.ToString(this.grid1.Rows[i].Cells["LOTNO"].Value),                     DbType.String, ParameterDirection.Input),
                                                         helper.CreateParameter("ITEMCODE",  Convert.ToString(this.grid1.Rows[i].Cells["ITEMCODE"].Value),                  DbType.String, ParameterDirection.Input),
                                                         helper.CreateParameter("WORKERID",  sWorkerid,                                                                     DbType.String, ParameterDirection.Input),
                                                         helper.CreateParameter("MAKER",     LoginInfo.UserID,                                                              DbType.String, ParameterDirection.Input)
                                                         );

                    }
                    SHIPNO = helper.RSMSG;


                }
                helper.Commit();
                ShowDialog("상차 등록이 완료되었습니다. ");
                }
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
