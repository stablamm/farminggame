using FarmingGame.Autoloads;
using System.Collections.Generic;

namespace FarmingGame.Scripts.Commands
{
    public interface ICommand 
    {
        string Execute(Farmer farmer, string[] args, Dictionary<FARM_AREA, FarmArea> farmAreas);
    }
}