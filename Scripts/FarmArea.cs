using System.Collections.Generic;

namespace FarmingGame.Scripts
{
    public partial class FarmArea
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPlantable { get; set; }
        public bool HasWater { get; set; }
        public bool IsStorage { get; set; }
        public Dictionary<string, int> Paths { get; set; } = new Dictionary<string, int>();
        public List<string> Crops { get; set; } = new List<string>();
    }
}