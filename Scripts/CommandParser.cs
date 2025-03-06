using FarmingGame.Autoloads;
using FarmingGame.Scripts.Commands;
using System.Collections.Generic;

namespace FarmingGame.Scripts
{
    public static class CommandParser
    {
        private static readonly Dictionary<string, ICommand> Commands = new()
        {
            { "go", new GoCommand() },
            { "look", new LookCommand() },
            { "plant", new PlantCommand() },
            { "get", new GetCommand() },
            { "store", new StoreCommand() },
            { "inventory", new InventoryCommand() }
        };

        public static string Parse(string input, Farmer farmer, Dictionary<FARM_AREA, FarmArea> farmAreas)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "What do you want to do?";
            }

            var parts = input.ToLower().Trim().Split(' ');
            string command = parts[0];
            string argument = parts.Length > 1 ? string.Join(" ", parts[1..]) : "";
            string[] args = parts.Length > 1 ? parts[1..] : new string[0];

            if (Commands.TryGetValue(command, out ICommand c))
            {
                return c.Execute(farmer, args, farmAreas);
            }

            return "Huh? Try 'look', 'go <direction>', 'plant wheat', 'get water', 'store wheat', or 'inventory'.";
        }

        private static string GetAreaDescription(FarmArea area)
        {
            string crops = area.Crops.Count > 0 ? " Growing here: " + string.Join(", ", area.Crops) : " Nothing is growing here yet.";
            return $"{area.Description}{crops}";
        }
    }
}