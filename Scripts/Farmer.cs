using System.Collections.Generic;

namespace FarmingGame.Scripts
{
    public partial class Farmer
    {
        public string Name { get; set; }
        public bool HasWater { get; set; }
        public FarmArea CurrentArea { get; set; }
        public List<string> Inventory { get; set; } = new List<string>();
    }
}