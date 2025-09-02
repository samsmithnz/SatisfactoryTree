using SatisfactoryTree.Models;
using SatisfactoryTree.Services;
using SatisfactoryTree.WinForm.Services;

namespace SatisfactoryTree.WinForm
{
    public partial class ProductionRowControl : UserControl
    {
        private ProductionGoal? _productionGoal;
        private readonly SatisfactoryDataService _dataService;
        private readonly ProductionPlanningService _productionService;
        private string _factoryId;

        public event EventHandler<ProductionGoal>? ProductionGoalChanged;
        public event EventHandler<ProductionGoal>? EditRequested;

        public ProductionRowControl(ProductionPlanningService productionService, string factoryId)
        {
            InitializeComponent();
            _dataService = SatisfactoryDataService.Instance;
            _productionService = productionService;
            _factoryId = factoryId;
        }

        public ProductionGoal? ProductionGoal
        {
            get => _productionGoal;
            set
            {
                _productionGoal = value;
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            if (_productionGoal == null) return;

            // Update item label with icon and indentation
            var icon = _productionGoal.ProduceInternally ? "🔧" : "📥";
            var indent = string.IsNullOrEmpty(_productionGoal.ParentGoalId) ? "" : "  → ";
            lblItem.Text = $"{indent}{icon} {_productionGoal.ItemName}";

            // Update quantity
            txtQuantity.Text = _productionGoal.TargetQuantity.ToString("N3");

            // Update progress bar
            progressBar.Value = Math.Min(100, Math.Max(0, (int)_productionGoal.ProgressPercentage));
            progressBar.Visible = _productionGoal.CurrentQuantity > 0;

            // Update recipe combo box
            UpdateRecipeComboBox();

            // Update other displays
            UpdateInputsAndBuildingsDisplay();

            // Update button states
            btnToggleMethod.Text = _productionGoal.ProduceInternally ? "Import" : "Produce";
            btnToggleMethod.Enabled = true;
        }

        private void UpdateRecipeComboBox()
        {
            if (_productionGoal == null) return;

            cmbRecipe.Items.Clear();

            if (!_productionGoal.ProduceInternally)
            {
                cmbRecipe.Items.Add("Import");
                cmbRecipe.SelectedIndex = 0;
                cmbRecipe.Enabled = false;
                return;
            }

            // Get available recipes for this item
            var item = _dataService.GetItemByDisplayName(_productionGoal.ItemName);
            if (item?.ClassName != null)
            {
                var recipes = _dataService.GetRecipesForItem(item.ClassName);
                foreach (var recipe in recipes)
                {
                    var displayName = recipe.IsAlternateRecipe 
                        ? $"Alternate: {recipe.DisplayName}" 
                        : recipe.DisplayName ?? "Standard Recipe";
                    cmbRecipe.Items.Add(displayName);
                    cmbRecipe.Tag = recipe; // Store recipe for later use
                }

                // Select current recipe if specified
                if (!string.IsNullOrEmpty(_productionGoal.RecipeClassName))
                {
                    var currentRecipe = _dataService.GetRecipeByClassName(_productionGoal.RecipeClassName);
                    if (currentRecipe != null)
                    {
                        var currentDisplayName = currentRecipe.IsAlternateRecipe 
                            ? $"Alternate: {currentRecipe.DisplayName}" 
                            : currentRecipe.DisplayName ?? "Standard Recipe";
                        var index = cmbRecipe.Items.Cast<string>().ToList().IndexOf(currentDisplayName);
                        if (index >= 0)
                        {
                            cmbRecipe.SelectedIndex = index;
                        }
                    }
                }
                else if (cmbRecipe.Items.Count > 0)
                {
                    cmbRecipe.SelectedIndex = 0; // Select first (usually standard) recipe
                }
            }

            cmbRecipe.Enabled = cmbRecipe.Items.Count > 1;
        }

        private void UpdateInputsAndBuildingsDisplay()
        {
            if (_productionGoal == null)
            {
                lblInputs.Text = "";
                lblBuildings.Text = "";
                lblPower.Text = "0";
                return;
            }

            if (!_productionGoal.ProduceInternally)
            {
                lblInputs.Text = "Imported";
                lblBuildings.Text = "N/A";
                lblPower.Text = "0";
                return;
            }

            // Get recipe information
            var item = _dataService.GetItemByDisplayName(_productionGoal.ItemName);
            var recipes = item?.ClassName != null ? _dataService.GetRecipesForItem(item.ClassName) : new List<NewRecipe>();
            var primaryRecipe = recipes.FirstOrDefault(r => !r.IsAlternateRecipe) ?? recipes.FirstOrDefault();

            // Use specified recipe if available
            if (!string.IsNullOrEmpty(_productionGoal.RecipeClassName))
            {
                var specifiedRecipe = _dataService.GetRecipeByClassName(_productionGoal.RecipeClassName);
                if (specifiedRecipe != null)
                {
                    primaryRecipe = specifiedRecipe;
                }
            }

            if (primaryRecipe != null)
            {
                // Update inputs
                var inputRequirements = _dataService.GetRecipeInputRequirements(primaryRecipe, _productionGoal.TargetQuantity);
                if (inputRequirements.Any())
                {
                    lblInputs.Text = string.Join(", ", inputRequirements.Select(i => $"🧱 {i.Key}: {i.Value:N1}/min"));
                }
                else
                {
                    lblInputs.Text = "No inputs required";
                }

                // Update buildings
                var buildingName = _dataService.GetBuildingDisplayName(primaryRecipe.ProducedIn);
                var buildingsRequired = _dataService.CalculateBuildingsRequired(primaryRecipe, _productionGoal.TargetQuantity);
                lblBuildings.Text = $"🏭 {buildingName}: {buildingsRequired:N0}";

                // Update power
                var powerPerBuilding = GetEstimatedPowerUsage(buildingName);
                var totalPower = buildingsRequired * powerPerBuilding;
                lblPower.Text = $"{totalPower:N1}";
            }
            else
            {
                lblInputs.Text = "No recipe found";
                lblBuildings.Text = "N/A";
                lblPower.Text = "0";
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

        private void cmbRecipe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_productionGoal == null || cmbRecipe.SelectedIndex < 0) return;

            if (!_productionGoal.ProduceInternally) return;

            // Get the selected recipe
            var item = _dataService.GetItemByDisplayName(_productionGoal.ItemName);
            if (item?.ClassName != null)
            {
                var recipes = _dataService.GetRecipesForItem(item.ClassName);
                if (cmbRecipe.SelectedIndex < recipes.Count)
                {
                    var selectedRecipe = recipes[cmbRecipe.SelectedIndex];
                    _productionGoal.RecipeClassName = selectedRecipe.ClassName;
                    
                    UpdateInputsAndBuildingsDisplay();
                    ProductionGoalChanged?.Invoke(this, _productionGoal);
                }
            }
        }

        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            if (_productionGoal == null) return;

            if (decimal.TryParse(txtQuantity.Text, out decimal newQuantity) && newQuantity > 0)
            {
                _productionGoal.TargetQuantity = newQuantity;
                UpdateInputsAndBuildingsDisplay();
                ProductionGoalChanged?.Invoke(this, _productionGoal);
            }
            else
            {
                // Reset to original value if invalid
                txtQuantity.Text = _productionGoal.TargetQuantity.ToString("N3");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_productionGoal != null)
            {
                EditRequested?.Invoke(this, _productionGoal);
            }
        }

        private void btnToggleMethod_Click(object sender, EventArgs e)
        {
            if (_productionGoal == null) return;

            // Toggle production method
            _productionService.ToggleGoalProductionMethod(_productionGoal.Id, !_productionGoal.ProduceInternally);
            
            UpdateUI();
            ProductionGoalChanged?.Invoke(this, _productionGoal);
        }
    }
}