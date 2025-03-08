namespace FarmingGame.Scripts.Commands
{
    public class LookCommand : BaseCommand, ICommand
    {
        public string Execute(Farmer farmer, string[] args)
        {
            return GetAreaDescription();
        }
    }
}
