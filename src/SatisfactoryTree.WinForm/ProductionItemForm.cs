using SatisfactoryTree.Models;
using SatisfactoryTree.Services;

namespace SatisfactoryTree.WinForm
{
    public partial class ProductionItemForm : Form
    {
        private readonly ProductionPlanningService _productionService;
        private readonly string _factoryId;
        private ProductionGoal? _existingGoal;

        public string ItemName => txtItemName.Text;
        public decimal TargetQuantity => txtTargetQuantity.Value;
        public bool ImportInputs => rbImportInputs.Checked;
        public string? SelectedRecipe => cmbRecipe.SelectedItem?.ToString();

        public ProductionItemForm(ProductionPlanningService productionService, string factoryId, ProductionGoal? existingGoal = null)
        {
            InitializeComponent();
            _productionService = productionService;
            _factoryId = factoryId;
            _existingGoal = existingGoal;
            
            LoadForm();
        }

        private void LoadForm()
        {
            // Load sample recipes (in a real implementation, this would come from the SatisfactoryTree dependency)
            LoadSampleRecipes();

            if (_existingGoal != null)
            {
                // Edit mode
                lblTitle.Text = "Edit Production Item";
                this.Text = "Edit Production Item";
                txtItemName.Text = _existingGoal.ItemName;
                txtTargetQuantity.Value = _existingGoal.TargetQuantity;
                txtItemName.ReadOnly = true; // Don't allow changing item name when editing
            }
            else
            {
                // Add mode
                lblTitle.Text = "Add Production Item";
                this.Text = "Add Production Item";
            }

            UpdateRecipeInfo();
        }

        private void LoadSampleRecipes()
        {
            // Sample recipes for demonstration
            // In a real implementation, this would load from the SatisfactoryTree dependency
            var sampleRecipes = new Dictionary<string, List<string>>
            {
                { "Iron Plate", new List<string> { "Standard Recipe", "Alternative: Coated Iron Plate" } },
                { "Iron Rod", new List<string> { "Standard Recipe", "Alternative: Steel Rod" } },
                { "Steel Ingot", new List<string> { "Standard Recipe", "Alternative: Solid Steel Ingot" } },
                { "Reinforced Plate", new List<string> { "Standard Recipe", "Alternative: Adhered Iron Plate" } },
                { "Concrete", new List<string> { "Standard Recipe", "Alternative: Wet Concrete" } },
                { "Copper Ingot", new List<string> { "Standard Recipe" } },
                { "Wire", new List<string> { "Standard Recipe", "Alternative: Fused Wire" } },
                { "Cable", new List<string> { "Standard Recipe", "Alternative: Coated Cable" } }
            };

            cmbRecipe.Items.Clear();
            
            string itemName = txtItemName.Text.Trim();
            if (sampleRecipes.ContainsKey(itemName))
            {
                foreach (var recipe in sampleRecipes[itemName])
                {
                    cmbRecipe.Items.Add(recipe);
                }
                if (cmbRecipe.Items.Count > 0)
                {
                    cmbRecipe.SelectedIndex = 0;
                }
            }
        }

        private void txtItemName_TextChanged(object sender, EventArgs e)
        {
            LoadSampleRecipes();
            UpdateRecipeInfo();
        }

        private void txtTargetQuantity_ValueChanged(object sender, EventArgs e)
        {
            UpdateRecipeInfo();
        }

        private void rbImportInputs_CheckedChanged(object sender, EventArgs e)
        {
            grpRecipeInfo.Enabled = !rbImportInputs.Checked;
            UpdateRecipeInfo();
        }

        private void rbProduceOnsite_CheckedChanged(object sender, EventArgs e)
        {
            grpRecipeInfo.Enabled = rbProduceOnsite.Checked;
            UpdateRecipeInfo();
        }

        private void cmbRecipe_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateRecipeInfo();
        }

