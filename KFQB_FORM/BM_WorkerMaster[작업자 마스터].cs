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
    public partial class BM_WorkerMaster : DC00_WinForm.BaseMDIChildForm
    {
        private UltraGridUtil _GridUtil = new UltraGridUtil();
        public BM_WorkerMaster()
        {
            InitializeComponent();
        }

        private void BM_WorkerMaster_Load(object sender, EventArgs e)
        {
            try
            {
                #region < 그리드 셋팅 >
                // 그리드 세팅을 위한 명령문
                _GridUtil.InitializeGrid(this.grid1, false, true, false, "", false);
                _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",  "공장",     true, GridColDataType_emu.VarChar,     130, 200, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "WORKERID",   "작업자ID", true, GridColDataType_emu.VarChar,     130, 200, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "WORKERNAME", "작업자명", true, GridColDataType_emu.VarChar,     130, 200, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "BANCODE",    "작업반",   true, GridColDataType_emu.VarChar,     130, 200, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "GRPID",      "그룹",     true, GridColDataType_emu.VarChar,     130, 200, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "DEPTCODE",   "부서",     true, GridColDataType_emu.VarChar,     130, 200, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "PHONENO",    "전화번호", true, GridColDataType_emu.VarChar,     130, 200, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "INDATE",     "입사일자", true, GridColDataType_emu.VarChar,     130, 200, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "OUTDATE",    "퇴사일자", true, GridColDataType_emu.VarChar,     130, 200, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "USEFLAG",    "사용여부", true, GridColDataType_emu.VarChar,     130, 200, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",   "등록일시", true, GridColDataType_emu.DateTime24,  130, 200, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKER",      "등록자",   true, GridColDataType_emu.VarChar,     130, 200, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "EDITDATE",   "수정일시", true, GridColDataType_emu.DateTime24,  130, 200, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "EDITOR",     "수정자",   true, GridColDataType_emu.VarChar,     130, 200, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.SetInitUltraGridBind(grid1);
                #endregion

                Common _Common = new Common();
                DataTable dtTemp = new DataTable();
                // 공장 정보를 데이터 테이블에 등록한다.
                dtTemp = _Common.Standard_CODE("PLANTCODE");
                Common.FillComboboxMaster(this.cboPlantCode, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName,dtTemp.Columns["CODE_NAME"].ColumnName,"ALL","");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", dtTemp, "CODE_ID", "CODE_NAME");

                dtTemp = _Common.Standard_CODE("USEFLAG");
                Common.FillComboboxMaster(this.cboUseFlag,   dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "USEFLAG",   dtTemp, "CODE_ID", "CODE_NAME");

                // 그룹 코드 그리드에 표현
                dtTemp = _Common.Standard_CODE("GRPID");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "GRPID",     dtTemp, "CODE_ID", "CODE_NAME");

                // 반 코드 그리드에 표현
                dtTemp = _Common.Standard_CODE("BANCODE");
                Common.FillComboboxMaster(this.cboUseFlag,   dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "BANCODE",   dtTemp, "CODE_ID", "CODE_NAME");

                // 부서 코드 그리드에 표현
                dtTemp = _Common.Standard_CODE("DEPTCODE");
                Common.FillComboboxMaster(this.cboUseFlag,   dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "DEPTCODE",  dtTemp, "CODE_ID", "CODE_NAME");

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
                string sWorkerID    = Convert.ToString(txtWorkerID.Text);
                string sWorkerName  = Convert.ToString(txtWorkerName.Text);
                string sUseFlag     = Convert.ToString(cboUseFlag.Value);

                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("15BM_WorkerMaster_S1", CommandType.StoredProcedure, 
                                 helper.CreateParameter("PLANTCODE", sPlantCode,  DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("WORKERID",  sWorkerID,   DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("WORKERNAME",sWorkerName, DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("USEFLAG",   sUseFlag,    DbType.String, ParameterDirection.Input)); // 스토어 프로시저 이름을 만듦.

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
                this.grid1.ActiveRow.Cells["USEFLAG"].Value   = "Y";

                // 수정 불가 컬럼 설정
                grid1.ActiveRow.Cells["MAKEDATE"].Activation = Activation.NoEdit; // 생성일시
                grid1.ActiveRow.Cells["MAKER"].Activation    = Activation.NoEdit; // 생성자
                grid1.ActiveRow.Cells["EDITDATE"].Activation = Activation.NoEdit; // 등록일시
                grid1.ActiveRow.Cells["EDITOR"].Activation   = Activation.NoEdit; // 등록자

            }
            catch (Exception ex)
            {

                
            }
        }

        public override void DoDelete()
        {
            // 삭제 버튼 클릭
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
                            helper.ExecuteNoneQuery("15BM_WorkerMaster_D1", CommandType.StoredProcedure,
                                                     helper.CreateParameter("PLANTCODE", Convert.ToString(drrow["PLANTCODE"]),  DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("WORKERID" , Convert.ToString(drrow["WORKERID"]),   DbType.String, ParameterDirection.Input));
                            break;
                        case DataRowState.Modified:
                            helper.ExecuteNoneQuery("15BM_WorkerMaster_U1", CommandType.StoredProcedure,
                                                     helper.CreateParameter("PLANTCODE", Convert.ToString(drrow["PLANTCODE"]),  DbType.String, ParameterDirection.Input),  // where 조건문
                                                     helper.CreateParameter("WORKERID",  Convert.ToString(drrow["WORKERID"]),   DbType.String, ParameterDirection.Input),  // where 조건문                                                  
                                                     helper.CreateParameter("WORKERNAME",Convert.ToString(drrow["WORKERNAME"]), DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("BANCODE",   Convert.ToString(drrow["BANCODE"]),    DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("GRPID",     Convert.ToString(drrow["GRPID"]),      DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("DEPTCODE",  Convert.ToString(drrow["DEPTCODE"]),   DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("PHONENO",   Convert.ToString(drrow["PHONENO"]),    DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("INDATE",    Convert.ToString(drrow["INDATE"]),     DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("OUTDATE",   Convert.ToString(drrow["OUTDATE"]),    DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("USEFLAG",   Convert.ToString(drrow["USEFLAG"]),    DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("EDITOR",    LoginInfo.UserID ,                     DbType.String, ParameterDirection.Input)
                                                     );
                            break;
                        case DataRowState.Added:
                            helper.ExecuteNoneQuery("15BM_WorkerMaster_I1", CommandType.StoredProcedure,
                                                     helper.CreateParameter("PLANTCODE", Convert.ToString(drrow["PLANTCODE"]),  DbType.String, ParameterDirection.Input),  
                                                     helper.CreateParameter("WORKERID",  Convert.ToString(drrow["WORKERID"]),   DbType.String, ParameterDirection.Input),                                                    
                                                     helper.CreateParameter("WORKERNAME",Convert.ToString(drrow["WORKERNAME"]), DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("BANCODE",   Convert.ToString(drrow["BANCODE"]),    DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("GRPID",     Convert.ToString(drrow["GRPID"]),      DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("DEPTCODE",  Convert.ToString(drrow["DEPTCODE"]),   DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("PHONENO",   Convert.ToString(drrow["PHONENO"]),    DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("INDATE",    Convert.ToString(drrow["INDATE"]),     DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("OUTDATE",   Convert.ToString(drrow["OUTDATE"]),    DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("USEFLAG",   Convert.ToString(drrow["USEFLAG"]),    DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("MAKER",     LoginInfo.UserID,                      DbType.String, ParameterDirection.Input)
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
