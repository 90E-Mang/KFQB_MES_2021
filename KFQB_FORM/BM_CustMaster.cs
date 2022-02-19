using DC00_assm;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KFQB_Form
{
    public partial class BM_CustMaster : DC00_WinForm.BaseMDIChildForm
    {
        private UltraGridUtil _GridUtil = new UltraGridUtil();
        public BM_CustMaster()
        {
            InitializeComponent();
        }

        private void BM_CustMaster_Load(object sender, EventArgs e)
        {
            try
            {
                #region < 그리드 셋팅 >
                // 그리드 세팅을 위한 명령문
                _GridUtil.InitializeGrid(this.grid1, false, true, false, "", false);
                _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",  "공장",               true, GridColDataType_emu.VarChar,     130, 200, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "CUSTTYPE",   "거래처유형",         true, GridColDataType_emu.VarChar,     130, 200, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "CUSTCODE",   "거래처코드",         true, GridColDataType_emu.VarChar,     130, 200, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "CUSTNAME",   "거래처명",           true, GridColDataType_emu.VarChar,     130, 200, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "BIZREQNO",   "사업자등록번호",     true, GridColDataType_emu.VarChar,     130, 200, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "BIZPOSTNO",  "우편번호",           true, GridColDataType_emu.VarChar,     130, 200, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "PHONE",      "전화번호",           true, GridColDataType_emu.VarChar,     130, 200, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "ADDRESS",    "주소",               true, GridColDataType_emu.VarChar,     130, 200, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "ASGNNAME",   "담당자명",           true, GridColDataType_emu.VarChar,     130, 200, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "USEFLAG",    "사용여부",           true, GridColDataType_emu.VarChar,     130, 200, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",   "등록일시",           true, GridColDataType_emu.DateTime24,  130, 200, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKER",      "등록자",             true, GridColDataType_emu.VarChar,     130, 200, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "EDITDATE",   "수정일시",           true, GridColDataType_emu.DateTime24,  130, 200, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "EDITOR",     "수정자",             true, GridColDataType_emu.VarChar,     130, 200, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.SetInitUltraGridBind(grid1);
                #endregion

                Common _Common = new Common();
                DataTable dtTemp = new DataTable();
                // 공장 정보를 데이터 테이블에 등록한다.
                dtTemp = _Common.Standard_CODE("PLANTCODE");
                Common.FillComboboxMaster(this.cboPlantCode, dtTemp,     dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName,"ALL","");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", dtTemp, "CODE_ID", "CODE_NAME");

                dtTemp = _Common.Standard_CODE("USEFLAG");
                Common.FillComboboxMaster(this.cboUseFlag,   dtTemp,     dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "USEFLAG",   dtTemp, "CODE_ID", "CODE_NAME");

                // 업체 유형
                dtTemp = _Common.Standard_CODE("CUSTTYPE");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "CUSTTYPE",  dtTemp, "CODE_ID", "CODE_NAME");

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
            //조회 버튼 클릭
            DBHelper helper = new DBHelper();
            try
            {
                string sPlantCode   = Convert.ToString(cboPlantCode.Value); // 공장
                string sCustCode    = Convert.ToString(txtCustCode.Text);   // 거래처 코드
                string sCustName    = Convert.ToString(txtCustName.Text);   // 거래처명
                string sUseFlag     = Convert.ToString(cboUseFlag.Value);   // 사용여부

                grid1.DataSource = helper.FillTable("15BM_CustMaster_S1", CommandType.StoredProcedure
                                                    ,helper.CreateParameter("PLANTCODE",    sPlantCode, DbType.String, ParameterDirection.Input)
                                                    ,helper.CreateParameter("CUSTCODE",     sCustCode,  DbType.String, ParameterDirection.Input)
                                                    ,helper.CreateParameter("CUSTNAME",     sCustName,  DbType.String, ParameterDirection.Input)
                                                    ,helper.CreateParameter("USEFLAG",      sUseFlag,   DbType.String, ParameterDirection.Input)
                                                    );
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
            base.DoNew();
            this.grid1.InsertRow(); // 신규 행 추가
            // 기본값 설정
            this.grid1.SetDefaultValue("PLANTCODE", LoginInfo.PlantCode);
            this.grid1.SetDefaultValue("USEFLAG", "Y");

            // 수정되면 안되는 컬럼 수정 불가 설정
            grid1.ActiveRow.Cells["MAKEDATE"].Activation = Activation.NoEdit;
            grid1.ActiveRow.Cells["MAKER"].Activation    = Activation.NoEdit;
            grid1.ActiveRow.Cells["EDITDATE"].Activation = Activation.NoEdit;
            grid1.ActiveRow.Cells["EDITOR"].Activation   = Activation.NoEdit;
        }

        public override void DoDelete()
        {
            // 삭제 버튼 클릭
            base.DoDelete();
            grid1.DeleteRow();
        }

        public override void DoSave()
        {
                                                        // 그리드에 바뀐 데이터가 있는 행을 테이블에 등록
            DataTable dt = grid1.chkChange();
            if (dt == null) return;                     // 바뀐 데이터행이 없으면 저장 종료
            DBHelper helper = new DBHelper("",true);    // 트랜잭션 설정 및 DB Open

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
                            drrow.RejectChanges();
                            helper.ExecuteNoneQuery("15BM_CustMaster_D1", CommandType.StoredProcedure
                                                    ,helper.CreateParameter("PLANTCODE", Convert.ToString(drrow["PLANTCODE"]),  DbType.String, ParameterDirection.Input)
                                                    ,helper.CreateParameter("CUSTTYPE",  Convert.ToString(drrow["CUSTTYPE"]),   DbType.String, ParameterDirection.Input)
                                                    ,helper.CreateParameter("CUSTCODE",  Convert.ToString(drrow["CUSTCODE"]),   DbType.String, ParameterDirection.Input)
                                                    );
                            break;
                        case DataRowState.Modified:
                            helper.ExecuteNoneQuery("15BM_CustMaster_U1", CommandType.StoredProcedure
                                                    ,helper.CreateParameter("PLANTCODE", Convert.ToString(drrow["PLANTCODE"]),  DbType.String, ParameterDirection.Input)
                                                    ,helper.CreateParameter("CUSTTYPE",  Convert.ToString(drrow["CUSTTYPE"]),   DbType.String, ParameterDirection.Input)
                                                    ,helper.CreateParameter("CUSTCODE",  Convert.ToString(drrow["CUSTCODE"]),   DbType.String, ParameterDirection.Input)
                                                    ,helper.CreateParameter("CUSTNAME",  Convert.ToString(drrow["CUSTNAME"]),   DbType.String, ParameterDirection.Input)
                                                    ,helper.CreateParameter("BIZREQNO",  Convert.ToString(drrow["BIZREQNO"]),   DbType.String, ParameterDirection.Input)
                                                    ,helper.CreateParameter("BIZPOSTNO", Convert.ToString(drrow["BIZPOSTNO"]),  DbType.String, ParameterDirection.Input)
                                                    ,helper.CreateParameter("PHONE",     Convert.ToString(drrow["PHONE"]),      DbType.String, ParameterDirection.Input)
                                                    ,helper.CreateParameter("ADDRESS",   Convert.ToString(drrow["ADDRESS"]),    DbType.String, ParameterDirection.Input)
                                                    ,helper.CreateParameter("ASGNNAME",  Convert.ToString(drrow["ASGNNAME"]),   DbType.String, ParameterDirection.Input)
                                                    ,helper.CreateParameter("USEFLAG",   Convert.ToString(drrow["USEFLAG"]),    DbType.String, ParameterDirection.Input)
                                                    ,helper.CreateParameter("EDITOR",    LoginInfo.UserID,                      DbType.String, ParameterDirection.Input)
                                                    );
                            break;
                        case DataRowState.Added:
                            helper.ExecuteNoneQuery("15BM_CustMaster_I1", CommandType.StoredProcedure
                                                    ,helper.CreateParameter("PLANTCODE", Convert.ToString(drrow["PLANTCODE"]),  DbType.String, ParameterDirection.Input)
                                                    ,helper.CreateParameter("CUSTTYPE",  Convert.ToString(drrow["CUSTTYPE"]),   DbType.String, ParameterDirection.Input)
                                                    ,helper.CreateParameter("CUSTCODE",  Convert.ToString(drrow["CUSTCODE"]),   DbType.String, ParameterDirection.Input)
                                                    ,helper.CreateParameter("CUSTNAME",  Convert.ToString(drrow["CUSTNAME"]),   DbType.String, ParameterDirection.Input)
                                                    ,helper.CreateParameter("BIZREQNO",  Convert.ToString(drrow["BIZREQNO"]),   DbType.String, ParameterDirection.Input)
                                                    ,helper.CreateParameter("BIZPOSTNO", Convert.ToString(drrow["BIZPOSTNO"]),  DbType.String, ParameterDirection.Input)
                                                    ,helper.CreateParameter("PHONE",     Convert.ToString(drrow["PHONE"]),      DbType.String, ParameterDirection.Input)
                                                    ,helper.CreateParameter("ADDRESS",   Convert.ToString(drrow["ADDRESS"]),    DbType.String, ParameterDirection.Input)
                                                    ,helper.CreateParameter("ASGNNAME",  Convert.ToString(drrow["ASGNNAME"]),   DbType.String, ParameterDirection.Input)
                                                    ,helper.CreateParameter("USEFLAG",   Convert.ToString(drrow["USEFLAG"]),    DbType.String, ParameterDirection.Input)
                                                    ,helper.CreateParameter("MAKER",     LoginInfo.UserID,                      DbType.String, ParameterDirection.Input)
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
