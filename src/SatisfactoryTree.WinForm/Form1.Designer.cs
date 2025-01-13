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
            dataGridView1 = new DataGridView();
            colPart = new DataGridViewComboBoxColumn();
            colRecipe = new DataGridViewComboBoxColumn();
            colQuantity = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { colPart, colRecipe, colQuantity });
            dataGridView1.Location = new Point(12, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(873, 574);
            dataGridView1.TabIndex = 0;
            // 
            // colPart
            // 
            colPart.HeaderText = "Part";
            colPart.MinimumWidth = 8;
            colPart.Name = "colPart";
            colPart.Resizable = DataGridViewTriState.True;
            colPart.SortMode = DataGridViewColumnSortMode.Automatic;
            colPart.Width = 150;
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(897, 598);
            Controls.Add(dataGridView1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridViewComboBoxColumn colPart;
        private DataGridViewComboBoxColumn colRecipe;
        private DataGridViewTextBoxColumn colQuantity;
    }
}
