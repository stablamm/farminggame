using FarmingGame.Scripts;
using System.Collections.Generic;

namespace FarmingGame.Autoloads.GameManagers
{
    public class InventoryManager
    {
        /// <summary>
        /// Holds every inventory in the game.
        /// </summary>
        public Dictionary<string, Inventory> AllInventories { get; private set; } = new();

        public void CreateNewInventory(string id)
        {
            if (!AllInventories.ContainsKey(id))
            {
                AllInventories.Add(id, new Inventory());
            }
        }
    }
}
