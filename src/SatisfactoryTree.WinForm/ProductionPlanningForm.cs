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
            RefreshActiveGoals();
            RefreshCompletedGoals();
            RefreshStorage();
        }

        private void LoadFactories()
        {
            cmbFactories.Items.Clear();
            cmbProductionFactory.Items.Clear();

            foreach (var factory in _productionService.GetAllFactories())
            {
                var displayText = $"{factory.Name} ({factory.Id})";
                cmbFactories.Items.Add(new ComboBoxItem { Text = displayText, Value = factory.Id });
                cmbProductionFactory.Items.Add(new ComboBoxItem { Text = displayText, Value = factory.Id });
            }

            if (cmbFactories.Items.Count > 0)
            {
                cmbFactories.SelectedIndex = 0;
                cmbProductionFactory.SelectedIndex = 0;
            }
        }

        private void cmbFactories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFactories.SelectedItem is ComboBoxItem selectedItem)
            {
                _selectedFactoryId = selectedItem.Value;
                RefreshActiveGoals();
                RefreshStorage();
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
                MessageBox.Show("Factory added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding factory: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddGoal_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtItemName.Text))
            {
                MessageBox.Show("Please enter an item name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _productionService.AddProductionGoal(txtItemName.Text, txtTargetQuantity.Value, _selectedFactoryId);
                txtItemName.Clear();
                txtTargetQuantity.Value = 100;
                RefreshActiveGoals();
                MessageBox.Show("Production goal added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding production goal: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProcessProduction_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductionItem.Text))
            {
                MessageBox.Show("Please enter an item name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbProductionFactory.SelectedItem is not ComboBoxItem selectedFactory)
            {
                MessageBox.Show("Please select a factory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _productionService.ProcessProduction(txtProductionItem.Text, txtProductionQuantity.Value, selectedFactory.Value);
                txtProductionItem.Clear();
                txtProductionQuantity.Value = 10;
                RefreshActiveGoals();
                RefreshCompletedGoals();
                RefreshStorage();
                MessageBox.Show("Production processed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing production: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshActiveGoals()
        {
            lstActiveGoals.Items.Clear();

            var factory = _productionService.GetFactory(_selectedFactoryId);
            if (factory == null) return;

            var activeGoals = factory.GetActiveGoals();
            foreach (var goal in activeGoals)
            {
                var item = new ListViewItem(goal.ItemName);
                item.SubItems.Add(goal.TargetQuantity.ToString("N0"));
                item.SubItems.Add(goal.CurrentQuantity.ToString("N0"));
                item.SubItems.Add(goal.ProgressPercentage.ToString("N1") + "%");
                item.SubItems.Add(factory.Name);
                item.Tag = goal;
                lstActiveGoals.Items.Add(item);
            }
        }

        private void RefreshCompletedGoals()
        {
            lstCompletedGoals.Items.Clear();

            var completedGoals = _productionService.GetAllCompletedGoals();
            foreach (var goal in completedGoals)
            {
                var factory = _productionService.GetFactory(goal.FactoryId);
                var item = new ListViewItem(goal.ItemName);
                item.SubItems.Add(goal.TargetQuantity.ToString("N0"));
                item.SubItems.Add(goal.CompletedDate?.ToString("yyyy-MM-dd HH:mm") ?? "");
                item.SubItems.Add(factory?.Name ?? "Unknown");
                item.Tag = goal;
                lstCompletedGoals.Items.Add(item);
            }
        }

        private void RefreshStorage()
        {
            lstStorage.Items.Clear();

            foreach (var factory in _productionService.GetAllFactories())
            {
                foreach (var storageItem in factory.Storage.GetAllItems())
                {
                    var item = new ListViewItem(storageItem.Key);
                    item.SubItems.Add(storageItem.Value.ToString("N0"));
                    item.SubItems.Add(factory.Name);
                    lstStorage.Items.Add(item);
                }
            }
        }

        private class ComboBoxItem
        {
            public string Text { get; set; } = "";
            public string Value { get; set; } = "";

            public override string ToString()
            {
                return Text;
            }
        }
    }
}