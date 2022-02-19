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
using DC00_assm;
using DC00_WinForm;

namespace KFQB_Form
{
    public partial class PP_WorkerPerProd : DC00_WinForm.BaseMDIChildForm
    {
        private UltraGridUtil _GridUtil = new UltraGridUtil();
        Common _Common = new Common();
        DataTable dtTemp = new DataTable();
        string plantCode = LoginInfo.PlantCode;
        
        public PP_WorkerPerProd()
        {
            InitializeComponent();
        }

        private void PP_WorkerPerProd_Load(object sender, EventArgs e)
        {
            try
            {
                #region < 그리드 셋팅 >
                // 그리드 세팅을 위한 명령문
                _GridUtil.InitializeGrid(this.grid1, false, true, false, "", false);
                _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",       "공장",        true, GridColDataType_emu.VarChar,     130, 200, Infragistics.Win.HAlign.Left,  true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "WORKERID",        "작업자ID",    true, GridColDataType_emu.VarChar,     130, 200, Infragistics.Win.HAlign.Left,  false, false);
                _GridUtil.InitColumnUltraGrid(grid1, "WORKERNAME",      "작업자",      true, GridColDataType_emu.VarChar,     130, 200, Infragistics.Win.HAlign.Left,  true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "PRODDATE",        "생산일자",    true, GridColDataType_emu.YearMonthDay,130, 200, Infragistics.Win.HAlign.Left,  true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERCODE",  "작업장",      true, GridColDataType_emu.VarChar,     120, 120, Infragistics.Win.HAlign.Left,  true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERNAME",  "작업장명",    true, GridColDataType_emu.VarChar,     150, 120, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",        "품목",        true, GridColDataType_emu.VarChar,     150, 120, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME",        "품명",        true, GridColDataType_emu.VarChar,     200, 120, Infragistics.Win.HAlign.Left,  true, false);               
                _GridUtil.InitColumnUltraGrid(grid1, "PRODQTY",         "생산수량",    true, GridColDataType_emu.Double,      100, 120, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "BADQTY",          "불량수량",    true, GridColDataType_emu.Double,      100, 120, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "TOTALQTY",        "총생산량",    true, GridColDataType_emu.Double,      100, 120, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "DEFECTIVERATE",   "불량률",      true, GridColDataType_emu.VarChar,      100, 120, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",        "생산일시",    true, GridColDataType_emu.DateTime24,  130, 200, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.SetInitUltraGridBind(grid1);
                #endregion

                
                // 공장 정보를 데이터 테이블에 등록한다.
                dtTemp = _Common.Standard_CODE("PLANTCODE");
                Common.FillComboboxMaster(this.cboPlantCode, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName,dtTemp.Columns["CODE_NAME"].ColumnName,"ALL","");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", dtTemp, "CODE_ID", "CODE_NAME");

                // 작업자 팝업 호출 
                BizTextBoxManager txtBiz = new BizTextBoxManager();
                txtBiz.PopUpAdd(txtWorkerID, txtWorkerName, "WORKER_MASTER", new object[] { "", "", "", "", "" });

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
                string sPlantCOde   = Convert.ToString(cboPlantCode.Value);
                string sWorkCenterCode    = Convert.ToString(txtWorkerID.Text);

                dtTemp = helper.FillTable("15PP_WorkerPerProd_S1", CommandType.StoredProcedure
                                    , helper.CreateParameter("PLANTCODE",        sPlantCOde,        DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("WORKCENTERCODE",   sWorkCenterCode,   DbType.String, ParameterDirection.Input)
                                    );
                this.grid1.DataSource = dtTemp;
            }
            catch (Exception ex)
            {
                ShowDialog(ex.ToString(), DialogForm.DialogType.OK);
            }
            finally
            {
                helper.Close();
            }
        }


    }
}
