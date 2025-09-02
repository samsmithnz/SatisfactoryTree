namespace SatisfactoryTree.WinForm
{
    partial class ProductionHeaderControl
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
            this.tlpHeader = new System.Windows.Forms.TableLayoutPanel();
            this.lblItemHeader = new System.Windows.Forms.Label();
            this.lblRecipeHeader = new System.Windows.Forms.Label();
            this.lblQuantityHeader = new System.Windows.Forms.Label();
            this.lblProgressHeader = new System.Windows.Forms.Label();
            this.lblInputsHeader = new System.Windows.Forms.Label();
            this.lblBuildingsHeader = new System.Windows.Forms.Label();
            this.lblPowerHeader = new System.Windows.Forms.Label();
            this.lblActionsHeader = new System.Windows.Forms.Label();
            this.tlpHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpHeader
            // 
            this.tlpHeader.ColumnCount = 8;
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tlpHeader.Controls.Add(this.lblItemHeader, 0, 0);
            this.tlpHeader.Controls.Add(this.lblRecipeHeader, 1, 0);
            this.tlpHeader.Controls.Add(this.lblQuantityHeader, 2, 0);
            this.tlpHeader.Controls.Add(this.lblProgressHeader, 3, 0);
            this.tlpHeader.Controls.Add(this.lblInputsHeader, 4, 0);
            this.tlpHeader.Controls.Add(this.lblBuildingsHeader, 5, 0);
            this.tlpHeader.Controls.Add(this.lblPowerHeader, 6, 0);
            this.tlpHeader.Controls.Add(this.lblActionsHeader, 7, 0);
            this.tlpHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpHeader.Location = new System.Drawing.Point(0, 0);
            this.tlpHeader.Name = "tlpHeader";
            this.tlpHeader.RowCount = 1;
            this.tlpHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHeader.Size = new System.Drawing.Size(850, 25);
            this.tlpHeader.TabIndex = 0;
            // 
            // lblItemHeader
            // 
            this.lblItemHeader.AutoSize = true;
            this.lblItemHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblItemHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblItemHeader.Location = new System.Drawing.Point(3, 0);
            this.lblItemHeader.Name = "lblItemHeader";
            this.lblItemHeader.Size = new System.Drawing.Size(194, 25);
            this.lblItemHeader.TabIndex = 0;
            this.lblItemHeader.Text = "Item";
            this.lblItemHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRecipeHeader
            // 
            this.lblRecipeHeader.AutoSize = true;
            this.lblRecipeHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRecipeHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblRecipeHeader.Location = new System.Drawing.Point(203, 0);
            this.lblRecipeHeader.Name = "lblRecipeHeader";
            this.lblRecipeHeader.Size = new System.Drawing.Size(144, 25);
            this.lblRecipeHeader.TabIndex = 1;
            this.lblRecipeHeader.Text = "Recipe";
            this.lblRecipeHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblQuantityHeader
            // 
            this.lblQuantityHeader.AutoSize = true;
            this.lblQuantityHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblQuantityHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblQuantityHeader.Location = new System.Drawing.Point(353, 0);
            this.lblQuantityHeader.Name = "lblQuantityHeader";
            this.lblQuantityHeader.Size = new System.Drawing.Size(74, 25);
            this.lblQuantityHeader.TabIndex = 2;
            this.lblQuantityHeader.Text = "Quantity";
            this.lblQuantityHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProgressHeader
            // 
            this.lblProgressHeader.AutoSize = true;
            this.lblProgressHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProgressHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblProgressHeader.Location = new System.Drawing.Point(433, 0);
            this.lblProgressHeader.Name = "lblProgressHeader";
            this.lblProgressHeader.Size = new System.Drawing.Size(74, 25);
            this.lblProgressHeader.TabIndex = 3;
            this.lblProgressHeader.Text = "Progress";
            this.lblProgressHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInputsHeader
            // 
            this.lblInputsHeader.AutoSize = true;
            this.lblInputsHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInputsHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblInputsHeader.Location = new System.Drawing.Point(513, 0);
            this.lblInputsHeader.Name = "lblInputsHeader";
            this.lblInputsHeader.Size = new System.Drawing.Size(114, 25);
            this.lblInputsHeader.TabIndex = 4;
            this.lblInputsHeader.Text = "Inputs";
            this.lblInputsHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBuildingsHeader
            // 
            this.lblBuildingsHeader.AutoSize = true;
            this.lblBuildingsHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBuildingsHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblBuildingsHeader.Location = new System.Drawing.Point(633, 0);
            this.lblBuildingsHeader.Name = "lblBuildingsHeader";
            this.lblBuildingsHeader.Size = new System.Drawing.Size(114, 25);
            this.lblBuildingsHeader.TabIndex = 5;
            this.lblBuildingsHeader.Text = "Buildings";
            this.lblBuildingsHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPowerHeader
            // 
            this.lblPowerHeader.AutoSize = true;
            this.lblPowerHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPowerHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPowerHeader.Location = new System.Drawing.Point(753, 0);
            this.lblPowerHeader.Name = "lblPowerHeader";
            this.lblPowerHeader.Size = new System.Drawing.Size(74, 25);
            this.lblPowerHeader.TabIndex = 6;
            this.lblPowerHeader.Text = "Power (MW)";
            this.lblPowerHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblActionsHeader
            // 
            this.lblActionsHeader.AutoSize = true;
            this.lblActionsHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblActionsHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblActionsHeader.Location = new System.Drawing.Point(833, 0);
            this.lblActionsHeader.Name = "lblActionsHeader";
            this.lblActionsHeader.Size = new System.Drawing.Size(134, 25);
            this.lblActionsHeader.TabIndex = 7;
            this.lblActionsHeader.Text = "Actions";
            this.lblActionsHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProductionHeaderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tlpHeader);
            this.Name = "ProductionHeaderControl";
            this.Size = new System.Drawing.Size(850, 25);
            this.tlpHeader.ResumeLayout(false);
            this.tlpHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpHeader;
        private System.Windows.Forms.Label lblItemHeader;
        private System.Windows.Forms.Label lblRecipeHeader;
        private System.Windows.Forms.Label lblQuantityHeader;
        private System.Windows.Forms.Label lblProgressHeader;
        private System.Windows.Forms.Label lblInputsHeader;
        private System.Windows.Forms.Label lblBuildingsHeader;
        private System.Windows.Forms.Label lblPowerHeader;
        private System.Windows.Forms.Label lblActionsHeader;
    }
}