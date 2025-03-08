namespace FarmingGame.Scripts.Commands
{
    public interface ICommand 
    {
        string Execute(Farmer farmer, string[] args);
    }
}