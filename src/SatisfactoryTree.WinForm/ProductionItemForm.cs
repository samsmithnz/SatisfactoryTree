using SatisfactoryTree.Models;
using SatisfactoryTree.Services;
using SatisfactoryTree.WinForm.Services;

namespace SatisfactoryTree.WinForm
{
    public partial class ProductionItemForm : Form
    {
        private readonly ProductionPlanningService _productionService;
        private readonly string _factoryId;
        private ProductionGoal? _existingGoal;
        private readonly SatisfactoryDataService _dataService;

        public string ItemName => txtItemName.Text;
        public decimal TargetQuantity => txtTargetQuantity.Value;
        public bool ImportInputs => rbImportInputs.Checked;
        public bool AutoDependencies => chkAutoDependencies.Checked;
        public string? SelectedRecipe => cmbRecipe.SelectedItem?.ToString();
        public string? SelectedRecipeClassName => (cmbRecipe.SelectedItem as RecipeItem)?.Recipe?.ClassName;

        public ProductionItemForm(ProductionPlanningService productionService, string factoryId, ProductionGoal? existingGoal = null)
        {
            InitializeComponent();
            _productionService = productionService;
            _factoryId = factoryId;
            _existingGoal = existingGoal;
            _dataService = SatisfactoryDataService.Instance;
            
            LoadForm();
        }

        private void LoadForm()
        {
            // Load actual recipes from SatisfactoryTree data
            LoadAvailableItems();

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

        private void LoadAvailableItems()
        {
            // Add AutoComplete source for item names
            var itemNames = _dataService.GetItemDisplayNames();
            var autoCompleteSource = new AutoCompleteStringCollection();
            autoCompleteSource.AddRange(itemNames.ToArray());
            
            txtItemName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtItemName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtItemName.AutoCompleteCustomSource = autoCompleteSource;
        }

        private void LoadRecipesForItem()
        {
            cmbRecipe.Items.Clear();
            
            string itemName = txtItemName.Text.Trim();
            if (string.IsNullOrEmpty(itemName))
                return;

            // Find the item by display name
            var item = _dataService.GetItemByDisplayName(itemName);
            if (item?.ClassName == null)
                return;

            // Get recipes that produce this item
            var recipes = _dataService.GetRecipesForItem(item.ClassName);
            
            foreach (var recipe in recipes.OrderBy(r => r.IsAlternateRecipe ? 1 : 0))
            {
                var displayName = recipe.IsAlternateRecipe ? 
                    $"Alternate: {recipe.DisplayName}" : 
                    recipe.DisplayName ?? "Standard Recipe";
                
                cmbRecipe.Items.Add(new RecipeItem(recipe, displayName));
            }
            
            if (cmbRecipe.Items.Count > 0)
            {
                cmbRecipe.SelectedIndex = 0;
            }
        }

        private void txtItemName_TextChanged(object sender, EventArgs e)
        {
            LoadRecipesForItem();
            UpdateRecipeInfo();
        }

        private void txtTargetQuantity_ValueChanged(object sender, EventArgs e)
        {
            UpdateRecipeInfo();
        }

        private void rbImportInputs_CheckedChanged(object sender, EventArgs e)
        {
            grpRecipeInfo.Enabled = !rbImportInputs.Checked;
            chkAutoDependencies.Enabled = false;
            chkAutoDependencies.Checked = false;
            UpdateRecipeInfo();
        }

        private void rbProduceOnsite_CheckedChanged(object sender, EventArgs e)
        {
            grpRecipeInfo.Enabled = rbProduceOnsite.Checked;
            chkAutoDependencies.Enabled = rbProduceOnsite.Checked;
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
            
            if (string.IsNullOrWhiteSpace(itemName) || cmbRecipe.SelectedItem == null)
            {
                txtBuildingsRequired.Text = "";
                txtPowerUsage.Text = "";
                lstInputsNeeded.Items.Clear();
                return;
            }

            // Get the selected recipe
            var selectedRecipeItem = cmbRecipe.SelectedItem as RecipeItem;
            if (selectedRecipeItem?.Recipe == null)
                return;

            var recipe = selectedRecipeItem.Recipe;
            
            // Calculate production requirements
            var outputPerMinute = _dataService.CalculateOutputPerMinute(recipe);
            var buildingsRequired = _dataService.CalculateBuildingsRequired(recipe, targetQuantity);
            var buildingDisplayName = _dataService.GetBuildingDisplayName(recipe.ProducedIn);

            // For power usage, we'll use estimated values since power consumption isn't in the recipe data
            var estimatedPowerPerBuilding = GetEstimatedPowerUsage(buildingDisplayName);
            var totalPowerUsage = buildingsRequired * estimatedPowerPerBuilding;

            txtBuildingsRequired.Text = $"{buildingsRequired:N0} {buildingDisplayName}";
            txtPowerUsage.Text = $"{totalPowerUsage:N1} MW";

            // Update inputs needed
            lstInputsNeeded.Items.Clear();
            var inputRequirements = _dataService.GetRecipeInputRequirements(recipe, targetQuantity);
            
            foreach (var input in inputRequirements)
            {
                var item = new ListViewItem(input.Key);
                item.SubItems.Add($"{input.Value:N1}/min");
                lstInputsNeeded.Items.Add(item);
            }
        }

        private decimal GetEstimatedPowerUsage(string buildingType)
        {
            // Estimated power usage values for different building types
            return buildingType.ToLower() switch
            {
                "constructor" => 4,
                "assembler" => 15,
                "foundry" => 16,
                "manufacturer" => 55,
                "refinery" => 30,
                "smelter" => 4,
                _ => 10 // Default estimate
            };
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

        private class RecipeItem
        {
            public NewRecipe Recipe { get; }
            public string DisplayName { get; }

            public RecipeItem(NewRecipe recipe, string displayName)
            {
                Recipe = recipe;
                DisplayName = displayName;
            }

            public override string ToString()
            {
                return DisplayName;
            }
        }
    }
}