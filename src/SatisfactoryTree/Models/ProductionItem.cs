﻿namespace SatisfactoryTree.Models
{
    public class ProductionItem
    {
        public ProductionItem(Item? item, Building? building, decimal quantity)
        {
            Item = item;
            Building = building;
            Quantity = quantity;
            Parents = new();
        }
        public Item? Item { get; set; }
        public Building? Building { get; set; }
        public decimal Quantity { get; set; }
        public List<ProductionItem> Parents { get; set; }
    }
}