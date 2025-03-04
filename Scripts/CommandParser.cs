using System.Collections.Generic;

namespace FarmingGame.Scripts
{
    public static class CommandParser
    {
        public static string Parse(string input, Farmer farmer, Dictionary<int, FarmArea> farmAreas)
        {
            if (string.IsNullOrEmpty(input)) return "What do you want to do?";

            var parts = input.ToLower().Trim().Split(' ');
            string command = parts[0];
            string argument = parts.Length > 1 ? string.Join(" ", parts[1..]) : "";

            switch (command)
            {
                case "go":
                    if (farmer.CurrentArea.Paths.ContainsKey(argument))
                    {
                        int nextId = farmer.CurrentArea.Paths[argument];
                        if (farmAreas.ContainsKey(nextId))
                        {
                            farmer.CurrentArea = farmAreas[nextId];
                            return $"You walk to {farmer.CurrentArea.Name}.\n{GetAreaDescription(farmer.CurrentArea)}";
                        }
                        return "Path leads nowhere!";
                    }
                    return "No path that way!";

                case "look":
                    return GetAreaDescription(farmer.CurrentArea);

                case "plant":
                    if (!farmer.CurrentArea.IsPlantable)
                        return "You can't plant here!";
                    if (argument != "wheat")
                        return "You can only plant wheat for now!";
                    if (!farmer.Inventory.Contains("wheat seeds"))
                        return "You don’t have wheat seeds!";
                    if (farmer.CurrentArea.Crops.Contains("wheat"))
                        return "Wheat is already planted here!";
                    farmer.CurrentArea.Crops.Add("wheat");
                    return "You plant wheat in the field.";

                case "get":
                    if (argument == "water")
                    {
                        if (!farmer.CurrentArea.HasWater)
                            return "There’s no water here!";
                        if (farmer.HasWater)
                            return "You’re already carrying water!";
                        farmer.HasWater = true;
                        return "You fetch water from the well.";
                    }
                    return "Get what? Try 'get water'.";

                case "store":
                    if (!farmer.CurrentArea.IsStorage)
                        return "You can’t store anything here!";
                    if (argument != "wheat")
                        return "You can only store wheat for now!";
                    if (!farmer.Inventory.Contains("wheat"))
                        return "You don’t have any wheat to store!";
                    farmer.Inventory.Remove("wheat");
                    return "You store the wheat in the barn.";

                case "inventory":
                    string items = farmer.Inventory.Count > 0 ? string.Join(", ", farmer.Inventory) : "nothing";
                    string water = farmer.HasWater ? " and some water" : "";
                    return $"You’re carrying: {items}{water}.";

                default:
                    return "Huh? Try 'look', 'go <direction>', 'plant wheat', 'get water', 'store wheat', or 'inventory'.";
            }
        }

        private static string GetAreaDescription(FarmArea area)
        {
            string crops = area.Crops.Count > 0 ? " Growing here: " + string.Join(", ", area.Crops) : " Nothing is growing here yet.";
            return $"{area.Description}{crops}";
        }
    }
}