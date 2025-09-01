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
            LoadFactories();
            RefreshFactoryDetails();
            SetupAutoComplete();
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

        private void treeFactoryDetails_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Handle selection of specific factory details for editing
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
                        _productionService.AddProductionGoal(dialog.ItemName, dialog.TargetQuantity, _selectedFactoryId);
                        RefreshFactoryDetails();
                        MessageBox.Show($"Production goal added successfully!\n\nItem: {dialog.ItemName}\nQuantity: {dialog.TargetQuantity:N0}\nMethod: {(dialog.ImportInputs ? "Import Inputs" : "Produce Onsite")}", 
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
            var selectedNode = treeFactoryDetails.SelectedNode;
            if (selectedNode?.Tag is ProductionGoal goal)
            {
                using (var dialog = new ProductionItemForm(_productionService, _selectedFactoryId, goal))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            // Update the existing goal
                            goal.TargetQuantity = dialog.TargetQuantity;
                            RefreshFactoryDetails();
                            MessageBox.Show($"Production goal updated successfully!\n\nItem: {dialog.ItemName}\nNew Quantity: {dialog.TargetQuantity:N0}", 
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error updating production goal: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a production goal to edit.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
            treeFactoryDetails.Nodes.Clear();

            foreach (var factory in _productionService.GetAllFactories())
            {
                var factoryNode = new TreeNode($"🏭 {factory.Name} ({factory.Id})")
                {
                    Tag = factory.Id,
                    Name = factory.Id
                };

                // Add Goal Production section
                var goalProductionNode = new TreeNode("🎯 Goal Production");
                var activeGoals = factory.GetActiveGoals();
                var completedGoals = factory.GetCompletedGoals();
                
                if (activeGoals.Any() || completedGoals.Any())
                {
                    foreach (var goal in activeGoals)
                    {
                        var goalText = $"📋 {goal.ItemName}: {goal.CurrentQuantity:N0}/{goal.TargetQuantity:N0} ({goal.ProgressPercentage:F1}%) - Active";
                        var goalNode = new TreeNode(goalText) { Tag = goal };
                        goalProductionNode.Nodes.Add(goalNode);
                    }
                    
                    foreach (var goal in completedGoals.Take(5)) // Show last 5 completed goals
                    {
                        var goalText = $"✅ {goal.ItemName}: {goal.TargetQuantity:N0} - Completed {goal.CompletedDate?.ToString("MM/dd")}";
                        var goalNode = new TreeNode(goalText) { Tag = goal };
                        goalProductionNode.Nodes.Add(goalNode);
                    }
                }
                else
                {
                    goalProductionNode.Nodes.Add(new TreeNode("No production goals"));
                }
                factoryNode.Nodes.Add(goalProductionNode);

                // Add Imports section (placeholder for future implementation)
                var importsNode = new TreeNode("📥 Imports");
                importsNode.Nodes.Add(new TreeNode("Import functionality will be implemented"));
                factoryNode.Nodes.Add(importsNode);

                // Add Items Being Exported or in Storage section
                var storageNode = new TreeNode("📦 Storage & Exports");
                var storageItems = factory.Storage.GetAllItems();
                if (storageItems.Any())
                {
                    foreach (var item in storageItems.OrderBy(x => x.Key))
                    {
                        var itemText = $"📋 {item.Key}: {item.Value:N0} units";
                        var itemNode = new TreeNode(itemText) { Tag = item };
                        storageNode.Nodes.Add(itemNode);
                    }
                }
                else
                {
                    storageNode.Nodes.Add(new TreeNode("No items in storage"));
                }
                factoryNode.Nodes.Add(storageNode);

                // Add Production Items section with detailed information
                var productionItemsNode = new TreeNode("⚙️ Production Items");
                foreach (var goal in activeGoals)
                {
                    var itemNode = new TreeNode($"🔧 {goal.ItemName} Production")
                    {
                        Tag = goal
                    };
                    
                    // Get actual recipe information for this item
                    var item = _dataService.GetItemByDisplayName(goal.ItemName);
                    var recipes = item?.ClassName != null ? _dataService.GetRecipesForItem(item.ClassName) : new List<NewRecipe>();
                    var primaryRecipe = recipes.FirstOrDefault(r => !r.IsAlternateRecipe) ?? recipes.FirstOrDefault();
                    
                    // Add detailed production information
                    itemNode.Nodes.Add(new TreeNode($"📊 Target Quantity: {goal.TargetQuantity:N0}"));
                    itemNode.Nodes.Add(new TreeNode($"📈 Current Progress: {goal.CurrentQuantity:N0} ({goal.ProgressPercentage:F1}%)"));
                    
                    if (primaryRecipe != null)
                    {
                        var recipeName = primaryRecipe.IsAlternateRecipe ? 
                            $"Alternate: {primaryRecipe.DisplayName}" : 
                            primaryRecipe.DisplayName ?? "Standard Recipe";
                        itemNode.Nodes.Add(new TreeNode($"🏗️ Recipe: {recipeName}"));
                        
                        var buildingName = _dataService.GetBuildingDisplayName(primaryRecipe.ProducedIn);
                        var buildingsRequired = _dataService.CalculateBuildingsRequired(primaryRecipe, goal.TargetQuantity);
                        itemNode.Nodes.Add(new TreeNode($"🏭 Buildings Required: {buildingsRequired:N0} {buildingName}"));
                        
                        var inputRequirements = _dataService.GetRecipeInputRequirements(primaryRecipe, goal.TargetQuantity);
                        if (inputRequirements.Any())
                        {
                            var inputsNode = new TreeNode("📥 Inputs Required:");
                            foreach (var input in inputRequirements)
                            {
                                inputsNode.Nodes.Add(new TreeNode($"  • {input.Key}: {input.Value:N1}/min"));
                            }
                            itemNode.Nodes.Add(inputsNode);
                        }
                        
                        // Estimate power usage
                        var powerPerBuilding = GetEstimatedPowerUsage(buildingName);
                        var totalPower = buildingsRequired * powerPerBuilding;
                        itemNode.Nodes.Add(new TreeNode($"⚡ Power Usage: {totalPower:N1} MW"));
                    }
                    else
                    {
                        itemNode.Nodes.Add(new TreeNode($"🏗️ Recipe: No recipe data available"));
                        itemNode.Nodes.Add(new TreeNode($"🏭 Buildings Required: Unknown"));
                        itemNode.Nodes.Add(new TreeNode($"📥 Inputs: Unknown"));
                        itemNode.Nodes.Add(new TreeNode($"⚡ Power Usage: Unknown"));
                    }
                    
                    productionItemsNode.Nodes.Add(itemNode);
                }
                
                if (!activeGoals.Any())
                {
                    productionItemsNode.Nodes.Add(new TreeNode("No active production items"));
                }
                factoryNode.Nodes.Add(productionItemsNode);

                treeFactoryDetails.Nodes.Add(factoryNode);
                factoryNode.Expand();
                
                // Expand the main sections
                foreach (TreeNode childNode in factoryNode.Nodes)
                {
                    childNode.Expand();
                }
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