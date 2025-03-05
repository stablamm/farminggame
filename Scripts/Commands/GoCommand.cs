using System.Collections.Generic;

namespace FarmingGame.Scripts.Commands
{
    public class GoCommand : ICommand
    {
        public string Execute(Farmer farmer, string[] args, Dictionary<int, FarmArea> farmAreas)
        {
            if (args.Length == 0)
            {
                return "Go where?";
            }

            string direction = args[0];
            if (farmer.CurrentArea.Paths.ContainsKey(direction))
            {
                int nextId = farmer.CurrentArea.Paths[direction];
                if (farmAreas.ContainsKey(nextId))
                {
                    farmer.CurrentArea = farmAreas[nextId];
                    return $"You walk to {farmer.CurrentArea.Name}.\n{GetAreaDescription(farmer.CurrentArea)}";
                }
                return "Path leads nowhere!";
            }

            return "No path that way!";
        }

        private string GetAreaDescription(FarmArea area)
        {
            string crops = area.Crops.Count > 0 ? " Growing here: " + string.Join(", ", area.Crops) : " Nothing is growing here yet.";
            return $"{area.Description}{crops}";
        }
    }
}