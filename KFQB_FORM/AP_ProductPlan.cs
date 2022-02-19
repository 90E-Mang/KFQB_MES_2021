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
    public partial class AP_ProductPlan : DC00_WinForm.BaseMDIChildForm
    {
        private UltraGridUtil _GridUtil = new UltraGridUtil();
        public AP_ProductPlan()
        {
            InitializeComponent();
        }

        private void AP_ProductPlan_Load(object sender, EventArgs e)
        {
            try
            {
                #region < 그리드 셋팅 >
                // 그리드 세팅을 위한 명령문
                _GridUtil.InitializeGrid(this.grid1, false, true, false, "", false);
                _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",        "공장",             true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Left,  true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "PLANNO",           "계획번호",         true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",         "생산품목",         true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Left,  true, true);            
                _GridUtil.InitColumnUltraGrid(grid1, "PLANQTY",          "계획수량",         true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Left,  true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE",         "단위",             true, GridColDataType_emu.Double,       130, 200, Infragistics.Win.HAlign.Right, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERCODE",   "작업장",           true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Left,  true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "CHK",              "확정",             true, GridColDataType_emu.CheckBox,     130, 200, Infragistics.Win.HAlign.Left,  true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "ORDERNO",          "작업지시번호",     true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Left,  true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "ORDERDAATE",       "확정일시",         true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Right, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "ORDERWORKER",      "확정자",           true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Right, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "ORDERCLOSEFLAG",   "작업지시종료여부", true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Left,  true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "ORDERCLOSEDATE",   "작업종료일시",     true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Left,  true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKER",            "등록자",           true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",         "등록일시",         true, GridColDataType_emu.DateTime24,   130, 200, Infragistics.Win.HAlign.Left,  true, false);                
                _GridUtil.InitColumnUltraGrid(grid1, "EDITOR",           "수정자",           true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "EDITDATE",         "수정일시",         true, GridColDataType_emu.DateTime24,   130, 200, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.SetInitUltraGridBind(grid1);
                #endregion

                Common _Common = new Common();
                DataTable dtTemp = new DataTable();
                // 공장 정보를 데이터 테이블에 등록한다.
                dtTemp = _Common.Standard_CODE("PLANTCODE");
                Common.FillComboboxMaster(this.cboPlantCode, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName,dtTemp.Columns["CODE_NAME"].ColumnName,"ALL","");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", dtTemp, "CODE_ID", "CODE_NAME");

                // 작업장
                dtTemp = _Common.GET_Workcenter_Code();
                Common.FillComboboxMaster(this.cboWorkCenterCode, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "WORKCENTERCODE", dtTemp, "CODE_ID", "CODE_NAME");

                // 지시 종료 여부
                dtTemp = _Common.Standard_CODE("YESNO");
                Common.FillComboboxMaster(this.cboOrderCloseFlag, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "ORDERCLOSEFLAG", dtTemp, "CODE_ID", "CODE_NAME");

                // 단위
                dtTemp = _Common.Standard_CODE("UNITCODE");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "UNITCODE", dtTemp, "CODE_ID", "CODE_NAME");

                // 품목 
                dtTemp = _Common.GET_ItemCodeFERT_Code("FERT");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "ITEMCODE", dtTemp, "CODE_ID", "CODE_NAME");

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
                string sPlantCode       = Convert.ToString(cboPlantCode.Value);
                string sWorkCenterCode  = Convert.ToString(cboWorkCenterCode.Value);
                string sOrderNo         = Convert.ToString(txtOrderNo.Text);
                string sOrderCloseFlag  = Convert.ToString(cboOrderCloseFlag.Value);

                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("15AP_ProductPlan_S1", CommandType.StoredProcedure, // 스토어 프로시저 이름을 만듦.
                                 helper.CreateParameter("PLANTCODE",           sPlantCode,        DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("WORKCENTERCODE",      sWorkCenterCode,   DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("ORDERNO",             sOrderNo,          DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("ORDERCLOSEFLAG",      sOrderCloseFlag,   DbType.String, ParameterDirection.Input)); 


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

        public override void DoNew()
        {
            try
            {
                base.DoNew();
                this.grid1.InsertRow(); // 그리드에 신규 행 추가

                this.grid1.ActiveRow.Cells["PLANTCODE"].Value = LoginInfo.PlantCode;

                // 수정 불가 컬럼 설정
                grid1.ActiveRow.Cells["PLANNO"].Activation         = Activation.NoEdit;   // 계획번호
                grid1.ActiveRow.Cells["CHK"].Activation            = Activation.NoEdit;   // 확정
                grid1.ActiveRow.Cells["ORDERNO"].Activation        = Activation.NoEdit;   // 작업지시번호
                grid1.ActiveRow.Cells["ORDERDAATE"].Activation     = Activation.NoEdit;   // 확정일시
                grid1.ActiveRow.Cells["ORDERWORKER"].Activation    = Activation.NoEdit;   // 확정자
                grid1.ActiveRow.Cells["ORDERCLOSEFLAG"].Activation = Activation.NoEdit;   // 지시종료여부
                grid1.ActiveRow.Cells["ORDERCLOSEDATE"].Activation = Activation.NoEdit;   // 지시종료일자

                grid1.ActiveRow.Cells["MAKEDATE"].Activation       = Activation.NoEdit; // 등록일시
                grid1.ActiveRow.Cells["MAKER"].Activation          = Activation.NoEdit; // 등록자
                grid1.ActiveRow.Cells["EDITDATE"].Activation       = Activation.NoEdit; // 수정일시
                grid1.ActiveRow.Cells["EDITOR"].Activation         = Activation.NoEdit; // 수정자

            }
            catch (Exception ex)
            {

                
            }
        }

        public override void DoDelete()
        {
            // 삭제 버튼 클릭
            if (Convert.ToString(this.grid1.ActiveRow.Cells["CHK"].Value) ==  "1")
            {
                ShowDialog("확정된 작업 지시는 삭제 할 수 없습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            base.DoDelete();
            this.grid1.DeleteRow();
        }

        public override void DoSave()
        {
            // 저장 버튼 클릭
            DataTable dt = grid1.chkChange();
            if (dt == null) return;

            DBHelper helper = new DBHelper("",true);
            try
            {
                if (ShowDialog("등록 내역을 저장하시겠습니까?") == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
                foreach (DataRow drrow in dt.Rows)
                {
                    switch (drrow.RowState)
                    {
                        case DataRowState.Deleted:
                            drrow.RejectChanges();
                            helper.ExecuteNoneQuery("15AP_ProductPlan_D1", CommandType.StoredProcedure,
                                                     helper.CreateParameter("PLANTCODE",      Convert.ToString(drrow["PLANTCODE"]),            DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("WORKCENTERCODE", Convert.ToString(drrow["WORKCENTERCODE"]),       DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("PLANNO",         Convert.ToString(drrow["PLANNO"]),               DbType.String, ParameterDirection.Input));
                            break;
                        case DataRowState.Modified:
                            string sOrderFlag = string.Empty;
                            if(Convert.ToString(drrow["CHK"]).ToUpper() == "1")
                            {
                                sOrderFlag = "Y";
                            }
                            else
                            {
                                sOrderFlag = "N";
                            }
                            helper.ExecuteNoneQuery("15AP_ProductPlan_U1", CommandType.StoredProcedure,
                                                     helper.CreateParameter("PLANTCODE", Convert.ToString(drrow["PLANTCODE"]),   DbType.String, ParameterDirection.Input),  
                                                     helper.CreateParameter("PLANNO",    Convert.ToString(drrow["PLANNO"]),      DbType.String, ParameterDirection.Input),                                                    
                                                     helper.CreateParameter("ORDERFLAG", sOrderFlag,                             DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("EDITOR",    LoginInfo.UserID,                       DbType.String, ParameterDirection.Input));

                            break;
                        case DataRowState.Added:
                            string sMsg = string.Empty;
                            if (Convert.ToString(drrow["ITEMCODE"]) == "")
                            {
                                sMsg += "생산품목 ";
                            }
                            if (Convert.ToString(drrow["PLANQTY"]) == "")
                            {
                                sMsg += "계획 수량 ";
                            }
                            if (Convert.ToString(drrow["WORKCENTERCODE"]) == "")
                            {
                                sMsg += "작업장  ";
                            }
                            if (sMsg != "")
                            {
                                ShowDialog(sMsg + " 을(를) 입력하지 않았습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                                helper.Rollback();
                                return;
                            }

                            helper.ExecuteNoneQuery("15AP_ProductPlan_I1", CommandType.StoredProcedure,
                                                     helper.CreateParameter("PLANTCODE",       Convert.ToString(drrow["PLANTCODE"]),                   DbType.String, ParameterDirection.Input),  
                                                     helper.CreateParameter("ITEMCODE",        Convert.ToString(drrow["ITEMCODE"]),                    DbType.String, ParameterDirection.Input),                                                    
                                                     helper.CreateParameter("PLANQTY",         Convert.ToString(drrow["PLANQTY"]).Replace(",",""),     DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("UNITCODE",        Convert.ToString(drrow["UNITCODE"]),                    DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("WORKCENTERCODE",  Convert.ToString(drrow["WORKCENTERCODE"]),              DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("MAKER",           LoginInfo.UserID,                                       DbType.String, ParameterDirection.Input)
                                                     );
                            break;                        
                    }
                    if (helper.RSCODE != "S") break;
                }
                if (helper.RSCODE != "S")
                {
                    helper.Rollback();
                    ShowDialog("데이터 등록에 실패하였습니다. ", DC00_WinForm.DialogForm.DialogType.OK);
                    return;
                }
                helper.Commit();
                ShowDialog("데이터 등록에 성공하였습니다. ", DC00_WinForm.DialogForm.DialogType.OK);
            }
            catch (Exception ex)
            {
                helper.Rollback();
            }
            finally
            {
                helper.Close();
            }
        }

    }
}
