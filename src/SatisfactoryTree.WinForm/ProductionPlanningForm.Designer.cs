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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.pnlLeftMenu = new System.Windows.Forms.Panel();
            this.treeFactories = new System.Windows.Forms.TreeView();
            this.grpFactoryActions = new System.Windows.Forms.GroupBox();
            this.btnAddFactory = new System.Windows.Forms.Button();
            this.txtFactoryName = new System.Windows.Forms.TextBox();
            this.lblFactoryName = new System.Windows.Forms.Label();
            this.pnlRightDetails = new System.Windows.Forms.Panel();
            this.treeFactoryDetails = new System.Windows.Forms.TreeView();
            this.grpProductionActions = new System.Windows.Forms.GroupBox();
            this.btnAddProductionItem = new System.Windows.Forms.Button();
            this.btnEditProductionItem = new System.Windows.Forms.Button();
            this.btnProcessProduction = new System.Windows.Forms.Button();
            this.txtProductionItem = new System.Windows.Forms.TextBox();
            this.lblProductionItem = new System.Windows.Forms.Label();
            this.txtProductionQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblProductionQuantity = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.pnlLeftMenu.SuspendLayout();
            this.grpFactoryActions.SuspendLayout();
            this.pnlRightDetails.SuspendLayout();
            this.grpProductionActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductionQuantity)).BeginInit();
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
            this.lblTitle.Text = "Factory Management";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 40);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Panel1.Controls.Add(this.pnlLeftMenu);
            this.splitContainer.Panel2.Controls.Add(this.pnlRightDetails);
            this.splitContainer.Size = new System.Drawing.Size(1200, 600);
            this.splitContainer.SplitterDistance = 300;
            this.splitContainer.TabIndex = 1;
            // 
            // pnlLeftMenu
            // 
            this.pnlLeftMenu.Controls.Add(this.treeFactories);
            this.pnlLeftMenu.Controls.Add(this.grpFactoryActions);
            this.pnlLeftMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeftMenu.Location = new System.Drawing.Point(0, 0);
            this.pnlLeftMenu.Name = "pnlLeftMenu";
            this.pnlLeftMenu.Size = new System.Drawing.Size(300, 600);
            this.pnlLeftMenu.TabIndex = 0;
            // 
            // treeFactories
            // 
            this.treeFactories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeFactories.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.treeFactories.Location = new System.Drawing.Point(0, 0);
            this.treeFactories.Name = "treeFactories";
            this.treeFactories.Size = new System.Drawing.Size(300, 490);
            this.treeFactories.TabIndex = 0;
            this.treeFactories.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeFactories_AfterSelect);
            // 
            // grpFactoryActions
            // 
            this.grpFactoryActions.Controls.Add(this.btnAddFactory);
            this.grpFactoryActions.Controls.Add(this.txtFactoryName);
            this.grpFactoryActions.Controls.Add(this.lblFactoryName);
            this.grpFactoryActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpFactoryActions.Location = new System.Drawing.Point(0, 490);
            this.grpFactoryActions.Name = "grpFactoryActions";
            this.grpFactoryActions.Size = new System.Drawing.Size(300, 110);
            this.grpFactoryActions.TabIndex = 1;
            this.grpFactoryActions.TabStop = false;
            this.grpFactoryActions.Text = "Factory Actions";
            // 
            // btnAddFactory
            // 
            this.btnAddFactory.Location = new System.Drawing.Point(200, 70);
            this.btnAddFactory.Name = "btnAddFactory";
            this.btnAddFactory.Size = new System.Drawing.Size(94, 23);
            this.btnAddFactory.TabIndex = 3;
            this.btnAddFactory.Text = "Add Factory";
            this.btnAddFactory.UseVisualStyleBackColor = true;
            this.btnAddFactory.Click += new System.EventHandler(this.btnAddFactory_Click);
            // 
            // txtFactoryName
            // 
            this.txtFactoryName.Location = new System.Drawing.Point(6, 70);
            this.txtFactoryName.Name = "txtFactoryName";
            this.txtFactoryName.Size = new System.Drawing.Size(188, 23);
            this.txtFactoryName.TabIndex = 2;
            // 
            // lblFactoryName
            // 
            this.lblFactoryName.AutoSize = true;
            this.lblFactoryName.Location = new System.Drawing.Point(6, 50);
            this.lblFactoryName.Name = "lblFactoryName";
            this.lblFactoryName.Size = new System.Drawing.Size(88, 15);
            this.lblFactoryName.TabIndex = 1;
            this.lblFactoryName.Text = "Factory Name:";
            // 
            // pnlRightDetails
            // 
            this.pnlRightDetails.Controls.Add(this.treeFactoryDetails);
            this.pnlRightDetails.Controls.Add(this.grpProductionActions);
            this.pnlRightDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRightDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlRightDetails.Name = "pnlRightDetails";
            this.pnlRightDetails.Size = new System.Drawing.Size(896, 600);
            this.pnlRightDetails.TabIndex = 0;
            // 
            // treeFactoryDetails
            // 
            this.treeFactoryDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeFactoryDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.treeFactoryDetails.Location = new System.Drawing.Point(0, 0);
            this.treeFactoryDetails.Name = "treeFactoryDetails";
            this.treeFactoryDetails.Size = new System.Drawing.Size(896, 470);
            this.treeFactoryDetails.TabIndex = 0;
            this.treeFactoryDetails.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeFactoryDetails_AfterSelect);
            // 
            // grpProductionActions
            // 
            this.grpProductionActions.Controls.Add(this.btnAddProductionItem);
            this.grpProductionActions.Controls.Add(this.btnEditProductionItem);
            this.grpProductionActions.Controls.Add(this.btnProcessProduction);
            this.grpProductionActions.Controls.Add(this.txtProductionItem);
            this.grpProductionActions.Controls.Add(this.lblProductionItem);
            this.grpProductionActions.Controls.Add(this.txtProductionQuantity);
            this.grpProductionActions.Controls.Add(this.lblProductionQuantity);
            this.grpProductionActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpProductionActions.Location = new System.Drawing.Point(0, 470);
            this.grpProductionActions.Name = "grpProductionActions";
            this.grpProductionActions.Size = new System.Drawing.Size(896, 130);
            this.grpProductionActions.TabIndex = 1;
            this.grpProductionActions.TabStop = false;
            this.grpProductionActions.Text = "Production Actions";
            // 
            // btnAddProductionItem
            // 
            this.btnAddProductionItem.Location = new System.Drawing.Point(15, 80);
            this.btnAddProductionItem.Name = "btnAddProductionItem";
            this.btnAddProductionItem.Size = new System.Drawing.Size(120, 30);
            this.btnAddProductionItem.TabIndex = 4;
            this.btnAddProductionItem.Text = "Add Production Item";
            this.btnAddProductionItem.UseVisualStyleBackColor = true;
            this.btnAddProductionItem.Click += new System.EventHandler(this.btnAddProductionItem_Click);
            // 
            // btnEditProductionItem
            // 
            this.btnEditProductionItem.Location = new System.Drawing.Point(150, 80);
            this.btnEditProductionItem.Name = "btnEditProductionItem";
            this.btnEditProductionItem.Size = new System.Drawing.Size(120, 30);
            this.btnEditProductionItem.TabIndex = 5;
            this.btnEditProductionItem.Text = "Edit Production Item";
            this.btnEditProductionItem.UseVisualStyleBackColor = true;
            this.btnEditProductionItem.Click += new System.EventHandler(this.btnEditProductionItem_Click);
            // 
            // btnProcessProduction
            // 
            this.btnProcessProduction.Location = new System.Drawing.Point(285, 80);
            this.btnProcessProduction.Name = "btnProcessProduction";
            this.btnProcessProduction.Size = new System.Drawing.Size(120, 30);
            this.btnProcessProduction.TabIndex = 6;
            this.btnProcessProduction.Text = "Process Production";
            this.btnProcessProduction.UseVisualStyleBackColor = true;
            this.btnProcessProduction.Click += new System.EventHandler(this.btnProcessProduction_Click);
            // 
            // txtProductionItem
            // 
            this.txtProductionItem.Location = new System.Drawing.Point(15, 40);
            this.txtProductionItem.Name = "txtProductionItem";
            this.txtProductionItem.Size = new System.Drawing.Size(150, 23);
            this.txtProductionItem.TabIndex = 1;
            // 
            // lblProductionItem
            // 
            this.lblProductionItem.AutoSize = true;
            this.lblProductionItem.Location = new System.Drawing.Point(15, 22);
            this.lblProductionItem.Name = "lblProductionItem";
            this.lblProductionItem.Size = new System.Drawing.Size(68, 15);
            this.lblProductionItem.TabIndex = 0;
            this.lblProductionItem.Text = "Item Name:";
            // 
            // txtProductionQuantity
            // 
            this.txtProductionQuantity.Location = new System.Drawing.Point(200, 40);
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
            this.txtProductionQuantity.Size = new System.Drawing.Size(100, 23);
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
            // ProductionPlanningForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 640);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "ProductionPlanningForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Satisfactory Factory Management";
            this.Load += new System.EventHandler(this.ProductionPlanningForm_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.pnlLeftMenu.ResumeLayout(false);
            this.grpFactoryActions.ResumeLayout(false);
            this.grpFactoryActions.PerformLayout();
            this.pnlRightDetails.ResumeLayout(false);
            this.grpProductionActions.ResumeLayout(false);
            this.grpProductionActions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductionQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel pnlLeftMenu;
        private System.Windows.Forms.TreeView treeFactories;
        private System.Windows.Forms.GroupBox grpFactoryActions;
        private System.Windows.Forms.Button btnAddFactory;
        private System.Windows.Forms.TextBox txtFactoryName;
        private System.Windows.Forms.Label lblFactoryName;
        private System.Windows.Forms.Panel pnlRightDetails;
        private System.Windows.Forms.TreeView treeFactoryDetails;
        private System.Windows.Forms.GroupBox grpProductionActions;
        private System.Windows.Forms.Button btnAddProductionItem;
        private System.Windows.Forms.Button btnEditProductionItem;
        private System.Windows.Forms.Button btnProcessProduction;
        private System.Windows.Forms.TextBox txtProductionItem;
        private System.Windows.Forms.Label lblProductionItem;
        private System.Windows.Forms.NumericUpDown txtProductionQuantity;
        private System.Windows.Forms.Label lblProductionQuantity;
    }
}