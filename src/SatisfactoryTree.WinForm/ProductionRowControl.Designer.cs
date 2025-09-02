namespace SatisfactoryTree.WinForm
{
    partial class ProductionRowControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblItem = new System.Windows.Forms.Label();
            this.cmbRecipe = new System.Windows.Forms.ComboBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblInputs = new System.Windows.Forms.Label();
            this.lblBuildings = new System.Windows.Forms.Label();
            this.lblPower = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnToggleMethod = new System.Windows.Forms.Button();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 9;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpMain.Controls.Add(this.lblItem, 0, 0);
            this.tlpMain.Controls.Add(this.cmbRecipe, 1, 0);
            this.tlpMain.Controls.Add(this.txtQuantity, 2, 0);
            this.tlpMain.Controls.Add(this.progressBar, 3, 0);
            this.tlpMain.Controls.Add(this.lblInputs, 4, 0);
            this.tlpMain.Controls.Add(this.lblBuildings, 5, 0);
            this.tlpMain.Controls.Add(this.lblPower, 6, 0);
            this.tlpMain.Controls.Add(this.btnEdit, 7, 0);
            this.tlpMain.Controls.Add(this.btnToggleMethod, 8, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(850, 35);
            this.tlpMain.TabIndex = 0;
            // 
            // lblItem
            // 
            this.lblItem.AutoSize = true;
            this.lblItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblItem.Location = new System.Drawing.Point(3, 0);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(194, 35);
            this.lblItem.TabIndex = 0;
            this.lblItem.Text = "Item Name";
            this.lblItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbRecipe
            // 
            this.cmbRecipe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbRecipe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRecipe.FormattingEnabled = true;
            this.cmbRecipe.Location = new System.Drawing.Point(203, 6);
            this.cmbRecipe.Name = "cmbRecipe";
            this.cmbRecipe.Size = new System.Drawing.Size(144, 23);
            this.cmbRecipe.TabIndex = 1;
            this.cmbRecipe.SelectedIndexChanged += new System.EventHandler(this.cmbRecipe_SelectedIndexChanged);
            // 
            // txtQuantity
            // 
            this.txtQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuantity.Location = new System.Drawing.Point(353, 6);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(74, 23);
            this.txtQuantity.TabIndex = 2;
            this.txtQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQuantity.Leave += new System.EventHandler(this.txtQuantity_Leave);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(433, 6);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(74, 23);
            this.progressBar.TabIndex = 3;
            // 
            // lblInputs
            // 
            this.lblInputs.AutoSize = true;
            this.lblInputs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInputs.Location = new System.Drawing.Point(513, 0);
            this.lblInputs.Name = "lblInputs";
            this.lblInputs.Size = new System.Drawing.Size(114, 35);
            this.lblInputs.TabIndex = 4;
            this.lblInputs.Text = "Inputs";
            this.lblInputs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBuildings
            // 
            this.lblBuildings.AutoSize = true;
            this.lblBuildings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBuildings.Location = new System.Drawing.Point(633, 0);
            this.lblBuildings.Name = "lblBuildings";
            this.lblBuildings.Size = new System.Drawing.Size(114, 35);
            this.lblBuildings.TabIndex = 5;
            this.lblBuildings.Text = "Buildings";
            this.lblBuildings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPower
            // 
            this.lblPower.AutoSize = true;
            this.lblPower.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPower.Location = new System.Drawing.Point(753, 0);
            this.lblPower.Name = "lblPower";
            this.lblPower.Size = new System.Drawing.Size(74, 35);
            this.lblPower.TabIndex = 6;
            this.lblPower.Text = "0";
            this.lblPower.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(833, 6);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(54, 23);
            this.btnEdit.TabIndex = 7;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnToggleMethod
            // 
            this.btnToggleMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToggleMethod.Location = new System.Drawing.Point(893, 6);
            this.btnToggleMethod.Name = "btnToggleMethod";
            this.btnToggleMethod.Size = new System.Drawing.Size(74, 23);
            this.btnToggleMethod.TabIndex = 8;
            this.btnToggleMethod.Text = "Toggle";
            this.btnToggleMethod.UseVisualStyleBackColor = true;
            this.btnToggleMethod.Click += new System.EventHandler(this.btnToggleMethod_Click);
            // 
            // ProductionRowControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tlpMain);
            this.Name = "ProductionRowControl";
            this.Size = new System.Drawing.Size(850, 35);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lblItem;
        private System.Windows.Forms.ComboBox cmbRecipe;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblInputs;
        private System.Windows.Forms.Label lblBuildings;
        private System.Windows.Forms.Label lblPower;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnToggleMethod;
    }
}