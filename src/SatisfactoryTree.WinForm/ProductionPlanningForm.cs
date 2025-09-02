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
        private List<ProductionRowControl> _rowControls = new List<ProductionRowControl>();

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
            SetupScrolling();
        }

        private void SetupScrolling()
        {
            // Setup manual scrolling for the production items panel
            vScrollBar.ValueChanged += vScrollBar_Scroll;
            pnlProductionItems.Resize += PnlProductionItems_Resize;
        }

        private void PnlProductionItems_Resize(object? sender, EventArgs e)
        {
            // Update row control widths when panel resizes
            foreach (var rowControl in _rowControls)
            {
                rowControl.Width = pnlProductionItems.Width - 20;
            }
            UpdateScrollBar();
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
            // Handle selection of specific production items for editing - now handled by row controls
        }

        private void vScrollBar_Scroll(object? sender, EventArgs e)
        {
            // Move all row controls up/down based on scroll position
            int scrollOffset = -vScrollBar.Value;
            for (int i = 0; i < _rowControls.Count; i++)
            {
                _rowControls[i].Location = new Point(0, (i * 37) + scrollOffset);
            }
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
            // Clear existing row controls
            foreach (var control in _rowControls)
            {
                control.ProductionGoalChanged -= OnProductionGoalChanged;
                control.EditRequested -= OnEditRequested;
                control.Dispose();
            }
            _rowControls.Clear();
            pnlProductionItems.Controls.Clear();

            // Get the currently selected factory
            var selectedFactory = _productionService.GetAllFactories().FirstOrDefault(f => f.Id == _selectedFactoryId);
            if (selectedFactory == null) return;

            // Get all goals including dependencies 
            var allGoals = _productionService.GetAllGoalsIncludingDependencies(_selectedFactoryId);
            
            int yPosition = 0;
            const int rowHeight = 37; // Height of each row control (35 + 2 for margin)

            foreach (var goal in allGoals)
            {
                // Create new row control
                var rowControl = new ProductionRowControl(_productionService, _selectedFactoryId)
                {
                    ProductionGoal = goal,
                    Location = new Point(0, yPosition),
                    Width = pnlProductionItems.Width - 20, // Leave some margin for scrollbar
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };

                // Subscribe to events
                rowControl.ProductionGoalChanged += OnProductionGoalChanged;
                rowControl.EditRequested += OnEditRequested;

                _rowControls.Add(rowControl);
                pnlProductionItems.Controls.Add(rowControl);

                yPosition += rowHeight;
            }
            
            if (!allGoals.Any())
            {
                // Show empty state
                var emptyLabel = new Label
                {
                    Text = "No active production items",
                    Location = new Point(10, 10),
                    Size = new Size(pnlProductionItems.Width - 20, 30),
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.Gray
                };
                pnlProductionItems.Controls.Add(emptyLabel);
            }

            // Update scrollbar
            UpdateScrollBar();
        }

        private void UpdateScrollBar()
        {
            int totalHeight = _rowControls.Count * 37; // 37 = row height
            int visibleHeight = pnlProductionItems.Height;

            if (totalHeight > visibleHeight)
            {
                vScrollBar.Visible = true;
                vScrollBar.Maximum = totalHeight - visibleHeight + vScrollBar.LargeChange;
                vScrollBar.LargeChange = Math.Max(1, visibleHeight / 4);
                vScrollBar.SmallChange = 37; // One row height
            }
            else
            {
                vScrollBar.Visible = false;
                vScrollBar.Value = 0;
            }
        }

        private void OnProductionGoalChanged(object? sender, ProductionGoal goal)
        {
            // Handle production goal changes from row controls
            // Refresh the display to show updated calculations
            var senderControl = sender as ProductionRowControl;
            if (senderControl != null)
            {
                // Update just this control instead of full refresh for better performance
                // The row control itself handles most updates, but we might need to
                // refresh dependencies if they changed
                RefreshFactoryDetails();
            }
        }

        private void OnEditRequested(object? sender, ProductionGoal goal)
        {
            // Handle edit requests from row controls
            using (var dialog = new ProductionItemForm(_productionService, _selectedFactoryId))
            {
                // Pre-populate the dialog with current goal data
                // Note: The ProductionItemForm would need to be updated to support editing
                // For now, show the same add dialog
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Handle updates...
                    RefreshFactoryDetails();
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