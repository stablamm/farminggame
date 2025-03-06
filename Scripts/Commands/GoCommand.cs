using FarmingGame.Autoloads;
using System.Collections.Generic;

namespace FarmingGame.Scripts.Commands
{
    public class GoCommand : BaseCommand, ICommand
    {
        public string Execute(Farmer farmer, string[] args, Dictionary<FARM_AREA, FarmArea> farmAreas)
        {
            if (args.Length == 0)
            {
                return "Go where?";
            }

            string direction = args[0];
            if (farmer.CurrentArea.Paths.ContainsKey(direction))
            {
                FARM_AREA nextId = (FARM_AREA)farmer.CurrentArea.Paths[direction];
                if (farmAreas.ContainsKey(nextId))
                {
                    farmer.CurrentArea = farmAreas[nextId];
                    return $"You walk to {farmer.CurrentArea.Name}.\n{GetAreaDescription(farmer.CurrentArea)}";
                }
                return "Path leads nowhere!";
            }

            return "No path that way!";
        }
    }
}