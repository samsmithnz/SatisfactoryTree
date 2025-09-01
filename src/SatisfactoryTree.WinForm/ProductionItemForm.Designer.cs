namespace SatisfactoryTree.WinForm
{
    partial class ProductionItemForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpProductionItem = new System.Windows.Forms.GroupBox();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.lblItemName = new System.Windows.Forms.Label();
            this.txtTargetQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblTargetQuantity = new System.Windows.Forms.Label();
            this.grpInputMethod = new System.Windows.Forms.GroupBox();
            this.rbImportInputs = new System.Windows.Forms.RadioButton();
            this.rbProduceOnsite = new System.Windows.Forms.RadioButton();
            this.chkAutoDependencies = new System.Windows.Forms.CheckBox();
            this.grpRecipeInfo = new System.Windows.Forms.GroupBox();
            this.lblRecipe = new System.Windows.Forms.Label();
            this.cmbRecipe = new System.Windows.Forms.ComboBox();
            this.lblBuildings = new System.Windows.Forms.Label();
            this.txtBuildingsRequired = new System.Windows.Forms.TextBox();
            this.lblInputsNeeded = new System.Windows.Forms.Label();
            this.lstInputsNeeded = new System.Windows.Forms.ListView();
            this.colInputItem = new System.Windows.Forms.ColumnHeader();
            this.colInputQuantity = new System.Windows.Forms.ColumnHeader();
            this.lblPowerUsage = new System.Windows.Forms.Label();
            this.txtPowerUsage = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpProductionItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTargetQuantity)).BeginInit();
            this.grpInputMethod.SuspendLayout();
            this.grpRecipeInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(145, 20);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Production Item";
            // 
            // grpProductionItem
            // 
            this.grpProductionItem.Controls.Add(this.txtItemName);
            this.grpProductionItem.Controls.Add(this.lblItemName);
            this.grpProductionItem.Controls.Add(this.txtTargetQuantity);
            this.grpProductionItem.Controls.Add(this.lblTargetQuantity);
            this.grpProductionItem.Location = new System.Drawing.Point(12, 40);
            this.grpProductionItem.Name = "grpProductionItem";
            this.grpProductionItem.Size = new System.Drawing.Size(460, 80);
            this.grpProductionItem.TabIndex = 1;
            this.grpProductionItem.TabStop = false;
            this.grpProductionItem.Text = "Item Details";
            // 
            // txtItemName
            // 
            this.txtItemName.Location = new System.Drawing.Point(100, 25);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(150, 23);
            this.txtItemName.TabIndex = 1;
            this.txtItemName.TextChanged += new System.EventHandler(this.txtItemName_TextChanged);
            // 
            // lblItemName
            // 
            this.lblItemName.AutoSize = true;
            this.lblItemName.Location = new System.Drawing.Point(15, 28);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(68, 15);
            this.lblItemName.TabIndex = 0;
            this.lblItemName.Text = "Item Name:";
            // 
            // txtTargetQuantity
            // 
            this.txtTargetQuantity.Location = new System.Drawing.Point(350, 25);
            this.txtTargetQuantity.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.txtTargetQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtTargetQuantity.Name = "txtTargetQuantity";
            this.txtTargetQuantity.Size = new System.Drawing.Size(100, 23);
            this.txtTargetQuantity.TabIndex = 3;
            this.txtTargetQuantity.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.txtTargetQuantity.ValueChanged += new System.EventHandler(this.txtTargetQuantity_ValueChanged);
            // 
            // lblTargetQuantity
            // 
            this.lblTargetQuantity.AutoSize = true;
            this.lblTargetQuantity.Location = new System.Drawing.Point(270, 28);
            this.lblTargetQuantity.Name = "lblTargetQuantity";
            this.lblTargetQuantity.Size = new System.Drawing.Size(56, 15);
            this.lblTargetQuantity.TabIndex = 2;
            this.lblTargetQuantity.Text = "Quantity:";
            // 
            // grpInputMethod
            // 
            this.grpInputMethod.Controls.Add(this.rbImportInputs);
            this.grpInputMethod.Controls.Add(this.rbProduceOnsite);
            this.grpInputMethod.Controls.Add(this.chkAutoDependencies);
            this.grpInputMethod.Location = new System.Drawing.Point(12, 130);
            this.grpInputMethod.Name = "grpInputMethod";
            this.grpInputMethod.Size = new System.Drawing.Size(460, 85);
            this.grpInputMethod.TabIndex = 2;
            this.grpInputMethod.TabStop = false;
            this.grpInputMethod.Text = "Input Method";
            // 
            // rbImportInputs
            // 
            this.rbImportInputs.AutoSize = true;
            this.rbImportInputs.Checked = true;
            this.rbImportInputs.Location = new System.Drawing.Point(15, 25);
            this.rbImportInputs.Name = "rbImportInputs";
            this.rbImportInputs.Size = new System.Drawing.Size(95, 19);
            this.rbImportInputs.TabIndex = 0;
            this.rbImportInputs.TabStop = true;
            this.rbImportInputs.Text = "Import Inputs";
            this.rbImportInputs.UseVisualStyleBackColor = true;
            this.rbImportInputs.CheckedChanged += new System.EventHandler(this.rbImportInputs_CheckedChanged);
            // 
            // rbProduceOnsite
            // 
            this.rbProduceOnsite.AutoSize = true;
            this.rbProduceOnsite.Location = new System.Drawing.Point(130, 25);
            this.rbProduceOnsite.Name = "rbProduceOnsite";
            this.rbProduceOnsite.Size = new System.Drawing.Size(108, 19);
            this.rbProduceOnsite.TabIndex = 1;
            this.rbProduceOnsite.Text = "Produce Onsite";
            this.rbProduceOnsite.UseVisualStyleBackColor = true;
            this.rbProduceOnsite.CheckedChanged += new System.EventHandler(this.rbProduceOnsite_CheckedChanged);
            // 
            // chkAutoDependencies
            // 
            this.chkAutoDependencies.AutoSize = true;
            this.chkAutoDependencies.Enabled = false;
            this.chkAutoDependencies.Location = new System.Drawing.Point(15, 50);
            this.chkAutoDependencies.Name = "chkAutoDependencies";
            this.chkAutoDependencies.Size = new System.Drawing.Size(280, 19);
            this.chkAutoDependencies.TabIndex = 2;
            this.chkAutoDependencies.Text = "Auto-populate production dependencies recursively";
            this.chkAutoDependencies.UseVisualStyleBackColor = true;
            // 
            // grpRecipeInfo
            // 
            this.grpRecipeInfo.Controls.Add(this.lblRecipe);
            this.grpRecipeInfo.Controls.Add(this.cmbRecipe);
            this.grpRecipeInfo.Controls.Add(this.lblBuildings);
            this.grpRecipeInfo.Controls.Add(this.txtBuildingsRequired);
            this.grpRecipeInfo.Controls.Add(this.lblInputsNeeded);
            this.grpRecipeInfo.Controls.Add(this.lstInputsNeeded);
            this.grpRecipeInfo.Controls.Add(this.lblPowerUsage);
            this.grpRecipeInfo.Controls.Add(this.txtPowerUsage);
            this.grpRecipeInfo.Location = new System.Drawing.Point(12, 225);
            this.grpRecipeInfo.Name = "grpRecipeInfo";
            this.grpRecipeInfo.Size = new System.Drawing.Size(460, 250);
            this.grpRecipeInfo.TabIndex = 3;
            this.grpRecipeInfo.TabStop = false;
            this.grpRecipeInfo.Text = "Recipe Information";
            // 
            // lblRecipe
            // 
            this.lblRecipe.AutoSize = true;
            this.lblRecipe.Location = new System.Drawing.Point(15, 25);
            this.lblRecipe.Name = "lblRecipe";
            this.lblRecipe.Size = new System.Drawing.Size(44, 15);
            this.lblRecipe.TabIndex = 0;
            this.lblRecipe.Text = "Recipe:";
            // 
            // cmbRecipe
            // 
            this.cmbRecipe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRecipe.FormattingEnabled = true;
            this.cmbRecipe.Location = new System.Drawing.Point(70, 22);
            this.cmbRecipe.Name = "cmbRecipe";
            this.cmbRecipe.Size = new System.Drawing.Size(200, 23);
            this.cmbRecipe.TabIndex = 1;
            this.cmbRecipe.SelectedIndexChanged += new System.EventHandler(this.cmbRecipe_SelectedIndexChanged);
            // 
            // lblBuildings
            // 
            this.lblBuildings.AutoSize = true;
            this.lblBuildings.Location = new System.Drawing.Point(15, 55);
            this.lblBuildings.Name = "lblBuildings";
            this.lblBuildings.Size = new System.Drawing.Size(124, 15);
            this.lblBuildings.TabIndex = 2;
            this.lblBuildings.Text = "Buildings Required:";
            // 
            // txtBuildingsRequired
            // 
            this.txtBuildingsRequired.Location = new System.Drawing.Point(150, 52);
            this.txtBuildingsRequired.Name = "txtBuildingsRequired";
            this.txtBuildingsRequired.ReadOnly = true;
            this.txtBuildingsRequired.Size = new System.Drawing.Size(100, 23);
            this.txtBuildingsRequired.TabIndex = 3;
            // 
            // lblInputsNeeded
            // 
            this.lblInputsNeeded.AutoSize = true;
            this.lblInputsNeeded.Location = new System.Drawing.Point(15, 85);
            this.lblInputsNeeded.Name = "lblInputsNeeded";
            this.lblInputsNeeded.Size = new System.Drawing.Size(89, 15);
            this.lblInputsNeeded.TabIndex = 4;
            this.lblInputsNeeded.Text = "Inputs Needed:";
            // 
            // lstInputsNeeded
            // 
            this.lstInputsNeeded.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colInputItem,
            this.colInputQuantity});
            this.lstInputsNeeded.FullRowSelect = true;
            this.lstInputsNeeded.GridLines = true;
            this.lstInputsNeeded.Location = new System.Drawing.Point(15, 105);
            this.lstInputsNeeded.Name = "lstInputsNeeded";
            this.lstInputsNeeded.Size = new System.Drawing.Size(430, 100);
            this.lstInputsNeeded.TabIndex = 5;
            this.lstInputsNeeded.UseCompatibleStateImageBehavior = false;
            this.lstInputsNeeded.View = System.Windows.Forms.View.Details;
            // 
            // colInputItem
            // 
            this.colInputItem.Text = "Item";
            this.colInputItem.Width = 200;
            // 
            // colInputQuantity
            // 
            this.colInputQuantity.Text = "Quantity per Minute";
            this.colInputQuantity.Width = 150;
            // 
            // lblPowerUsage
            // 
            this.lblPowerUsage.AutoSize = true;
            this.lblPowerUsage.Location = new System.Drawing.Point(15, 220);
            this.lblPowerUsage.Name = "lblPowerUsage";
            this.lblPowerUsage.Size = new System.Drawing.Size(84, 15);
            this.lblPowerUsage.TabIndex = 6;
            this.lblPowerUsage.Text = "Power Usage:";
            // 
            // txtPowerUsage
            // 
            this.txtPowerUsage.Location = new System.Drawing.Point(110, 217);
            this.txtPowerUsage.Name = "txtPowerUsage";
            this.txtPowerUsage.ReadOnly = true;
            this.txtPowerUsage.Size = new System.Drawing.Size(100, 23);
            this.txtPowerUsage.TabIndex = 7;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(300, 495);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 30);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(390, 495);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // ProductionItemForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(484, 537);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grpRecipeInfo);
            this.Controls.Add(this.grpInputMethod);
            this.Controls.Add(this.grpProductionItem);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProductionItemForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add/Edit Production Item";
            this.grpProductionItem.ResumeLayout(false);
            this.grpProductionItem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTargetQuantity)).EndInit();
            this.grpInputMethod.ResumeLayout(false);
            this.grpInputMethod.PerformLayout();
            this.grpRecipeInfo.ResumeLayout(false);
            this.grpRecipeInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpProductionItem;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.NumericUpDown txtTargetQuantity;
        private System.Windows.Forms.Label lblTargetQuantity;
        private System.Windows.Forms.GroupBox grpInputMethod;
        private System.Windows.Forms.RadioButton rbImportInputs;
        private System.Windows.Forms.RadioButton rbProduceOnsite;
        private System.Windows.Forms.CheckBox chkAutoDependencies;
        private System.Windows.Forms.GroupBox grpRecipeInfo;
        private System.Windows.Forms.Label lblRecipe;
        private System.Windows.Forms.ComboBox cmbRecipe;
        private System.Windows.Forms.Label lblBuildings;
        private System.Windows.Forms.TextBox txtBuildingsRequired;
        private System.Windows.Forms.Label lblInputsNeeded;
        private System.Windows.Forms.ListView lstInputsNeeded;
        private System.Windows.Forms.ColumnHeader colInputItem;
        private System.Windows.Forms.ColumnHeader colInputQuantity;
        private System.Windows.Forms.Label lblPowerUsage;
        private System.Windows.Forms.TextBox txtPowerUsage;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}