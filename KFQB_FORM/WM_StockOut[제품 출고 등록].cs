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
    public partial class WM_StockOut : DC00_WinForm.BaseMDIChildForm
    {
        private UltraGridUtil _GridUtil = new UltraGridUtil();
        public WM_StockOut()
        {
            InitializeComponent();
        }

        private void WM_StockOut_Load(object sender, EventArgs e)
        {
            try
            {
                #region < 그리드 셋팅 >
                // 그리드 세팅을 위한 명령문
                _GridUtil.InitializeGrid(this.grid1, true, true, false, "", false);
                _GridUtil.InitColumnUltraGrid(grid1, "CHK",         "출고 등록",   true, GridColDataType_emu.CheckBox,    120, 120, Infragistics.Win.HAlign.Left,  true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",   "공장",        true, GridColDataType_emu.VarChar,     120, 120, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "SHIPNO",      "상차번호",    true, GridColDataType_emu.VarChar,     160, 120, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "SHIPDATE",    "상차일자",    true, GridColDataType_emu.VarChar,     140, 120, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "CARNO",       "차량번호",    true, GridColDataType_emu.VarChar,     140, 120, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "CUSTCODE",    "거래처",      true, GridColDataType_emu.VarChar,     120, 120, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "CUSTNAME",    "거래처명",    true, GridColDataType_emu.VarChar,     120, 120, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "WORKER",      "상차자",      true, GridColDataType_emu.VarChar,      100, 120, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "TRADINGNO",   "명세서번호",  true, GridColDataType_emu.VarChar,     100, 120, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "TRADINGDATE", "출고일자",    true, GridColDataType_emu.VarChar,     100, 120, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",    "등록일시",    true, GridColDataType_emu.DateTime24,  150, 120, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKER",       "등록자",      true, GridColDataType_emu.VarChar,     100, 120, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "EDITDATE",    "수정일시",    true, GridColDataType_emu.DateTime24,  150, 120, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "EDITOR",      "수정자",      true, GridColDataType_emu.VarChar,     100, 120, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.SetInitUltraGridBind(grid1);
                
                
                _GridUtil.InitializeGrid(this.grid2, true, true, false, "", false);
                _GridUtil.InitColumnUltraGrid(grid2, "PLANTCODE", "공장",        true, GridColDataType_emu.VarChar,     100, 120, Infragistics.Win.HAlign.Left,  false, false);
                _GridUtil.InitColumnUltraGrid(grid2, "SHIPNO",    "상차번호",    true, GridColDataType_emu.VarChar,     120, 120, Infragistics.Win.HAlign.Left,  false, false);
                _GridUtil.InitColumnUltraGrid(grid2, "SHIPSEQ",   "상차순번",    true, GridColDataType_emu.VarChar,     120, 120, Infragistics.Win.HAlign.Center,  true, false);
                _GridUtil.InitColumnUltraGrid(grid2, "LOTNO",     "LOTNO",       true, GridColDataType_emu.VarChar,     160, 120, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid2, "ITEMCODE",  "품목",        true, GridColDataType_emu.VarChar,     120, 120, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid2, "ITEMNAME",  "품목명",      true, GridColDataType_emu.VarChar,     150, 120, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid2, "SHIPQTY",   "상차수량",    true, GridColDataType_emu.Double,      100, 120, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid2, "UNITCODE",  "단위",        true, GridColDataType_emu.VarChar,     100, 120, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.SetInitUltraGridBind(grid2);
                #endregion

                Common _Common = new Common();
                DataTable dtTemp = new DataTable();
                // 공장 정보를 데이터 테이블에 등록한다.
                dtTemp = _Common.Standard_CODE("PLANTCODE");
                Common.FillComboboxMaster(this.cboPlantCode, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName,dtTemp.Columns["CODE_NAME"].ColumnName,"ALL","");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", dtTemp, "CODE_ID", "CODE_NAME");

                dtTemp = _Common.Standard_CODE("UNITCODE");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "UNITCODE",   dtTemp, "CODE_ID", "CODE_NAME");

                // 공장 코드 고정
                cboPlantCode.Value = LoginInfo.PlantCode;
                BizTextBoxManager btbManager = new BizTextBoxManager();
                btbManager.PopUpAdd(txtCustCode, txtCustName, "CUST_MASTER", new object[] { "1000","","",""});
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
                string sCustCode    = Convert.ToString(txtCustCode.Text);
                string sCarNo       = Convert.ToString(txtCarNo.Text);
                string sShipNO      = Convert.ToString(txtShipNo.Text);
                string sStartDate   = string.Format("{0:yyyy-MM-dd}",dtStart.Value);
                string sEndDate     = string.Format("{0:yyyy-MM-dd}",dtEnd.Value);

                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("15WM_StockOut_S1", CommandType.StoredProcedure, 
                                 helper.CreateParameter("PLANTCODE", sPlantCode,  DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("CUSTCODE",  sCustCode,   DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("CARNO",     sCarNo,      DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("SHIPNO",    sShipNO,      DbType.String, ParameterDirection.Input),
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
                string sCarNO = Convert.ToString(dt.Rows[0]["CARNO"]);
                int ChkCount = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["CHK"]) != "1")
                    {
                        continue;
                       
                    }
                    if (sCarNO != Convert.ToString(dt.Rows[i]["CARNO"]))
                    {
                        ShowDialog("동일한 차량번호를 선택해 주세요.");
                        return;
                    }
                    ChkCount += 1;
                }
                if (ChkCount == 0)
                {
                    ShowDialog("출고 내역을 선택해주세요.");
                    return;
                }

                if (ShowDialog("선택하신 내역을 출고 등록 하시겠습니까?") == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                string sTradingNo = string.Empty;
                foreach (DataRow drrow in dt.Rows)
                {
                    switch (drrow.RowState)
                    {
                        case DataRowState.Deleted:
                            break;
                        case DataRowState.Added:
                            break;
                        case DataRowState.Modified:
                            if (Convert.ToString(drrow["CHK"]) == "0")
                            {
                                continue;
                            }
                            helper.ExecuteNoneQuery("15WM_StockOut_U1", CommandType.StoredProcedure
                                                    , helper.CreateParameter("PLANTCODE",    Convert.ToString(drrow["PLANTCODE"]), DbType.String, ParameterDirection.Input)
                                                    , helper.CreateParameter("SHIPNO",       Convert.ToString(drrow["SHIPNO"]),    DbType.String, ParameterDirection.Input)
                                                    , helper.CreateParameter("TRADINGNO",    sTradingNo,                           DbType.String, ParameterDirection.Input)
                                                    , helper.CreateParameter("MAKER",        LoginInfo.UserID, DbType.String,                     ParameterDirection.Input)
                                                    );
                            if (helper.RSCODE == "S")
                            {
                                sTradingNo = helper.RSMSG;
                            }
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

        private void grid1_AfterRowActivate(object sender, EventArgs e)
        {
            //상차 공통에 대한 상세 실적 조회
            DBHelper helper = new DBHelper(false); 
            try
            {
                string sPlantCode   = Convert.ToString(this.grid1.ActiveRow.Cells["PLANTCODE"].Value);
                string sShipNo      = Convert.ToString(this.grid1.ActiveRow.Cells["SHIPNO"].Value);
                DataTable rtnDtTemp = helper.FillTable("15WM_StockOut_S2", CommandType.StoredProcedure
                                                             , helper.CreateParameter("PLANTCODE", sPlantCode, DbType.String, ParameterDirection.Input)
                                                             , helper.CreateParameter("SHIPNO",    sShipNo, DbType.String, ParameterDirection.Input)
                                                             );
                if (rtnDtTemp.Rows.Count > 0)
                {
                    grid2.DataSource = rtnDtTemp;
                    grid2.DataBinds(rtnDtTemp);
                }
                else
                {
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                helper.Close();
            }
        }
    }
}
