using FarmingGame.Autoloads;

namespace FarmingGame.Scripts.Commands
{
    public class InventoryCommand : ICommand
    {
        public string Execute(string[] args)
        {
            string items = AutoloadManager.Instance.GameManager.Inventory.AllInventories[AutoloadManager.Instance.GameManager.Player.ID].ToString();
            return $"You’re carrying: {items}.";
        }
    }
}
