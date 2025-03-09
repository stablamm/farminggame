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
            { "inventory", new InventoryCommand() },
            { "pickup", new PickupCommand() },
            { "take", new PickupCommand() },
            { "fill", new FillCommand() },
            { "water", new WaterCommand() },
        };

        public static string Parse(string input)
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
                return c.Execute(args);
            }

            return "Huh? Try 'look', 'go <direction>', 'plant wheat', 'get water', 'store wheat', 'pickup wheat seed', or 'inventory'.";
        }
    }
}