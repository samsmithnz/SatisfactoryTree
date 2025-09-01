namespace SatisfactoryTree.WinForm
{
    partial class ProductionPlanningForm
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
            this.grpFactories = new System.Windows.Forms.GroupBox();
            this.cmbFactories = new System.Windows.Forms.ComboBox();
            this.btnAddFactory = new System.Windows.Forms.Button();
            this.txtFactoryName = new System.Windows.Forms.TextBox();
            this.lblFactoryName = new System.Windows.Forms.Label();
            this.grpProductionGoals = new System.Windows.Forms.GroupBox();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.lblItemName = new System.Windows.Forms.Label();
            this.txtTargetQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblTargetQuantity = new System.Windows.Forms.Label();
            this.btnAddGoal = new System.Windows.Forms.Button();
            this.lstActiveGoals = new System.Windows.Forms.ListView();
            this.colItemName = new System.Windows.Forms.ColumnHeader();
            this.colTargetQuantity = new System.Windows.Forms.ColumnHeader();
            this.colCurrentQuantity = new System.Windows.Forms.ColumnHeader();
            this.colProgress = new System.Windows.Forms.ColumnHeader();
            this.colFactory = new System.Windows.Forms.ColumnHeader();
            this.grpProduction = new System.Windows.Forms.GroupBox();
            this.txtProductionItem = new System.Windows.Forms.TextBox();
            this.lblProductionItem = new System.Windows.Forms.Label();
            this.txtProductionQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblProductionQuantity = new System.Windows.Forms.Label();
            this.cmbProductionFactory = new System.Windows.Forms.ComboBox();
            this.lblProductionFactory = new System.Windows.Forms.Label();
            this.btnProcessProduction = new System.Windows.Forms.Button();
            this.grpStorage = new System.Windows.Forms.GroupBox();
            this.lstStorage = new System.Windows.Forms.ListView();
            this.colStorageItem = new System.Windows.Forms.ColumnHeader();
            this.colStorageQuantity = new System.Windows.Forms.ColumnHeader();
            this.colStorageFactory = new System.Windows.Forms.ColumnHeader();
            this.lstCompletedGoals = new System.Windows.Forms.ListView();
            this.colCompletedItem = new System.Windows.Forms.ColumnHeader();
            this.colCompletedQuantity = new System.Windows.Forms.ColumnHeader();
            this.colCompletedDate = new System.Windows.Forms.ColumnHeader();
            this.colCompletedFactory = new System.Windows.Forms.ColumnHeader();
            this.lblCompletedGoals = new System.Windows.Forms.Label();
            this.grpFactories.SuspendLayout();
            this.grpProductionGoals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTargetQuantity)).BeginInit();
            this.grpProduction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductionQuantity)).BeginInit();
            this.grpStorage.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(267, 26);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Production Planning";
            // 
            // grpFactories
            // 
            this.grpFactories.Controls.Add(this.cmbFactories);
            this.grpFactories.Controls.Add(this.btnAddFactory);
            this.grpFactories.Controls.Add(this.txtFactoryName);
            this.grpFactories.Controls.Add(this.lblFactoryName);
            this.grpFactories.Location = new System.Drawing.Point(12, 50);
            this.grpFactories.Name = "grpFactories";
            this.grpFactories.Size = new System.Drawing.Size(350, 100);
            this.grpFactories.TabIndex = 1;
            this.grpFactories.TabStop = false;
            this.grpFactories.Text = "Factories";
            // 
            // cmbFactories
            // 
            this.cmbFactories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFactories.FormattingEnabled = true;
            this.cmbFactories.Location = new System.Drawing.Point(6, 19);
            this.cmbFactories.Name = "cmbFactories";
            this.cmbFactories.Size = new System.Drawing.Size(200, 23);
            this.cmbFactories.TabIndex = 0;
            this.cmbFactories.SelectedIndexChanged += new System.EventHandler(this.cmbFactories_SelectedIndexChanged);
            // 
            // btnAddFactory
            // 
            this.btnAddFactory.Location = new System.Drawing.Point(250, 70);
            this.btnAddFactory.Name = "btnAddFactory";
            this.btnAddFactory.Size = new System.Drawing.Size(94, 23);
            this.btnAddFactory.TabIndex = 3;
            this.btnAddFactory.Text = "Add Factory";
            this.btnAddFactory.UseVisualStyleBackColor = true;
            this.btnAddFactory.Click += new System.EventHandler(this.btnAddFactory_Click);
            // 
            // txtFactoryName
            // 
            this.txtFactoryName.Location = new System.Drawing.Point(100, 70);
            this.txtFactoryName.Name = "txtFactoryName";
            this.txtFactoryName.Size = new System.Drawing.Size(144, 23);
            this.txtFactoryName.TabIndex = 2;
            // 
            // lblFactoryName
            // 
            this.lblFactoryName.AutoSize = true;
            this.lblFactoryName.Location = new System.Drawing.Point(6, 73);
            this.lblFactoryName.Name = "lblFactoryName";
            this.lblFactoryName.Size = new System.Drawing.Size(88, 15);
            this.lblFactoryName.TabIndex = 1;
            this.lblFactoryName.Text = "Factory Name:";
            // 
            // grpProductionGoals
            // 
            this.grpProductionGoals.Controls.Add(this.txtItemName);
            this.grpProductionGoals.Controls.Add(this.lblItemName);
            this.grpProductionGoals.Controls.Add(this.txtTargetQuantity);
            this.grpProductionGoals.Controls.Add(this.lblTargetQuantity);
            this.grpProductionGoals.Controls.Add(this.btnAddGoal);
            this.grpProductionGoals.Controls.Add(this.lstActiveGoals);
            this.grpProductionGoals.Location = new System.Drawing.Point(12, 160);
            this.grpProductionGoals.Name = "grpProductionGoals";
            this.grpProductionGoals.Size = new System.Drawing.Size(600, 250);
            this.grpProductionGoals.TabIndex = 2;
            this.grpProductionGoals.TabStop = false;
            this.grpProductionGoals.Text = "Production Goals";
            // 
            // txtItemName
            // 
            this.txtItemName.Location = new System.Drawing.Point(80, 19);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(150, 23);
            this.txtItemName.TabIndex = 1;
            // 
            // lblItemName
            // 
            this.lblItemName.AutoSize = true;
            this.lblItemName.Location = new System.Drawing.Point(6, 22);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(68, 15);
            this.lblItemName.TabIndex = 0;
            this.lblItemName.Text = "Item Name:";
            // 
            // txtTargetQuantity
            // 
            this.txtTargetQuantity.Location = new System.Drawing.Point(350, 19);
            this.txtTargetQuantity.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.txtTargetQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtTargetQuantity.Name = "txtTargetQuantity";
            this.txtTargetQuantity.Size = new System.Drawing.Size(120, 23);
            this.txtTargetQuantity.TabIndex = 3;
            this.txtTargetQuantity.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lblTargetQuantity
            // 
            this.lblTargetQuantity.AutoSize = true;
            this.lblTargetQuantity.Location = new System.Drawing.Point(250, 22);
            this.lblTargetQuantity.Name = "lblTargetQuantity";
            this.lblTargetQuantity.Size = new System.Drawing.Size(94, 15);
            this.lblTargetQuantity.TabIndex = 2;
            this.lblTargetQuantity.Text = "Target Quantity:";
            // 
            // btnAddGoal
            // 
            this.btnAddGoal.Location = new System.Drawing.Point(500, 19);
            this.btnAddGoal.Name = "btnAddGoal";
            this.btnAddGoal.Size = new System.Drawing.Size(75, 23);
            this.btnAddGoal.TabIndex = 4;
            this.btnAddGoal.Text = "Add Goal";
            this.btnAddGoal.UseVisualStyleBackColor = true;
            this.btnAddGoal.Click += new System.EventHandler(this.btnAddGoal_Click);
            // 
            // lstActiveGoals
            // 
            this.lstActiveGoals.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colItemName,
            this.colTargetQuantity,
            this.colCurrentQuantity,
            this.colProgress,
            this.colFactory});
            this.lstActiveGoals.FullRowSelect = true;
            this.lstActiveGoals.GridLines = true;
            this.lstActiveGoals.Location = new System.Drawing.Point(6, 50);
            this.lstActiveGoals.Name = "lstActiveGoals";
            this.lstActiveGoals.Size = new System.Drawing.Size(588, 194);
            this.lstActiveGoals.TabIndex = 5;
            this.lstActiveGoals.UseCompatibleStateImageBehavior = false;
            this.lstActiveGoals.View = System.Windows.Forms.View.Details;
            // 
            // colItemName
            // 
            this.colItemName.Text = "Item";
            this.colItemName.Width = 120;
            // 
            // colTargetQuantity
            // 
            this.colTargetQuantity.Text = "Target";
            this.colTargetQuantity.Width = 80;
            // 
            // colCurrentQuantity
            // 
            this.colCurrentQuantity.Text = "Current";
            this.colCurrentQuantity.Width = 80;
            // 
            // colProgress
            // 
            this.colProgress.Text = "Progress %";
            this.colProgress.Width = 80;
            // 
            // colFactory
            // 
            this.colFactory.Text = "Factory";
            this.colFactory.Width = 120;
            // 
            // grpProduction
            // 
            this.grpProduction.Controls.Add(this.txtProductionItem);
            this.grpProduction.Controls.Add(this.lblProductionItem);
            this.grpProduction.Controls.Add(this.txtProductionQuantity);
            this.grpProduction.Controls.Add(this.lblProductionQuantity);
            this.grpProduction.Controls.Add(this.cmbProductionFactory);
            this.grpProduction.Controls.Add(this.lblProductionFactory);
            this.grpProduction.Controls.Add(this.btnProcessProduction);
            this.grpProduction.Location = new System.Drawing.Point(380, 50);
            this.grpProduction.Name = "grpProduction";
            this.grpProduction.Size = new System.Drawing.Size(350, 100);
            this.grpProduction.TabIndex = 3;
            this.grpProduction.TabStop = false;
            this.grpProduction.Text = "Process Production";
            // 
            // txtProductionItem
            // 
            this.txtProductionItem.Location = new System.Drawing.Point(60, 19);
            this.txtProductionItem.Name = "txtProductionItem";
            this.txtProductionItem.Size = new System.Drawing.Size(120, 23);
            this.txtProductionItem.TabIndex = 1;
            // 
            // lblProductionItem
            // 
            this.lblProductionItem.AutoSize = true;
            this.lblProductionItem.Location = new System.Drawing.Point(6, 22);
            this.lblProductionItem.Name = "lblProductionItem";
            this.lblProductionItem.Size = new System.Drawing.Size(34, 15);
            this.lblProductionItem.TabIndex = 0;
            this.lblProductionItem.Text = "Item:";
            // 
            // txtProductionQuantity
            // 
            this.txtProductionQuantity.Location = new System.Drawing.Point(270, 19);
            this.txtProductionQuantity.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.txtProductionQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtProductionQuantity.Name = "txtProductionQuantity";
            this.txtProductionQuantity.Size = new System.Drawing.Size(74, 23);
            this.txtProductionQuantity.TabIndex = 3;
            this.txtProductionQuantity.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lblProductionQuantity
            // 
            this.lblProductionQuantity.AutoSize = true;
            this.lblProductionQuantity.Location = new System.Drawing.Point(200, 22);
            this.lblProductionQuantity.Name = "lblProductionQuantity";
            this.lblProductionQuantity.Size = new System.Drawing.Size(56, 15);
            this.lblProductionQuantity.TabIndex = 2;
            this.lblProductionQuantity.Text = "Quantity:";
            // 
            // cmbProductionFactory
            // 
            this.cmbProductionFactory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProductionFactory.FormattingEnabled = true;
            this.cmbProductionFactory.Location = new System.Drawing.Point(60, 48);
            this.cmbProductionFactory.Name = "cmbProductionFactory";
            this.cmbProductionFactory.Size = new System.Drawing.Size(200, 23);
            this.cmbProductionFactory.TabIndex = 5;
            // 
            // lblProductionFactory
            // 
            this.lblProductionFactory.AutoSize = true;
            this.lblProductionFactory.Location = new System.Drawing.Point(6, 51);
            this.lblProductionFactory.Name = "lblProductionFactory";
            this.lblProductionFactory.Size = new System.Drawing.Size(48, 15);
            this.lblProductionFactory.TabIndex = 4;
            this.lblProductionFactory.Text = "Factory:";
            // 
            // btnProcessProduction
            // 
            this.btnProcessProduction.Location = new System.Drawing.Point(270, 48);
            this.btnProcessProduction.Name = "btnProcessProduction";
            this.btnProcessProduction.Size = new System.Drawing.Size(74, 23);
            this.btnProcessProduction.TabIndex = 6;
            this.btnProcessProduction.Text = "Process";
            this.btnProcessProduction.UseVisualStyleBackColor = true;
            this.btnProcessProduction.Click += new System.EventHandler(this.btnProcessProduction_Click);
            // 
            // grpStorage
            // 
            this.grpStorage.Controls.Add(this.lstStorage);
            this.grpStorage.Location = new System.Drawing.Point(630, 160);
            this.grpStorage.Name = "grpStorage";
            this.grpStorage.Size = new System.Drawing.Size(300, 250);
            this.grpStorage.TabIndex = 4;
            this.grpStorage.TabStop = false;
            this.grpStorage.Text = "Storage";
            // 
            // lstStorage
            // 
            this.lstStorage.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colStorageItem,
            this.colStorageQuantity,
            this.colStorageFactory});
            this.lstStorage.FullRowSelect = true;
            this.lstStorage.GridLines = true;
            this.lstStorage.Location = new System.Drawing.Point(6, 22);
            this.lstStorage.Name = "lstStorage";
            this.lstStorage.Size = new System.Drawing.Size(288, 222);
            this.lstStorage.TabIndex = 0;
            this.lstStorage.UseCompatibleStateImageBehavior = false;
            this.lstStorage.View = System.Windows.Forms.View.Details;
            // 
            // colStorageItem
            // 
            this.colStorageItem.Text = "Item";
            this.colStorageItem.Width = 120;
            // 
            // colStorageQuantity
            // 
            this.colStorageQuantity.Text = "Quantity";
            this.colStorageQuantity.Width = 80;
            // 
            // colStorageFactory
            // 
            this.colStorageFactory.Text = "Factory";
            this.colStorageFactory.Width = 80;
            // 
            // lstCompletedGoals
            // 
            this.lstCompletedGoals.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCompletedItem,
            this.colCompletedQuantity,
            this.colCompletedDate,
            this.colCompletedFactory});
            this.lstCompletedGoals.FullRowSelect = true;
            this.lstCompletedGoals.GridLines = true;
            this.lstCompletedGoals.Location = new System.Drawing.Point(12, 440);
            this.lstCompletedGoals.Name = "lstCompletedGoals";
            this.lstCompletedGoals.Size = new System.Drawing.Size(600, 150);
            this.lstCompletedGoals.TabIndex = 5;
            this.lstCompletedGoals.UseCompatibleStateImageBehavior = false;
            this.lstCompletedGoals.View = System.Windows.Forms.View.Details;
            // 
            // colCompletedItem
            // 
            this.colCompletedItem.Text = "Item";
            this.colCompletedItem.Width = 120;
            // 
            // colCompletedQuantity
            // 
            this.colCompletedQuantity.Text = "Quantity";
            this.colCompletedQuantity.Width = 80;
            // 
            // colCompletedDate
            // 
            this.colCompletedDate.Text = "Completed";
            this.colCompletedDate.Width = 120;
            // 
            // colCompletedFactory
            // 
            this.colCompletedFactory.Text = "Factory";
            this.colCompletedFactory.Width = 120;
            // 
            // lblCompletedGoals
            // 
            this.lblCompletedGoals.AutoSize = true;
            this.lblCompletedGoals.Location = new System.Drawing.Point(12, 422);
            this.lblCompletedGoals.Name = "lblCompletedGoals";
            this.lblCompletedGoals.Size = new System.Drawing.Size(100, 15);
            this.lblCompletedGoals.TabIndex = 6;
            this.lblCompletedGoals.Text = "Completed Goals:";
            // 
            // ProductionPlanningForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 610);
            this.Controls.Add(this.lblCompletedGoals);
            this.Controls.Add(this.lstCompletedGoals);
            this.Controls.Add(this.grpStorage);
            this.Controls.Add(this.grpProduction);
            this.Controls.Add(this.grpProductionGoals);
            this.Controls.Add(this.grpFactories);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ProductionPlanningForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Satisfactory Production Planning";
            this.Load += new System.EventHandler(this.ProductionPlanningForm_Load);
            this.grpFactories.ResumeLayout(false);
            this.grpFactories.PerformLayout();
            this.grpProductionGoals.ResumeLayout(false);
            this.grpProductionGoals.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTargetQuantity)).EndInit();
            this.grpProduction.ResumeLayout(false);
            this.grpProduction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductionQuantity)).EndInit();
            this.grpStorage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpFactories;
        private System.Windows.Forms.ComboBox cmbFactories;
        private System.Windows.Forms.Button btnAddFactory;
        private System.Windows.Forms.TextBox txtFactoryName;
        private System.Windows.Forms.Label lblFactoryName;
        private System.Windows.Forms.GroupBox grpProductionGoals;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.NumericUpDown txtTargetQuantity;
        private System.Windows.Forms.Label lblTargetQuantity;
        private System.Windows.Forms.Button btnAddGoal;
        private System.Windows.Forms.ListView lstActiveGoals;
        private System.Windows.Forms.ColumnHeader colItemName;
        private System.Windows.Forms.ColumnHeader colTargetQuantity;
        private System.Windows.Forms.ColumnHeader colCurrentQuantity;
        private System.Windows.Forms.ColumnHeader colProgress;
        private System.Windows.Forms.ColumnHeader colFactory;
        private System.Windows.Forms.GroupBox grpProduction;
        private System.Windows.Forms.TextBox txtProductionItem;
        private System.Windows.Forms.Label lblProductionItem;
        private System.Windows.Forms.NumericUpDown txtProductionQuantity;
        private System.Windows.Forms.Label lblProductionQuantity;
        private System.Windows.Forms.ComboBox cmbProductionFactory;
        private System.Windows.Forms.Label lblProductionFactory;
        private System.Windows.Forms.Button btnProcessProduction;
        private System.Windows.Forms.GroupBox grpStorage;
        private System.Windows.Forms.ListView lstStorage;
        private System.Windows.Forms.ColumnHeader colStorageItem;
        private System.Windows.Forms.ColumnHeader colStorageQuantity;
        private System.Windows.Forms.ColumnHeader colStorageFactory;
        private System.Windows.Forms.ListView lstCompletedGoals;
        private System.Windows.Forms.ColumnHeader colCompletedItem;
        private System.Windows.Forms.ColumnHeader colCompletedQuantity;
        private System.Windows.Forms.ColumnHeader colCompletedDate;
        private System.Windows.Forms.ColumnHeader colCompletedFactory;
        private System.Windows.Forms.Label lblCompletedGoals;
    }
}