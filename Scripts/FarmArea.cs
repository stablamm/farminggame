using FarmingGame.Autoloads;
using FarmingGame.Scripts.Items;
using System;
using System.Collections.Generic;

namespace FarmingGame.Scripts
{
    public class FarmArea
    {
        public FARM_AREA Id { get; set; }
        public string InventoryId { get; set; } = Guid.NewGuid().ToString();
        public string AreaId { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPlantable { get; set; }
        public bool HasWater { get; set; }
        public bool IsStorage { get; set; }
        public List<string> Crops { get; set; } = new();

        private bool isInstantiated = false;

        public void Instantiate()
        {
            try
            {
                AutoloadManager.Instance.GameManager.Inventory.CreateNewInventory(InventoryId);
                isInstantiated = true;
            }
            catch(Exception ex)
            {
                _ = ex;
                isInstantiated = false;
            }
        }

        public void AddItem(Item item, int quantity = 1)
        {
            if (!isInstantiated) Instantiate();
            AutoloadManager.Instance.GameManager.Inventory.AllInventories[InventoryId].AddItem(item, quantity);
        }

        public void RemoveItem(Item item, int quantity = 1)
        {
            if (!isInstantiated) Instantiate();
            AutoloadManager.Instance.GameManager.Inventory.AllInventories[InventoryId].RemoveItem(item, quantity);
        }
    }
}