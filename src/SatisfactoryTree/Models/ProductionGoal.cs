namespace SatisfactoryTree.Models
{
    public class ProductionGoal
    {
        public ProductionGoal(string itemName, decimal targetQuantity, string factoryId = "default")
        {
            Id = Guid.NewGuid().ToString();
            ItemName = itemName;
            TargetQuantity = targetQuantity;
            CurrentQuantity = 0;
            FactoryId = factoryId;
            CreatedDate = DateTime.Now;
            IsCompleted = false;
        }

        public string Id { get; set; }
        public string ItemName { get; set; }
        public decimal TargetQuantity { get; set; }
        public decimal CurrentQuantity { get; set; }
        public string FactoryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public bool IsCompleted { get; set; }

        public decimal ProgressPercentage => TargetQuantity > 0 ? (CurrentQuantity / TargetQuantity) * 100 : 0;

        public void UpdateProgress(decimal quantity)
        {
            CurrentQuantity = Math.Min(CurrentQuantity + quantity, TargetQuantity);
            if (CurrentQuantity >= TargetQuantity && !IsCompleted)
            {
                IsCompleted = true;
                CompletedDate = DateTime.Now;
            }
        }

        public decimal GetRemainingQuantity()
        {
            return Math.Max(0, TargetQuantity - CurrentQuantity);
        }
    }
}