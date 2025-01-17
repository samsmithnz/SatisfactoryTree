using Microsoft.VisualBasic.ApplicationServices;
using SatisfactoryTree.Console;
using SatisfactoryTree.Console.NewModels;
using SatisfactoryTree.Console.OldModels;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace SatisfactoryTree.WinForm
{
    public partial class Form1 : Form
    {
        private FinalData data;
        private List<ComboItem> parts;
        private List<Recipe> recipes;
        private DataTable gridData;

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            Processor processor = new();
            processor.GetContentFiles();
            if (processor != null)
            {
                string inputFile = processor.InputFile;
                string outputFile = processor.OutputFile;
                data = await Processor.ProcessFileOldModel(inputFile, outputFile);

                parts = GetParts(data);
                gridData = new();
                gridData.Columns.Add("Part", typeof(string));
                gridData.Columns.Add("Recipe", typeof(string));
                gridData.Columns.Add("Quantity", typeof(int));
                lblParts.Text = gridData.Rows.Count + " parts";
                dgProduction.AutoGenerateColumns = false;
                dgProduction.DataSource = gridData;

                DataGridViewComboBoxColumn colParts = (DataGridViewComboBoxColumn)dgProduction.Columns[0];
                colParts.DataSource = parts;
                colParts.DataPropertyName = "Id";
                colParts.DisplayMember = "Text";
                colParts.ValueMember = "Id";

            }
        }

        private List<ComboItem> GetParts(FinalData data)
        {
            List<ComboItem> parts = new();
            if (data.items != null && data.items.parts != null)
            {
                foreach (KeyValuePair<string, Console.OldModels.Part> part in data.items.parts)
                {
                    parts.Add(new ComboItem(part.Key, part.Value.name));
                }
            }
            //sort the parts
            parts.Sort((x, y) => x.Text.CompareTo(y.Text));
            return parts;
        }

        private List<ComboItem> GetRecipesForPart(string partId)
        {
            List<ComboItem> recipes = new();
            if (data.recipes != null)
            {
                foreach (Recipe recipe in data.recipes)
                {
                    if (recipe.products != null)
                    {
                        foreach (Product product in recipe.products)
                        {
                            if (product.part == partId)
                            {
                                recipes.Add(new ComboItem(recipe.id, recipe.displayName));
                            }
                        }
                    }
                }
            }
            //sort the parts
            recipes.Sort((x, y) => x.Text.CompareTo(y.Text));
            return recipes;
        }

        private void btnAddPart_Click(object sender, EventArgs e)
        {
            DataRow row = gridData.NewRow();
            row["Part"] = "IronPlate";
            gridData.Rows.Add(row);
            lblParts.Text = gridData.Rows.Count + " parts";
            dgProduction.DataSource = gridData;
            dgProduction.Refresh();
        }

    }
}
