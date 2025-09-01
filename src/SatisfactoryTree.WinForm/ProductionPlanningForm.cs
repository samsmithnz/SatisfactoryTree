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
            // TODO: Open add production item dialog
            if (string.IsNullOrWhiteSpace(txtProductionItem.Text))
            {
                MessageBox.Show("Please enter an item name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _productionService.AddProductionGoal(txtProductionItem.Text, txtProductionQuantity.Value, _selectedFactoryId);
                txtProductionItem.Clear();
                txtProductionQuantity.Value = 100;
                RefreshFactoryDetails();
                MessageBox.Show("Production goal added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding production goal: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditProductionItem_Click(object sender, EventArgs e)
        {
            // TODO: Open edit production item dialog
            MessageBox.Show("Edit Production Item functionality will be implemented.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                var factoryNode = new TreeNode($"{factory.Name} ({factory.Id})")
                {
                    Tag = factory.Id,
                    Name = factory.Id
                };

                // Add Production Goals section
                var goalsNode = new TreeNode("Production Goals");
                var activeGoals = factory.GetActiveGoals();
                foreach (var goal in activeGoals)
                {
                    var goalText = $"{goal.ItemName}: {goal.CurrentQuantity:N0}/{goal.TargetQuantity:N0} ({goal.ProgressPercentage:F1}%)";
                    var goalNode = new TreeNode(goalText) { Tag = goal };
                    goalsNode.Nodes.Add(goalNode);
                }
                factoryNode.Nodes.Add(goalsNode);

                // Add Storage section
                var storageNode = new TreeNode("Storage");
                foreach (var item in factory.Storage.GetAllItems())
                {
                    var itemText = $"{item.Key}: {item.Value:N0}";
                    var itemNode = new TreeNode(itemText) { Tag = item };
                    storageNode.Nodes.Add(itemNode);
                }
                factoryNode.Nodes.Add(storageNode);

                // Add Completed Goals section
                var completedNode = new TreeNode("Completed Goals");
                var completedGoals = factory.GetCompletedGoals();
                foreach (var goal in completedGoals)
                {
                    var goalText = $"{goal.ItemName}: {goal.TargetQuantity:N0} (Completed: {goal.CompletedDate?.ToString("MM/dd/yyyy")})";
                    var goalNode = new TreeNode(goalText) { Tag = goal };
                    completedNode.Nodes.Add(goalNode);
                }
                factoryNode.Nodes.Add(completedNode);

                treeFactoryDetails.Nodes.Add(factoryNode);
                factoryNode.Expand();
            }
        }
    }
}