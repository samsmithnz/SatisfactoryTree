namespace SatisfactoryTree.WinForm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgProduction = new DataGridView();
            colPart = new DataGridViewComboBoxColumn();
            colRecipe = new DataGridViewComboBoxColumn();
            colQuantity = new DataGridViewTextBoxColumn();
            colDelete = new DataGridViewButtonColumn();
            lblParts = new Label();
            label1 = new Label();
            dataGridView1 = new DataGridView();
            dataGridViewComboBoxColumn2 = new DataGridViewComboBoxColumn();
            dataGridViewComboBoxColumn1 = new DataGridViewComboBoxColumn();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewButtonColumn1 = new DataGridViewButtonColumn();
            label2 = new Label();
            btnAddPart = new Button();
            button1 = new Button();
            dataGridView2 = new DataGridView();
            dataGridViewComboBoxColumn3 = new DataGridViewComboBoxColumn();
            dataGridViewComboBoxColumn4 = new DataGridViewComboBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewButtonColumn2 = new DataGridViewButtonColumn();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgProduction).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // dgProduction
            // 
            dgProduction.AllowUserToAddRows = false;
            dgProduction.AllowUserToDeleteRows = false;
            dgProduction.AllowUserToResizeColumns = false;
            dgProduction.AllowUserToResizeRows = false;
            dgProduction.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgProduction.Columns.AddRange(new DataGridViewColumn[] { colPart, colRecipe, colQuantity, colDelete });
            dgProduction.Location = new Point(12, 47);
            dgProduction.MultiSelect = false;
            dgProduction.Name = "dgProduction";
            dgProduction.RowHeadersWidth = 62;
            dgProduction.ScrollBars = ScrollBars.Vertical;
            dgProduction.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgProduction.Size = new Size(849, 527);
            dgProduction.TabIndex = 0;
            // 
            // colPart
            // 
            colPart.HeaderText = "Part";
            colPart.MinimumWidth = 8;
            colPart.Name = "colPart";
            colPart.Resizable = DataGridViewTriState.True;
            colPart.SortMode = DataGridViewColumnSortMode.Automatic;
            colPart.Width = 300;
            // 
            // colRecipe
            // 
            colRecipe.HeaderText = "Recipe";
            colRecipe.MinimumWidth = 8;
            colRecipe.Name = "colRecipe";
            colRecipe.Width = 150;
            // 
            // colQuantity
            // 
            colQuantity.HeaderText = "Quantity";
            colQuantity.MinimumWidth = 8;
            colQuantity.Name = "colQuantity";
            colQuantity.Width = 150;
            // 
            // colDelete
            // 
            colDelete.HeaderText = "Delete?";
            colDelete.MinimumWidth = 8;
            colDelete.Name = "colDelete";
            colDelete.Resizable = DataGridViewTriState.True;
            colDelete.SortMode = DataGridViewColumnSortMode.Automatic;
            colDelete.Text = "DELETE";
            colDelete.Width = 150;
            // 
            // lblParts
            // 
            lblParts.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblParts.AutoSize = true;
            lblParts.Location = new Point(1669, 1150);
            lblParts.Name = "lblParts";
            lblParts.Size = new Size(69, 25);
            lblParts.TabIndex = 2;
            lblParts.Text = "lblParts";
            lblParts.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(177, 28);
            label1.TabIndex = 3;
            label1.Text = "Production goals:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { dataGridViewComboBoxColumn2, dataGridViewComboBoxColumn1, dataGridViewTextBoxColumn1, dataGridViewButtonColumn1 });
            dataGridView1.Location = new Point(889, 47);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.ScrollBars = ScrollBars.Vertical;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(849, 527);
            dataGridView1.TabIndex = 4;
            // 
            // dataGridViewComboBoxColumn2
            // 
            dataGridViewComboBoxColumn2.HeaderText = "Factory";
            dataGridViewComboBoxColumn2.MinimumWidth = 8;
            dataGridViewComboBoxColumn2.Name = "dataGridViewComboBoxColumn2";
            dataGridViewComboBoxColumn2.Width = 150;
            // 
            // dataGridViewComboBoxColumn1
            // 
            dataGridViewComboBoxColumn1.HeaderText = "Part";
            dataGridViewComboBoxColumn1.MinimumWidth = 8;
            dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
            dataGridViewComboBoxColumn1.Resizable = DataGridViewTriState.True;
            dataGridViewComboBoxColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
            dataGridViewComboBoxColumn1.Width = 300;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Quantity";
            dataGridViewTextBoxColumn1.MinimumWidth = 8;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.Width = 150;
            // 
            // dataGridViewButtonColumn1
            // 
            dataGridViewButtonColumn1.HeaderText = "Delete?";
            dataGridViewButtonColumn1.MinimumWidth = 8;
            dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            dataGridViewButtonColumn1.Resizable = DataGridViewTriState.True;
            dataGridViewButtonColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
            dataGridViewButtonColumn1.Text = "DELETE";
            dataGridViewButtonColumn1.Width = 150;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label2.Location = new Point(889, 9);
            label2.Name = "label2";
            label2.Size = new Size(91, 28);
            label2.TabIndex = 5;
            label2.Text = "Imports:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnAddPart
            // 
            btnAddPart.Location = new Point(195, 7);
            btnAddPart.Name = "btnAddPart";
            btnAddPart.Size = new Size(112, 34);
            btnAddPart.TabIndex = 6;
            btnAddPart.Text = "Add Part";
            btnAddPart.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(986, 7);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 7;
            button1.Text = "Add Import";
            button1.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.AllowUserToResizeColumns = false;
            dataGridView2.AllowUserToResizeRows = false;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { dataGridViewComboBoxColumn3, dataGridViewComboBoxColumn4, dataGridViewTextBoxColumn2, dataGridViewButtonColumn2 });
            dataGridView2.Location = new Point(12, 626);
            dataGridView2.MultiSelect = false;
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 62;
            dataGridView2.ScrollBars = ScrollBars.Vertical;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.Size = new Size(849, 527);
            dataGridView2.TabIndex = 8;
            // 
            // dataGridViewComboBoxColumn3
            // 
            dataGridViewComboBoxColumn3.HeaderText = "Part";
            dataGridViewComboBoxColumn3.MinimumWidth = 8;
            dataGridViewComboBoxColumn3.Name = "dataGridViewComboBoxColumn3";
            dataGridViewComboBoxColumn3.Resizable = DataGridViewTriState.True;
            dataGridViewComboBoxColumn3.SortMode = DataGridViewColumnSortMode.Automatic;
            dataGridViewComboBoxColumn3.Width = 300;
            // 
            // dataGridViewComboBoxColumn4
            // 
            dataGridViewComboBoxColumn4.HeaderText = "Recipe";
            dataGridViewComboBoxColumn4.MinimumWidth = 8;
            dataGridViewComboBoxColumn4.Name = "dataGridViewComboBoxColumn4";
            dataGridViewComboBoxColumn4.Width = 150;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Quantity";
            dataGridViewTextBoxColumn2.MinimumWidth = 8;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.Width = 150;
            // 
            // dataGridViewButtonColumn2
            // 
            dataGridViewButtonColumn2.HeaderText = "Delete?";
            dataGridViewButtonColumn2.MinimumWidth = 8;
            dataGridViewButtonColumn2.Name = "dataGridViewButtonColumn2";
            dataGridViewButtonColumn2.Resizable = DataGridViewTriState.True;
            dataGridViewButtonColumn2.SortMode = DataGridViewColumnSortMode.Automatic;
            dataGridViewButtonColumn2.Text = "DELETE";
            dataGridViewButtonColumn2.Width = 150;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label3.Location = new Point(12, 586);
            label3.Name = "label3";
            label3.Size = new Size(194, 28);
            label3.TabIndex = 9;
            label3.Text = "Intermediate Parts:";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1750, 1187);
            Controls.Add(label3);
            Controls.Add(dataGridView2);
            Controls.Add(button1);
            Controls.Add(btnAddPart);
            Controls.Add(label2);
            Controls.Add(dataGridView1);
            Controls.Add(label1);
            Controls.Add(lblParts);
            Controls.Add(dgProduction);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgProduction).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgProduction;
        private Label lblParts;
        private DataGridViewComboBoxColumn colPart;
        private DataGridViewComboBoxColumn colRecipe;
        private DataGridViewTextBoxColumn colQuantity;
        private DataGridViewButtonColumn colDelete;
        private Label label1;
        private DataGridView dataGridView1;
        private Label label2;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn2;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewButtonColumn dataGridViewButtonColumn1;
        private Button btnAddPart;
        private Button button1;
        private DataGridView dataGridView2;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn3;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewButtonColumn dataGridViewButtonColumn2;
        private Label label3;
    }
}
