using System.Collections.Generic;

namespace FarmingGame.Scripts.Commands
{
    public class LookCommand : ICommand
    {
        public string Execute(Farmer farmer, string[] args, Dictionary<int, FarmArea> farmAreas)
        {
            return GetAreaDescription(farmer.CurrentArea);
        }

        private string GetAreaDescription(FarmArea area)
        {
            string crops = area.Crops.Count > 0 ? " Growing here: " + string.Join(", ", area.Crops) : " Nothing is growing here yet.";
            return $"{area.Description}{crops}";
        }
    }
}
