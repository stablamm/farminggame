using FarmingGame.Autoloads;
using System;

namespace FarmingGame.Scripts
{
    public class Farmer
    {
        public string ID { get; set; } = Guid.NewGuid().ToString();

        public void Instantiate()
        {
            AutoloadManager.Instance.GameManager.Inventory.CreateNewInventory(ID);
            AutoloadManager.Instance.GameManager.Player.ID = ID;
        }

        public Inventory GetInventory() => AutoloadManager.Instance.GameManager.Inventory.AllInventories[ID];
}
}