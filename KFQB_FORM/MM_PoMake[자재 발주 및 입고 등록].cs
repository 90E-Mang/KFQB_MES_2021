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
    public partial class MM_PoMake : DC00_WinForm.BaseMDIChildForm
    {
        private UltraGridUtil _GridUtil = new UltraGridUtil();
        public MM_PoMake()
        {
            InitializeComponent();
        }

        private void MM_PoMake_Load(object sender, EventArgs e)
        {
            try
            {
                #region < 그리드 셋팅 >
                // 그리드 세팅을 위한 명령문
                _GridUtil.InitializeGrid(this.grid1, false, true, false, "", false);
                _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",  "공장",       true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Left,  true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "PONO",       "발주번호",   true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",   "발주품목",   true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Left,  true, true);            
                _GridUtil.InitColumnUltraGrid(grid1, "PODATE",     "발주일자",   true, GridColDataType_emu.YearMonthDay, 130, 200, Infragistics.Win.HAlign.Left,  true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "POQTY",      "발주수량",   true, GridColDataType_emu.Double,       130, 200, Infragistics.Win.HAlign.Right, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE",   "단위",       true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Left,  true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "CUSTCODE",   "거래처",     true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Left,  true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "CHK",        "입고",       true, GridColDataType_emu.CheckBox,     130, 200, Infragistics.Win.HAlign.Left,  true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "INQTY",      "입고수량",   true, GridColDataType_emu.Double,       130, 200, Infragistics.Win.HAlign.Right, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "LOTNO",      "LOT번호",    true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Left,  true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "INDATE",     "입고일시",   true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Left,  true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "INWORKER",   "입고자",     true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Left,  true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",   "등록일시",   true, GridColDataType_emu.DateTime24,   130, 200, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKER",      "등록자",     true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "EDITDATE",   "수정일시",   true, GridColDataType_emu.DateTime24,   130, 200, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "EDITOR",     "수정자",     true, GridColDataType_emu.VarChar,      130, 200, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.SetInitUltraGridBind(grid1);
                #endregion

                Common _Common = new Common();
                DataTable dtTemp = new DataTable();
                // 공장 정보를 데이터 테이블에 등록한다.
                dtTemp = _Common.Standard_CODE("PLANTCODE");
                Common.FillComboboxMaster(this.cboPlantCode, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName,dtTemp.Columns["CODE_NAME"].ColumnName,"ALL","");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", dtTemp, "CODE_ID", "CODE_NAME");

                // 거래처 정보 기준정보에서 호출하여 콤보박스 생성
                dtTemp = _Common.GET_TB_CUSTMATTER_CODE("");
                    // ㄴ 디자인에 있는 콤보박스 컨트롤에 거래처 정보를 value 멤버와 display 멤버로 나누어서 표현하라.
                Common.FillComboboxMaster(this.cboCustCode,   dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                    // 그리드에 있는 해당 컬럼에 거래처 정보를 value 멤버와 display 멤버로 나누어서 표현하라.
                UltraGridUtil.SetComboUltraGrid(this.grid1, "CUSTCODE",   dtTemp, "CODE_ID", "CODE_NAME");

                // 단위
                dtTemp = _Common.Standard_CODE("UNITCODE");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "UNITCODE", dtTemp, "CODE_ID", "CODE_NAME");

                // 품목 정보 기준정보에서 호출하여 콤보박스 생성 (원자재 정보만 가지고 옴)
                dtTemp = _Common.GET_ItemCodeFERT_Code("ROH");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "ITEMCODE", dtTemp, "CODE_ID", "CODE_NAME");

                // 공장 코드 고정
                cboPlantCode.Value = LoginInfo.PlantCode;

                // 조회 시작 일자를 매월 1일로 고정
                dtStart.Value = string.Format("{0:yyyy-MM-01}", DateTime.Now);
                

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
                string sCustCode    = Convert.ToString(cboCustCode.Value);
                string sStartDate   = string.Format("{0:yyyy-MM-dd}", dtStart.Value);
                string sEndDate     = string.Format("{0:yyyy-MM-dd}", dtEnd.Value);
                string sPoNo        = Convert.ToString(txtPoNo.Text);

                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("15MM_PoMake_S1", CommandType.StoredProcedure, // 스토어 프로시저 이름을 만듦.
                                 helper.CreateParameter("PLANTCODE", sPlantCode,  DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("CUSTCODE",  sCustCode,   DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("STARTDATE", sStartDate,  DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("ENDDATE",   sEndDate,    DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("PONO",      sPoNo,       DbType.String, ParameterDirection.Input)); 


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
                grid1.ActiveRow.Cells["PONO"].Activation     = Activation.NoEdit;   // 발주번호
                grid1.ActiveRow.Cells["CHK"].Activation      = Activation.NoEdit;   // 입고선택
                grid1.ActiveRow.Cells["LOTNO"].Activation    = Activation.NoEdit;   // LOTNO
                grid1.ActiveRow.Cells["INDATE"].Activation   = Activation.NoEdit;   // 입고일자
                grid1.ActiveRow.Cells["INWORKER"].Activation = Activation.NoEdit;   // 입고자


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
            if (Convert.ToString(this.grid1.ActiveRow.Cells["CHK"].Value) ==  "1")
            {
                ShowDialog("입고가 완료된 발주내역은 삭제 할 수 없습니다.", DC00_WinForm.DialogForm.DialogType.OK);
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
                            helper.ExecuteNoneQuery("15MM_PoMake_D1", CommandType.StoredProcedure,
                                                     helper.CreateParameter("PLANTCODE", Convert.ToString(drrow["PLANTCODE"]),  DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("PONO" ,     Convert.ToString(drrow["PONO"]),       DbType.String, ParameterDirection.Input));
                            break;
                        case DataRowState.Modified:
                            // 구매자재 입고 등록
                            if (Convert.ToString(drrow["CHK"]) == "0")
                            {
                                continue;
                            }
                            if (Convert.ToString(drrow["INQTY"]) == "")
                            {
                                helper.Rollback();
                                ShowDialog("입고 수량을 입력하지 않았습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                                return;
                            }
                            helper.ExecuteNoneQuery("15MM_PoMake_U1", CommandType.StoredProcedure,
                                                     helper.CreateParameter("PLANTCODE", Convert.ToString(drrow["PLANTCODE"]),                   DbType.String, ParameterDirection.Input),  
                                                     helper.CreateParameter("PONO",      Convert.ToString(drrow["PONO"]),                        DbType.String, ParameterDirection.Input),                                                    
                                                     helper.CreateParameter("LOTNO",     Convert.ToString(drrow["LOTNO"]),                       DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("ITEMCODE",  Convert.ToString(drrow["ITEMCODE"]),                    DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("INQTY",     Convert.ToString(drrow["INQTY"]).Replace(",",""),       DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("MAKER",     LoginInfo.UserID,                                       DbType.String, ParameterDirection.Input)
                                                     );
                            break;
                        case DataRowState.Added:
                            string sMsg = string.Empty;
                            if (Convert.ToString(drrow["ITEMCODE"]) == "")
                            {
                                sMsg += "품목 ";
                            }
                            if (Convert.ToString(drrow["POQTY"]) == "")
                            {
                                sMsg += "발주 수량 ";
                            }
                            if (Convert.ToString(drrow["CUSTCODE"]) == "")
                            {
                                sMsg += "거래처  ";
                            }
                            if (sMsg != "")
                            {
                                helper.Rollback();
                                ShowDialog(sMsg + " 을(를) 입력하지 않았습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                                // this.grid1.DeleteRow();
                                return;
                            }

                            helper.ExecuteNoneQuery("15MM_PoMake_I1", CommandType.StoredProcedure,
                                                     helper.CreateParameter("PLANTCODE", Convert.ToString(drrow["PLANTCODE"]),                   DbType.String, ParameterDirection.Input),  
                                                     helper.CreateParameter("ITEMCODE",  Convert.ToString(drrow["ITEMCODE"]),                    DbType.String, ParameterDirection.Input),                                                    
                                                     helper.CreateParameter("POQTY",     Convert.ToString(drrow["POQTY"]).Replace(",",""),       DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("UNITCODE",  Convert.ToString(drrow["UNITCODE"]),                    DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("CUSTCODE",  Convert.ToString(drrow["CUSTCODE"]),                    DbType.String, ParameterDirection.Input),
                                                     helper.CreateParameter("MAKER",     LoginInfo.UserID,                                       DbType.String, ParameterDirection.Input)
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
