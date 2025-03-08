using FarmingGame.Scripts;
using System.Collections.Generic;

namespace FarmingGame.Autoloads.GameManagers
{
    public class AreaManager
    {
        public Dictionary<string, FarmArea> AllAreas { get; private set; } = new();

        public void AddNewArea(string id, FarmArea area)
        {
            if (!AllAreas.ContainsKey(id))
            {
                AllAreas.Add(id, area);
            }
        }
    }
}
