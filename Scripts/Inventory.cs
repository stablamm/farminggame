using FarmingGame.Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FarmingGame.Scripts
{
    /// <summary>
    /// Represents the player's inventory, managing items and their quantities.
    /// </summary>
    public class Inventory
    {
        /// <summary>
        /// ID related to this specific inventory item.
        /// </summary>
        public string ID { get; private set; } = Guid.NewGuid().ToString();

        // Internal list storing inventory items.
        private List<InventoryItem> items = new List<InventoryItem>();

        /// <summary>
        /// Provides the inventory items.
        /// </summary>
        public List<InventoryItem> Items => items;

        /// <summary>
        /// Sets the ID of the inventory.
        /// </summary>
        /// <param name="id">New Inventory ID</param>
        public void SetInventoryID(string id) => ID = id; 

        /// <summary>
        /// Adds an item to the inventory.
        /// If the item is stackable and already exists, it increases the quantity (up to MaxStack).
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <param name="quantity">The quantity to add (default is 1).</param>
        public void AddItem(Item item, int quantity = 1)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (item.IsStackable)
            {
                var existing = items.FirstOrDefault(i => i.Item.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase));
                if (existing != null)
                {
                    int newQuantity = existing.Quantity + quantity;
                    // If new quantity exceeds MaxStack, cap it at MaxStack.
                    existing.Quantity = Math.Min(newQuantity, item.MaxStack);
                    return;
                }
            }

            items.Add(new InventoryItem(item, quantity));
        }

        /// <summary>
        /// Removes an item from the inventory by item name.
        /// </summary>
        /// <param name="itemName">The name of the item to remove.</param>
        /// <param name="quantity">The quantity to remove (default is 1).</param>
        /// <returns>True if removal was successful; otherwise false.</returns>
        public bool RemoveItem(string itemName, int quantity = 1)
        {
            if (string.IsNullOrWhiteSpace(itemName))
                throw new ArgumentException("Item name cannot be null or whitespace.", nameof(itemName));

            var invItem = items.FirstOrDefault(i => i.Item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
            if (invItem == null)
                return false;

            return RemoveInventoryItem(invItem, quantity);
        }

        /// <summary>
        /// Removes an item from the inventory by item reference.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <param name="quantity">The quantity to remove (default is 1).</param>
        /// <returns>True if removal was successful; otherwise false.</returns>
        public bool RemoveItem(Item item, int quantity = 1)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var invItem = items.FirstOrDefault(i => i.Item.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase));
            if (invItem == null)
                return false;

            return RemoveInventoryItem(invItem, quantity);
        }

        /// <summary>
        /// Helper method to remove a specified quantity from an InventoryItem.
        /// </summary>
        /// <param name="invItem">The inventory item to update.</param>
        /// <param name="quantity">The quantity to remove.</param>
        /// <returns>True if successful; otherwise false.</returns>
        private bool RemoveInventoryItem(InventoryItem invItem, int quantity)
        {
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
                // Not enough quantity to remove.
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if the inventory contains at least the specified quantity of an item by name.
        /// </summary>
        /// <param name="itemName">The name of the item to check.</param>
        /// <param name="quantity">The required quantity (default is 1).</param>
        /// <returns>True if found; otherwise false.</returns>
        public bool HasItem(string itemName, int quantity = 1)
        {
            if (string.IsNullOrWhiteSpace(itemName))
                throw new ArgumentException("Item name cannot be null or whitespace.", nameof(itemName));

            var invItem = items.FirstOrDefault(i => i.Item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
            return invItem != null && invItem.Quantity >= quantity;
        }

        /// <summary>
        /// Returns a string representation of the inventory, listing items and their quantities.
        /// </summary>
        /// <returns>A comma-separated string of items, or "nothing" if empty.</returns>
        public override string ToString()
        {
            if (items.Count == 0)
                return "nothing";

            List<string> itemDescriptions = new List<string>();
            foreach (var invItem in items)
            {
                itemDescriptions.Add($"{invItem.Item.ToString()} (x{invItem.Quantity})");
            }

            return string.Join(", ", itemDescriptions);
        }
    }
}
