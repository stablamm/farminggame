using FarmingGame.Scripts;
using Godot;
using System.Collections.Generic;

namespace FarmingGame.Autoloads
{
    public enum FARM_AREA
    {
        FIELD,
        STONE_WELL,
        OLD_BARN
    }

    public partial class FarmGenerator : Node
    {
        public const string NODE_PATH = "/root/FarmGenerator";

        private string farmLayoutPath = "res://Assets/JSON/farm_layout.json";

        public Dictionary<FARM_AREA, FarmArea> FarmAreas { get; private set; } = new Dictionary<FARM_AREA, FarmArea>();

        public override void _Ready()
        {
            LoadFarmLayout();
            GeneratePaths();
        }

        public FarmArea GetArea(FARM_AREA id)
        {
            return FarmAreas.ContainsKey(id) ? FarmAreas[id] : null;
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
                FarmAreas[(FARM_AREA)area.Id] = area;
            }
        }

        private void GeneratePaths()
        {
            if (FarmAreas.ContainsKey(FARM_AREA.FIELD) && FarmAreas.ContainsKey(FARM_AREA.STONE_WELL))
            {
                FarmAreas[FARM_AREA.FIELD].Paths["north"] = (int)FARM_AREA.STONE_WELL; 
            }
            if (FarmAreas.ContainsKey(FARM_AREA.STONE_WELL))
            {
                FarmAreas[FARM_AREA.STONE_WELL].Paths["south"] = (int)FARM_AREA.FIELD; 
                FarmAreas[FARM_AREA.STONE_WELL].Paths["north"] = (int)FARM_AREA.OLD_BARN; 
            }
            if (FarmAreas.ContainsKey(FARM_AREA.OLD_BARN))
            {
                FarmAreas[FARM_AREA.OLD_BARN].Paths["south"] = (int)FARM_AREA.STONE_WELL; 
            }
        }
    }
}