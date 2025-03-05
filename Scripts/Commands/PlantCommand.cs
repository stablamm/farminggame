using System.Collections.Generic;

namespace FarmingGame.Scripts.Commands
{
    public class PlantCommand : ICommand
    {
        public string Execute(Farmer farmer, string[] args, Dictionary<int, FarmArea> farmAreas)
        {
            if (!farmer.CurrentArea.IsPlantable)
            {
                return "You can't plant here!";
            }
            if (args[0] != "wheat")
            {
                return "You can only plant wheat for now!";
            }
            if (!farmer.Inventory.Contains("wheat seeds"))
            {
                return "You don’t have wheat seeds!";
            }
            if (farmer.CurrentArea.Crops.Contains("wheat"))
            {
                return "Wheat is already planted here!";
            }
            
            farmer.CurrentArea.Crops.Add("wheat");
            return "You plant wheat in the field.";
        }
    }
}
