
namespace KFQB_Form
{
    partial class PP_ActureOutput
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            this.label1 = new System.Windows.Forms.Label();
            this.cboPlantCode = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.label2 = new System.Windows.Forms.Label();
            this.txtInLotNo = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.label4 = new System.Windows.Forms.Label();
            this.cboWorkercenterCode = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.grid1 = new DC00_Component.Grid(this.components);
            this.btnOrderClose = new Infragistics.Win.Misc.UltraButton();
            this.txtWorkerID = new DC00_Component.SBtnTextEditor();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWorkerName = new DC00_Component.STextBox(this.components);
            this.btnWorker = new Infragistics.Win.Misc.UltraButton();
            this.btnOrderSelect = new Infragistics.Win.Misc.UltraButton();
            this.btnLotin = new Infragistics.Win.Misc.UltraButton();
            this.btnRunStop = new Infragistics.Win.Misc.UltraButton();
            this.label5 = new System.Windows.Forms.Label();
            this.txtProdQty = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBadQty = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.btnProdIn = new Infragistics.Win.Misc.UltraButton();
            ((System.ComponentModel.ISupportInitialize)(this.gbxHeader)).BeginInit();
            this.gbxHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbxBody)).BeginInit();
            this.gbxBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlantCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInLotNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboWorkercenterCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkerID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProdQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBadQty)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxHeader
            // 
            this.gbxHeader.ContentPadding.Bottom = 2;
            this.gbxHeader.ContentPadding.Left = 2;
            this.gbxHeader.ContentPadding.Right = 2;
            this.gbxHeader.ContentPadding.Top = 4;
            this.gbxHeader.Controls.Add(this.btnProdIn);
            this.gbxHeader.Controls.Add(this.btnRunStop);
            this.gbxHeader.Controls.Add(this.btnLotin);
            this.gbxHeader.Controls.Add(this.btnOrderSelect);
            this.gbxHeader.Controls.Add(this.btnWorker);
            this.gbxHeader.Controls.Add(this.txtWorkerName);
            this.gbxHeader.Controls.Add(this.label3);
            this.gbxHeader.Controls.Add(this.txtWorkerID);
            this.gbxHeader.Controls.Add(this.btnOrderClose);
            this.gbxHeader.Controls.Add(this.txtBadQty);
            this.gbxHeader.Controls.Add(this.label6);
            this.gbxHeader.Controls.Add(this.txtProdQty);
            this.gbxHeader.Controls.Add(this.label5);
            this.gbxHeader.Controls.Add(this.txtInLotNo);
            this.gbxHeader.Controls.Add(this.label2);
            this.gbxHeader.Controls.Add(this.cboWorkercenterCode);
            this.gbxHeader.Controls.Add(this.label4);
            this.gbxHeader.Controls.Add(this.cboPlantCode);
            this.gbxHeader.Controls.Add(this.label1);
            this.gbxHeader.Size = new System.Drawing.Size(1174, 167);
            // 
            // gbxBody
            // 
            this.gbxBody.ContentPadding.Bottom = 4;
            this.gbxBody.ContentPadding.Left = 4;
            this.gbxBody.ContentPadding.Right = 4;
            this.gbxBody.ContentPadding.Top = 6;
            this.gbxBody.Controls.Add(this.grid1);
            this.gbxBody.Location = new System.Drawing.Point(0, 167);
            this.gbxBody.Size = new System.Drawing.Size(1174, 658);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "공장";
            // 
            // cboPlantCode
            // 
            this.cboPlantCode.Location = new System.Drawing.Point(83, 27);
            this.cboPlantCode.Name = "cboPlantCode";
            this.cboPlantCode.Size = new System.Drawing.Size(158, 29);
            this.cboPlantCode.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(260, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "LOT 번호";
            // 
            // txtInLotNo
            // 
            this.txtInLotNo.Location = new System.Drawing.Point(337, 71);
            this.txtInLotNo.Name = "txtInLotNo";
            this.txtInLotNo.Size = new System.Drawing.Size(158, 29);
            this.txtInLotNo.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(254, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "(1) 작업장";
            // 
            // cboWorkercenterCode
            // 
            this.cboWorkercenterCode.Location = new System.Drawing.Point(337, 27);
            this.cboWorkercenterCode.Name = "cboWorkercenterCode";
            this.cboWorkercenterCode.Size = new System.Drawing.Size(158, 29);
            this.cboWorkercenterCode.TabIndex = 1;
            this.cboWorkercenterCode.ValueChanged += new System.EventHandler(this.cboWorkcenterCode_ValueChanged);
            // 
            // grid1
            // 
            this.grid1.AutoResizeColumn = true;
            this.grid1.AutoUserColumn = true;
            this.grid1.ContextMenuCopyEnabled = true;
            this.grid1.ContextMenuDeleteEnabled = true;
            this.grid1.ContextMenuExcelEnabled = true;
            this.grid1.ContextMenuInsertEnabled = true;
            this.grid1.ContextMenuPasteEnabled = true;
            this.grid1.DeleteButtonEnable = true;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.grid1.DisplayLayout.Appearance = appearance1;
            this.grid1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.grid1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.grid1.DisplayLayout.DefaultSelectedBackColor = System.Drawing.Color.Empty;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.grid1.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grid1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance4;
            this.grid1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.grid1.DisplayLayout.GroupByBox.Hidden = true;
            appearance3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance3.BackColor2 = System.Drawing.SystemColors.Control;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grid1.DisplayLayout.GroupByBox.PromptAppearance = appearance3;
            this.grid1.DisplayLayout.MaxColScrollRegions = 1;
            this.grid1.DisplayLayout.MaxRowScrollRegions = 1;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            appearance7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grid1.DisplayLayout.Override.ActiveCellAppearance = appearance7;
            appearance10.BackColor = System.Drawing.SystemColors.Highlight;
            appearance10.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.grid1.DisplayLayout.Override.ActiveRowAppearance = appearance10;
            this.grid1.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.True;
            this.grid1.DisplayLayout.Override.AllowMultiCellOperations = ((Infragistics.Win.UltraWinGrid.AllowMultiCellOperation)(((Infragistics.Win.UltraWinGrid.AllowMultiCellOperation.Copy | Infragistics.Win.UltraWinGrid.AllowMultiCellOperation.Cut) 
            | Infragistics.Win.UltraWinGrid.AllowMultiCellOperation.Paste)));
            this.grid1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.grid1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance12.BackColor = System.Drawing.SystemColors.Window;
            this.grid1.DisplayLayout.Override.CardAreaAppearance = appearance12;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.grid1.DisplayLayout.Override.CellAppearance = appearance8;
            this.grid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.grid1.DisplayLayout.Override.CellPadding = 0;
            appearance6.BackColor = System.Drawing.SystemColors.Control;
            appearance6.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance6.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance6.BorderColor = System.Drawing.SystemColors.Window;
            this.grid1.DisplayLayout.Override.GroupByRowAppearance = appearance6;
            appearance5.TextHAlignAsString = "Left";
            this.grid1.DisplayLayout.Override.HeaderAppearance = appearance5;
            this.grid1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grid1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.grid1.DisplayLayout.Override.RowAppearance = appearance11;
            this.grid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance9.BackColor = System.Drawing.SystemColors.ControlLight;
            this.grid1.DisplayLayout.Override.TemplateAddRowAppearance = appearance9;
            this.grid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grid1.DisplayLayout.SelectionOverlayBorderThickness = 2;
            this.grid1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.grid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid1.EnterNextRowEnable = true;
            this.grid1.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.grid1.Location = new System.Drawing.Point(6, 6);
            this.grid1.Name = "grid1";
            this.grid1.Size = new System.Drawing.Size(1162, 646);
            this.grid1.TabIndex = 0;
            this.grid1.Text = "grid1";
            this.grid1.TextRenderingMode = Infragistics.Win.TextRenderingMode.GDI;
            this.grid1.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnCellChange;
            this.grid1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.grid1.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grid1.AfterRowActivate += new System.EventHandler(this.grid1_AfterRowActivate);
            // 
            // btnOrderClose
            // 
            this.btnOrderClose.Location = new System.Drawing.Point(963, 88);
            this.btnOrderClose.Name = "btnOrderClose";
            this.btnOrderClose.Size = new System.Drawing.Size(151, 49);
            this.btnOrderClose.TabIndex = 4;
            this.btnOrderClose.Text = "(7) 작업 지시 종료";
            this.btnOrderClose.Click += new System.EventHandler(this.btnOrderClose_Click);
            // 
            // txtWorkerID
            // 
            appearance13.FontData.BoldAsString = "False";
            appearance13.FontData.UnderlineAsString = "False";
            appearance13.ForeColor = System.Drawing.Color.Black;
            this.txtWorkerID.Appearance = appearance13;
            this.txtWorkerID.btnImgType = DC00_Component.SBtnTextEditor.ButtonImgTypeEnum.Type1;
            this.txtWorkerID.btnWidth = 26;
            this.txtWorkerID.Location = new System.Drawing.Point(586, 27);
            this.txtWorkerID.Name = "txtWorkerID";
            this.txtWorkerID.RequireFlag = DC00_Component.SBtnTextEditor.RequireFlagEnum.NO;
            this.txtWorkerID.RequirePop = DC00_Component.SBtnTextEditor.RequireFlagEnum.NO;
            this.txtWorkerID.Size = new System.Drawing.Size(139, 29);
            this.txtWorkerID.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(526, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "작업자";
            // 
            // txtWorkerName
            // 
            appearance14.FontData.BoldAsString = "False";
            appearance14.FontData.UnderlineAsString = "False";
            appearance14.ForeColor = System.Drawing.Color.Black;
            this.txtWorkerName.Appearance = appearance14;
            this.txtWorkerName.Location = new System.Drawing.Point(731, 27);
            this.txtWorkerName.Name = "txtWorkerName";
            this.txtWorkerName.RequireFlag = DC00_Component.STextBox.RequireFlagEnum.NO;
            this.txtWorkerName.RequirePop = DC00_Component.STextBox.RequireFlagEnum.NO;
            this.txtWorkerName.Size = new System.Drawing.Size(204, 29);
            this.txtWorkerName.TabIndex = 7;
            // 
            // btnWorker
            // 
            this.btnWorker.Location = new System.Drawing.Point(963, 27);
            this.btnWorker.Name = "btnWorker";
            this.btnWorker.Size = new System.Drawing.Size(135, 29);
            this.btnWorker.TabIndex = 8;
            this.btnWorker.Text = "(2) 작업자 등록";
            this.btnWorker.Click += new System.EventHandler(this.btnWorker_Click);
            // 
            // btnOrderSelect
            // 
            this.btnOrderSelect.Location = new System.Drawing.Point(41, 71);
            this.btnOrderSelect.Name = "btnOrderSelect";
            this.btnOrderSelect.Size = new System.Drawing.Size(142, 54);
            this.btnOrderSelect.TabIndex = 9;
            this.btnOrderSelect.Text = "(3) 작업지시 선택";
            this.btnOrderSelect.Click += new System.EventHandler(this.btnOrderSelect_Click);
            // 
            // btnLotin
            // 
            this.btnLotin.Location = new System.Drawing.Point(271, 106);
            this.btnLotin.Name = "btnLotin";
            this.btnLotin.Size = new System.Drawing.Size(224, 29);
            this.btnLotin.TabIndex = 10;
            this.btnLotin.Text = "(4) LOT 투입";
            this.btnLotin.Click += new System.EventHandler(this.btnLotIn_Click);
            // 
            // btnRunStop
            // 
            this.btnRunStop.Location = new System.Drawing.Point(501, 69);
            this.btnRunStop.Name = "btnRunStop";
            this.btnRunStop.Size = new System.Drawing.Size(152, 66);
            this.btnRunStop.TabIndex = 11;
            this.btnRunStop.Text = "(5) 가동";
            this.btnRunStop.Click += new System.EventHandler(this.btnRunStop_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(671, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "양품 수량";
            // 
            // txtProdQty
            // 
            this.txtProdQty.Location = new System.Drawing.Point(748, 62);
            this.txtProdQty.Name = "txtProdQty";
            this.txtProdQty.Size = new System.Drawing.Size(158, 29);
            this.txtProdQty.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(671, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 20);
            this.label6.TabIndex = 2;
            this.label6.Text = "불량 수량";
            // 
            // txtBadQty
            // 
            this.txtBadQty.Location = new System.Drawing.Point(748, 97);
            this.txtBadQty.Name = "txtBadQty";
            this.txtBadQty.Size = new System.Drawing.Size(158, 29);
            this.txtBadQty.TabIndex = 3;
            // 
            // btnProdIn
            // 
            this.btnProdIn.Location = new System.Drawing.Point(675, 131);
            this.btnProdIn.Name = "btnProdIn";
            this.btnProdIn.Size = new System.Drawing.Size(231, 29);
            this.btnProdIn.TabIndex = 12;
            this.btnProdIn.Text = "(6) 생산 실적 등록";
            this.btnProdIn.Click += new System.EventHandler(this.btnProdIn_Click);
            // 
            // PP_ActureOutput
            // 
            this.ClientSize = new System.Drawing.Size(1174, 825);
            this.Name = "PP_ActureOutput";
            this.Text = "생산 실적 등록";
            this.Load += new System.EventHandler(this.PP_ActureOutput_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gbxHeader)).EndInit();
            this.gbxHeader.ResumeLayout(false);
            this.gbxHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbxBody)).EndInit();
            this.gbxBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboPlantCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInLotNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboWorkercenterCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkerID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProdQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBadQty)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtInLotNo;
        private System.Windows.Forms.Label label2;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor cboWorkercenterCode;
        private System.Windows.Forms.Label label4;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor cboPlantCode;
        private System.Windows.Forms.Label label1;
        private DC00_Component.Grid grid1;
        private Infragistics.Win.Misc.UltraButton btnOrderClose;
        private Infragistics.Win.Misc.UltraButton btnProdIn;
        private Infragistics.Win.Misc.UltraButton btnRunStop;
        private Infragistics.Win.Misc.UltraButton btnLotin;
        private Infragistics.Win.Misc.UltraButton btnOrderSelect;
        private Infragistics.Win.Misc.UltraButton btnWorker;
        private DC00_Component.STextBox txtWorkerName;
        private System.Windows.Forms.Label label3;
        private DC00_Component.SBtnTextEditor txtWorkerID;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtBadQty;
        private System.Windows.Forms.Label label6;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtProdQty;
        private System.Windows.Forms.Label label5;
    }
}
