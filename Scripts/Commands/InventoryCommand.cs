using System.Collections.Generic;

namespace FarmingGame.Scripts.Commands
{
    public class InventoryCommand : ICommand
    {
        public string Execute(Farmer farmer, string[] args, Dictionary<int, FarmArea> farmAreas)
        {
            string items = farmer.Inventory.Count > 0 ? string.Join(", ", farmer.Inventory) : "nothing";
            string water = farmer.HasWater ? " and some water" : "";

            return $"You’re carrying: {items}{water}.";
        }
    }
}
