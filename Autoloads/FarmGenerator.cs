using FarmingGame.Scripts;
using Godot;
using System.Collections.Generic;

namespace FarmingGame.Autoloads
{
    public partial class FarmGenerator : Node
    {
        public const string NODE_PATH = "/root/FarmGenerator";

        [Export]
        private string farmLayoutPath = "res://Assets/JSON/farm_layout.json";

        public Dictionary<int, FarmArea> FarmAreas { get; private set; } = new Dictionary<int, FarmArea>();

        public override void _Ready()
        {
            LoadFarmLayout();
            GeneratePaths();
        }

        private void LoadFarmLayout()
        {
            FileAccess file = FileAccess.Open(farmLayoutPath, FileAccess.ModeFlags.Read);
            if (file == null)
            {
                GD.PrintErr($"Failed to load {farmLayoutPath}");
                return;
            }

            string jsonText = file.GetAsText();
            file.Close();

            var json = Json.ParseString(jsonText);
            if (json.VariantType == Variant.Type.Nil)
            {
                GD.PrintErr("Failed to parse farm_layout.json");
                return;
            }

            var farmData = json.AsGodotDictionary();
            var areasArray = farmData["areas"].AsGodotArray();

            foreach (var areaData in areasArray)
            {
                var areaDict = areaData.AsGodotDictionary();
                var area = new FarmArea
                {
                    Id = (int)(float)areaDict["id"],
                    Name = areaDict["name"].ToString(),
                    Description = areaDict["description"].ToString(),
                    IsPlantable = (bool)areaDict["isPlantable"],
                    HasWater = (bool)areaDict["hasWater"],
                    IsStorage = (bool)areaDict["isStorage"]
                };
                FarmAreas[area.Id] = area;
            }
        }

        private void GeneratePaths()
        {
            if (FarmAreas.ContainsKey(0) && FarmAreas.ContainsKey(1))
            {
                FarmAreas[0].Paths["north"] = 1; 
            }
            if (FarmAreas.ContainsKey(1))
            {
                FarmAreas[1].Paths["south"] = 0; 
                FarmAreas[1].Paths["north"] = 2; 
            }
            if (FarmAreas.ContainsKey(2))
            {
                FarmAreas[2].Paths["south"] = 1; 
            }
        }

        public FarmArea GetArea(int id)
        {
            return FarmAreas.ContainsKey(id) ? FarmAreas[id] : null;
        }
    }
}