using FarmingGame.Autoloads;
using FarmingGame.Scripts.Items;

namespace FarmingGame.Scripts.Commands
{
    public class FillCommand : ICommand
    {
        public string Execute(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                return "Fill what?";
            }

            string targetItemName = string.Join(" ", args).ToLower();

            if (targetItemName == "watering can")
            {
                var playerPosition = AutoloadManager.Instance.GameManager.Player.Position;
                var playerAreaId = AutoloadManager.Instance.GameManager.Map.GetMapCell((int)playerPosition.X, (int)playerPosition.Y);
                var area = AutoloadManager.Instance.GameManager.Areas.AllAreas[playerAreaId];
                if (!area.HasWater)
                {
                    return "There's no water here to fill the watering can.";
                }

                var playerInventory = AutoloadManager.Instance.GameManager.Inventory.AllInventories[AutoloadManager.Instance.GameManager.Player.ID];
                foreach (var item in playerInventory.Items)
                {
                    if (item.Item.GetType() == typeof(WateringCan))
                    {
                        var wc = item.Item as WateringCan;
                        if (!wc.IsFull)
                        {
                            wc.Fill();
                            return "Watering can filled.";
                        }
                    }
                }
            }

            return "Nothing happened.";
        }
    }
}
