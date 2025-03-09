using FarmingGame.Autoloads;
using System.Linq;

namespace FarmingGame.Scripts.Commands
{
    public class PickupCommand : ICommand
    {
        public string Execute(string[] args)
        {
            // Ensure the player specified what to pick up.
            if (args == null || args.Length == 0)
            {
                return "Pick up what?";
            }

            // Concatenate arguments to form the item name (e.g., "wheat seed").
            string targetItemName = string.Join(" ", args).ToLower();

            var playerPosition = AutoloadManager.Instance.GameManager.Player.Position;
            var playerCellId = AutoloadManager.Instance.GameManager.Map.GetMapCell((int)playerPosition.X, (int)playerPosition.Y);
            var cell = AutoloadManager.Instance.GameManager.Areas.AllAreas[playerCellId];
            var cellInventory = AutoloadManager.Instance.GameManager.Inventory.AllInventories[cell.InventoryId];
            var item = cellInventory.Items.FirstOrDefault(i => i.Item.Name.ToLower() == targetItemName);

            if (item == null)
            {
                return $"There is no {targetItemName} here to pick up.";
            }

            cellInventory.RemoveItem(item.Item);
            AutoloadManager.Instance.GameManager.Inventory.AllInventories[AutoloadManager.Instance.GameManager.Player.ID].AddItem(item.Item);

            return $"You picked up {item.Item.Name}";
        }
    }
}
