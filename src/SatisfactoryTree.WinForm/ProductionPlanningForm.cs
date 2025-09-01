using SatisfactoryTree.Models;
using SatisfactoryTree.Services;

namespace SatisfactoryTree.WinForm
{
    public partial class ProductionPlanningForm : Form
    {
        private readonly ProductionPlanningService _productionService;
        private string _selectedFactoryId = "default";

        public ProductionPlanningForm()
        {
            InitializeComponent();
            _productionService = new ProductionPlanningService();
        }

        private void ProductionPlanningForm_Load(object sender, EventArgs e)
        {
            LoadFactories();
            RefreshFactoryDetails();
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
                    
                    // Add detailed production information
                    itemNode.Nodes.Add(new TreeNode($"📊 Target Quantity: {goal.TargetQuantity:N0}"));
                    itemNode.Nodes.Add(new TreeNode($"📈 Current Progress: {goal.CurrentQuantity:N0} ({goal.ProgressPercentage:F1}%)"));
                    itemNode.Nodes.Add(new TreeNode($"🏗️ Recipe: Standard Recipe (placeholder)"));
                    itemNode.Nodes.Add(new TreeNode($"🏭 Buildings Required: Calculated based on recipe"));
                    itemNode.Nodes.Add(new TreeNode($"📥 Inputs: Will show required inputs"));
                    itemNode.Nodes.Add(new TreeNode($"⚡ Power Usage: Will show power requirements"));
                    
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
    }
}