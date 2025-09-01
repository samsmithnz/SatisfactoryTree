using SatisfactoryTree.Models;
using SatisfactoryTree.Services;
using SatisfactoryTree.WinForm.Services;

namespace SatisfactoryTree.WinForm
{
    public partial class ProductionPlanningForm : Form
    {
        private readonly ProductionPlanningService _productionService;
        private readonly SatisfactoryDataService _dataService;
        private string _selectedFactoryId = "default";

        public ProductionPlanningForm()
        {
            InitializeComponent();
            _productionService = new ProductionPlanningService();
            _dataService = SatisfactoryDataService.Instance;
        }

        private void ProductionPlanningForm_Load(object sender, EventArgs e)
        {
            InitializeListView();
            LoadFactories();
            RefreshFactoryDetails();
            SetupAutoComplete();
        }

        private void InitializeListView()
        {
            // Initialize ListView columns according to the requirements
            listProductionItems.Columns.Clear();
            listProductionItems.Columns.Add("Item", 200);
            listProductionItems.Columns.Add("Recipe", 200);
            listProductionItems.Columns.Add("Quantity", 100);
            listProductionItems.Columns.Add("Inputs", 250);
            listProductionItems.Columns.Add("Buildings", 150);
            listProductionItems.Columns.Add("Power (MW)", 100);
        }

        private void SetupAutoComplete()
        {
            // Add AutoComplete source for production item names
            var itemNames = _dataService.GetItemDisplayNames();
            var autoCompleteSource = new AutoCompleteStringCollection();
            autoCompleteSource.AddRange(itemNames.ToArray());
            
            txtProductionItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtProductionItem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtProductionItem.AutoCompleteCustomSource = autoCompleteSource;
        }

        private void LoadFactories()
        {
            treeFactories.Nodes.Clear();

            foreach (var factory in _productionService.GetAllFactories())
            {
                var factoryNode = new TreeNode(factory.Name)
                {
                    Tag = factory.Id,
                    Name = factory.Id
                };
                treeFactories.Nodes.Add(factoryNode);
            }

            if (treeFactories.Nodes.Count > 0)
            {
                treeFactories.SelectedNode = treeFactories.Nodes[0];
                _selectedFactoryId = treeFactories.Nodes[0].Tag.ToString() ?? "default";
            }
        }

