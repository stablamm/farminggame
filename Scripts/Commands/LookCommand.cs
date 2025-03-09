namespace FarmingGame.Scripts.Commands
{
    public class LookCommand : BaseCommand, ICommand
    {
        public string Execute(string[] args)
        {
            return GetAreaDescription();
        }
    }
}
