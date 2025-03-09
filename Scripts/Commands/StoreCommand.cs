namespace FarmingGame.Scripts.Commands
{
    public class StoreCommand : ICommand
    {
        public string Execute(string[] args)
        {
            //if (!farmer.CurrentArea.IsStorage)
            //{
            //    return "You can’t store anything here!";
            //}
            //if (args[1] != "wheat")
            //{
            //    return "You can only store wheat for now!";
            //}
            //if (!farmer.Inventory.HasItem("wheat"))
            //{
            //    return "You don’t have any wheat to store!";
            //}
            
            //farmer.Inventory.RemoveItem("wheat");
            return "You store the wheat in the barn.";
        }
    }
}
