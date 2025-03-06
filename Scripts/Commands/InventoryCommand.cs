using FarmingGame.Autoloads;
using System.Collections.Generic;

namespace FarmingGame.Scripts.Commands
{
    public class InventoryCommand : ICommand
    {
        public string Execute(Farmer farmer, string[] args, Dictionary<FARM_AREA, FarmArea> farmAreas)
        {
            string items = farmer.Inventory.ToString();
            return $"You’re carrying: {items}.";
        }
    }
}
