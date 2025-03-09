using FarmingGame.Autoloads;

namespace FarmingGame.Scripts.Commands
{
    public class PlantCommand : ICommand
    {
        public string Execute(string[] args)
        {
            if (args.Length == 0)
            {
                return "Plant what?";
            }

            var playerPos = AutoloadManager.Instance.GameManager.Player.Position;
            var playerAreaId = AutoloadManager.Instance.GameManager.Map.GetMapCell((int)playerPos.X, (int)playerPos.Y);
            var area = AutoloadManager.Instance.GameManager.Areas.AllAreas[playerAreaId];
            if (!area.IsPlantable)
            {
                return "You can't plant here!";
            }

            string targetItemName = string.Join(" ", args).ToLower();
            if (targetItemName == "wheat seed")
            {
                if (area.Crops.Contains("wheat"))
                {
                    return "Wheat is already planted here.";
                }

                var playerInventory = AutoloadManager.Instance.GameManager.Inventory.AllInventories[AutoloadManager.Instance.GameManager.Player.ID];
                for (int i = 0; i < playerInventory.Items.Count; i++)
                {
                    var item = playerInventory.Items[i];
                    if (item.Item.Name.ToLower() == targetItemName)
                    {
                        playerInventory.RemoveItem(item.Item);
                        area.Crops.Add("wheat");

                        return "You planted wheat seeds in the field.";
                    }
                }
            }

            return "Nothing happened.";
        }
    }
}