        private void treeFactories_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag != null)
            {
                _selectedFactoryId = e.Node.Tag.ToString() ?? "default";
                RefreshFactoryDetails();
            }
        }

        private void listProductionItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle selection of specific production items for editing
        }

        private void btnAddFactory_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFactoryName.Text))
            {
                MessageBox.Show("Please enter a factory name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var factoryId = Guid.NewGuid().ToString();
                _productionService.AddFactory(factoryId, txtFactoryName.Text);
                txtFactoryName.Clear();
                LoadFactories();
                RefreshFactoryDetails();
                MessageBox.Show("Factory added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding factory: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddProductionItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new ProductionItemForm(_productionService, _selectedFactoryId))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var goal = _productionService.AddProductionGoalWithDependencies(
                            dialog.ItemName, 
                            dialog.TargetQuantity, 
                            _selectedFactoryId, 
                            !dialog.ImportInputs && dialog.AutoDependencies,
                            dialog.SelectedRecipeClassName);
                        
                        RefreshFactoryDetails();
                        
                        var dependencyMessage = goal.DependentGoals.Any() 
                            ? $"\n\nAuto-created {goal.DependentGoals.Count} dependency goals recursively." 
                            : "";
                        
                        MessageBox.Show($"Production goal added successfully!\n\nItem: {dialog.ItemName}\nQuantity: {dialog.TargetQuantity:N0}\nMethod: {(dialog.ImportInputs ? "Import Inputs" : "Produce Onsite")}{dependencyMessage}", 
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error adding production goal: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnEditProductionItem_Click(object sender, EventArgs e)
        {
            // For now, check if any production item is selected
            // TODO: Implement editing functionality
            MessageBox.Show("Edit functionality will be implemented based on selected production item.", "Edit Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnProcessProduction_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductionItem.Text))
            {
                MessageBox.Show("Please enter an item name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _productionService.ProcessProduction(txtProductionItem.Text, txtProductionQuantity.Value, _selectedFactoryId);
                txtProductionItem.Clear();
                txtProductionQuantity.Value = 10;
                RefreshFactoryDetails();
                MessageBox.Show("Production processed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing production: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshFactoryDetails()
        {
            listProductionItems.Items.Clear();

            // Get the currently selected factory
            var selectedFactory = _productionService.GetAllFactories().FirstOrDefault(f => f.Id == _selectedFactoryId);
            if (selectedFactory == null) return;

            // Get all goals including dependencies 
            var allGoals = _productionService.GetAllGoalsIncludingDependencies(_selectedFactoryId);
            
            foreach (var goal in allGoals)
            {
                // Get actual recipe information for this item
                var item = _dataService.GetItemByDisplayName(goal.ItemName);
                var recipes = item?.ClassName != null ? _dataService.GetRecipesForItem(item.ClassName) : new List<NewRecipe>();
                var primaryRecipe = recipes.FirstOrDefault(r => !r.IsAlternateRecipe) ?? recipes.FirstOrDefault();
                
                // Use specified recipe if available
                if (!string.IsNullOrEmpty(goal.RecipeClassName))
                {
                    var specifiedRecipe = _dataService.GetRecipeByClassName(goal.RecipeClassName);
                    if (specifiedRecipe != null)
                    {
                        primaryRecipe = specifiedRecipe;
                    }
                }
                
                // Create main production item row
                var icon = goal.ProduceInternally ? "🔧" : "📥";
                var indent = string.IsNullOrEmpty(goal.ParentGoalId) ? "" : "  → ";
                var listItem = new ListViewItem($"{indent}{icon} {goal.ItemName}");
                listItem.Tag = goal;
                
                // Add recipe column
                var recipeName = goal.ProduceInternally && primaryRecipe != null
                    ? (primaryRecipe.IsAlternateRecipe ? $"Alternate: {primaryRecipe.DisplayName}" : primaryRecipe.DisplayName ?? "Standard Recipe")
                    : "Import";
                listItem.SubItems.Add(recipeName);
                
                // Add quantity column (with 3 decimal places and progress)
                var quantityText = $"{goal.TargetQuantity:N3}";
                if (goal.CurrentQuantity > 0)
                {
                    quantityText += $" ({goal.ProgressPercentage:F1}% complete)";
                }
                listItem.SubItems.Add(quantityText);
                
                // Add inputs column
                var inputsText = "";
                var buildingsText = "";
                var powerText = "";
                
                if (goal.ProduceInternally && primaryRecipe != null)
                {
                    var inputRequirements = _dataService.GetRecipeInputRequirements(primaryRecipe, goal.TargetQuantity);
                    if (inputRequirements.Any())
                    {
                        inputsText = string.Join(", ", inputRequirements.Select(i => $"🧱 {i.Key}: {i.Value:N1}/min"));
                    }
                    else
                    {
                        inputsText = "No inputs required";
                    }
                    
                    // Add buildings column
                    var buildingName = _dataService.GetBuildingDisplayName(primaryRecipe.ProducedIn);
                    var buildingsRequired = _dataService.CalculateBuildingsRequired(primaryRecipe, goal.TargetQuantity);
                    buildingsText = $"🏭 {buildingName}: {buildingsRequired:N0}";
                    
                    // Add power column  
                    var powerPerBuilding = GetEstimatedPowerUsage(buildingName);
                    var totalPower = buildingsRequired * powerPerBuilding;
                    powerText = $"{totalPower:N1}";
                }
                else
                {
                    inputsText = "Imported";
                    buildingsText = "N/A";
                    powerText = "0";
                }
                
                listItem.SubItems.Add(inputsText);
                listItem.SubItems.Add(buildingsText);
                listItem.SubItems.Add(powerText);
                
                listProductionItems.Items.Add(listItem);
            }
            
            if (!allGoals.Any())
            {
                var emptyItem = new ListViewItem("No active production items");
                emptyItem.SubItems.Add("");
                emptyItem.SubItems.Add("");
                emptyItem.SubItems.Add("");
                emptyItem.SubItems.Add("");
                emptyItem.SubItems.Add("");
                listProductionItems.Items.Add(emptyItem);
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
    }
}