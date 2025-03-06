using FarmingGame.Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FarmingGame.Scripts
{
    public class Inventory
    {
        private List<InventoryItem> items = new List<InventoryItem>();

        public IReadOnlyList<InventoryItem> Items => items.AsReadOnly();

        public void AddItem(Item item, int quantity = 1)
        {
            if (item.IsStackable)
            {
                var existing = items.FirstOrDefault(i => i.Item.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase));
                if (existing != null)
                {
                    existing.Quantity = Math.Min(existing.Quantity + quantity, item.MaxStack);
                    return;
                }
            }

            items.Add(new InventoryItem(item, quantity));
        }

        public bool RemoveItem(string itemName, int quantity = 1)
        {
            var invItem = items.FirstOrDefault(i => i.Item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
            if (invItem == null)
            {
                return false;
            }
            
            if (invItem.Quantity > quantity)
            {
                invItem.Quantity -= quantity;
            }
            else if (invItem.Quantity == quantity)
            {
                items.Remove(invItem);
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool HasItem(string itemName, int quantity = 1)
        {
            var invItem = items.FirstOrDefault(i => i.Item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
            return invItem != null && invItem.Quantity >= quantity;
        }

        public override string ToString()
        {
            if (items.Count == 0)
            {
                return "nothing";
            }

            List<string> itemDescriptions = new List<string>();
            foreach (var invItem in items)
            {
                itemDescriptions.Add($"{invItem.Item.Name} (x{invItem.Quantity})");
            }
            
            return string.Join(", ", itemDescriptions);
        }
    }
}
