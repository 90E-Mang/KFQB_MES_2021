using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DC00_assm;


namespace DC_POPUP
{
    public partial class POP_ORDERNO : DC00_WinForm.BasePopupForm
    {

        #region [ 선언자 ]
        //그리드 객체 생성
        UltraGridUtil _GridUtil = new UltraGridUtil();

        //임시로 사용할 데이터테이블 생성
        DataTable _DtTemp = new DataTable();
        #endregion

        public POP_ORDERNO(string sWorkcenterCode, string sWorkcenterName)
        {
            InitializeComponent();
            txtWorkCenterCode.Text = sWorkcenterCode;
            txtWorkCenterName.Text = sWorkcenterName;
        }

        private void POP_ORDERNO_Load(object sender, EventArgs e)
        {
            _GridUtil.InitializeGrid(this.Grid1);

            _GridUtil.InitColumnUltraGrid(Grid1, "PLANTCODE",       "공장",            false, GridColDataType_emu.VarChar, 120, 100, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(Grid1, "WORKCENTERCODE",  "직압장",          false, GridColDataType_emu.VarChar, 110, 100, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(Grid1, "WORKCENTERNAME",  "작업자명",        false, GridColDataType_emu.VarChar, 140, 100, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(Grid1, "ORDERNO",         "작업지시번호",    false, GridColDataType_emu.VarChar, 170, 100, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(Grid1, "ORDERDATE",       "작업지시일자",    false, GridColDataType_emu.VarChar, 170, 100, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(Grid1, "ITEMCODE",        "지시 품목",       false, GridColDataType_emu.VarChar, 170, 100, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(Grid1, "ITEMNAME",        "지시 품명",       false, GridColDataType_emu.VarChar, 170, 100, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(Grid1, "ORDERQTY",        "지시 수량",       false, GridColDataType_emu.Double,  170, 100, Infragistics.Win.HAlign.Right, true, false);
            _GridUtil.SetInitUltraGridBind(Grid1);

            search();
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            search();
        }

        private void search()
        {
            DBHelper helper = new DBHelper(false);
            try
            {
                _GridUtil.Grid_Clear(Grid1);
                string sPlantCode      = Convert.ToString(cboPlantCode_H.Value);
                string sWorkCenterCode = Convert.ToString(txtWorkCenterCode.Text);
                string sWorkCenterName = Convert.ToString(txtWorkCenterName.Text);
                string sStartDate      = string.Format("{0:yyyy-MM-dd}",dtStart.Value);
                string sEndDate        = string.Format("{0:yyyy-MM-dd}",dtEnd.Value);

                DataTable rtnDtTemp = new DataTable();
                rtnDtTemp = helper.FillTable("15POP_OrderNo_S1", CommandType.StoredProcedure,
                                 helper.CreateParameter("PLANTCODE",      sPlantCode,      DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("WORKCENTERCODE", sWorkCenterCode, DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("WORKCENTERNAME", sWorkCenterName, DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("STARTDATE",      sStartDate,      DbType.String, ParameterDirection.Input),
                                 helper.CreateParameter("ENDDATE",        sEndDate,        DbType.String, ParameterDirection.Input)
                                 );
                this.Grid1.DataSource = rtnDtTemp;
            }
            catch (Exception ex)
            {
                MessageBox.Show("선택하신 날짜에는 작업장에 내려진 작업지시가 없습니다.");
            }
            finally
            {
                helper.Close();
            }
        }
        private void Grid1_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            this.Tag = Convert.ToString(Grid1.ActiveRow.Cells["ORDERNO"].Value);
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dtStart_BeforeDropDown(object sender, CancelEventArgs e)
        {

        }

        private void ultraLabel2_Click(object sender, EventArgs e)
        {

        }


    }
}
