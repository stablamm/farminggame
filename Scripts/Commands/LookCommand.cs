using FarmingGame.Autoloads;
using System.Collections.Generic;

namespace FarmingGame.Scripts.Commands
{
    public class LookCommand : BaseCommand, ICommand
    {
        public string Execute(Farmer farmer, string[] args, Dictionary<FARM_AREA, FarmArea> farmAreas)
        {
            return GetAreaDescription(farmer.CurrentArea);
        }
    }
}
