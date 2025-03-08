using FarmingGame.Autoloads;
using System;

namespace FarmingGame.Scripts.Commands
{
    public class BaseCommand
    {
        public string GetAreaDescription()
        {
            var playerPosition = AutoloadManager.Instance.GameManager.Player.Position;
            var playerCellId = AutoloadManager.Instance.GameManager.Map.GetMapCell((int)playerPosition.X, (int)playerPosition.Y);
            var cell = AutoloadManager.Instance.GameManager.Areas.AllAreas[playerCellId];
            var loots = AutoloadManager.Instance.GameManager.Inventory.AllInventories[cell.InventoryId];
            var exits = AutoloadManager.Instance.GameManager.Map.GetExits((int)playerPosition.X, (int) playerPosition.Y);

            var areaDescription = cell.Description;

            if (loots.Items.Count > 0)
            { 
                string lootString = "Items in area: \n";

                foreach (var loot in loots.Items)
                {
                    lootString += loot.Item.Name + Environment.NewLine;
                }

                areaDescription += Environment.NewLine + lootString;
            }

            string exitString = "Exits: ";

            if (exits.north) exitString += "north, ";
            if (exits.east) exitString += "east, ";
            if (exits.south) exitString += "south, ";
            if (exits.west) exitString += "west, ";

            areaDescription += Environment.NewLine + exitString.TrimEnd().Trim(',');

            return areaDescription;
        }
    }
}
