namespace FarmingGame.Scripts.Commands
{
    public class InventoryCommand : ICommand
    {
        public string Execute(Farmer farmer, string[] args)
        {
            string items = farmer.GetInventory().ToString();
            return $"You’re carrying: {items}.";
        }
    }
}