        private void UpdateRecipeInfo()
        {
            if (rbImportInputs.Checked)
            {
                // Clear recipe info when importing
                txtBuildingsRequired.Text = "N/A";
                txtPowerUsage.Text = "N/A";
                lstInputsNeeded.Items.Clear();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtItemName.Text) || cmbRecipe.SelectedItem == null)
            {
                txtBuildingsRequired.Text = "";
                txtPowerUsage.Text = "";
                lstInputsNeeded.Items.Clear();
                return;
            }

            // Sample recipe calculations (in a real implementation, this would use actual recipe data)
            CalculateRecipeRequirements();
        }

        private void CalculateRecipeRequirements()
        {
            string itemName = txtItemName.Text.Trim();
            decimal targetQuantity = txtTargetQuantity.Value;
            string recipe = cmbRecipe.SelectedItem?.ToString() ?? "";

            // Sample calculations for demonstration
            var recipeData = GetSampleRecipeData(itemName, recipe);
            
            if (recipeData != null)
            {
                decimal productionPerMinute = recipeData.OutputPerMinute;
                decimal buildingsRequired = Math.Ceiling(targetQuantity / productionPerMinute);
                decimal totalPowerUsage = buildingsRequired * recipeData.PowerUsage;

                txtBuildingsRequired.Text = $"{buildingsRequired:N0} {recipeData.BuildingType}";
                txtPowerUsage.Text = $"{totalPowerUsage:N0} MW";

                // Update inputs needed
                lstInputsNeeded.Items.Clear();
                foreach (var input in recipeData.Inputs)
                {
                    decimal inputNeeded = (targetQuantity / productionPerMinute) * input.Value;
                    var item = new ListViewItem(input.Key);
                    item.SubItems.Add($"{inputNeeded:N1}/min");
                    lstInputsNeeded.Items.Add(item);
                }
            }
        }

        private RecipeData? GetSampleRecipeData(string itemName, string recipe)
        {
            // Sample recipe data for demonstration
            // In a real implementation, this would come from the SatisfactoryTree dependency
            var sampleData = new Dictionary<string, RecipeData>
            {
                { "Iron Plate", new RecipeData 
                    { 
                        OutputPerMinute = 20, 
                        BuildingType = "Constructor", 
                        PowerUsage = 4,
                        Inputs = new Dictionary<string, decimal> { { "Iron Ingot", 30 } }
                    }
                },
                { "Iron Rod", new RecipeData 
                    { 
                        OutputPerMinute = 15, 
                        BuildingType = "Constructor", 
                        PowerUsage = 4,
                        Inputs = new Dictionary<string, decimal> { { "Iron Ingot", 15 } }
                    }
                },
                { "Steel Ingot", new RecipeData 
                    { 
                        OutputPerMinute = 45, 
                        BuildingType = "Foundry", 
                        PowerUsage = 16,
                        Inputs = new Dictionary<string, decimal> { { "Iron Ore", 45 }, { "Coal", 45 } }
                    }
                },
                { "Reinforced Plate", new RecipeData 
                    { 
                        OutputPerMinute = 5, 
                        BuildingType = "Assembler", 
                        PowerUsage = 15,
                        Inputs = new Dictionary<string, decimal> { { "Iron Plate", 30 }, { "Screw", 60 } }
                    }
                },
                { "Wire", new RecipeData 
                    { 
                        OutputPerMinute = 30, 
                        BuildingType = "Constructor", 
                        PowerUsage = 4,
                        Inputs = new Dictionary<string, decimal> { { "Copper Ingot", 15 } }
                    }
                }
            };

            return sampleData.ContainsKey(itemName) ? sampleData[itemName] : null;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtItemName.Text))
            {
                MessageBox.Show("Please enter an item name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtTargetQuantity.Value <= 0)
            {
                MessageBox.Show("Target quantity must be greater than 0.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (rbProduceOnsite.Checked && cmbRecipe.SelectedItem == null)
            {
                MessageBox.Show("Please select a recipe when producing onsite.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private class RecipeData
        {
            public decimal OutputPerMinute { get; set; }
            public string BuildingType { get; set; } = "";
            public decimal PowerUsage { get; set; }
            public Dictionary<string, decimal> Inputs { get; set; } = new();
        }
    }
}