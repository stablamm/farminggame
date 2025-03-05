using System.Collections.Generic;

namespace FarmingGame.Scripts.Commands
{
    public class StoreCommand : ICommand
    {
        public string Execute(Farmer farmer, string[] args, Dictionary<int, FarmArea> farmAreas)
        {
            if (!farmer.CurrentArea.IsStorage)
            {
                return "You can’t store anything here!";
            }
            if (args[1] != "wheat")
            {
                return "You can only store wheat for now!";
            }
            if (!farmer.Inventory.Contains("wheat"))
            {
                return "You don’t have any wheat to store!";
            }
            
            farmer.Inventory.Remove("wheat");
            return "You store the wheat in the barn.";
        }
    }
}
