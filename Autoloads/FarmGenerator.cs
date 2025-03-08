using FarmingGame.Scripts;
using Godot;

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

        public override void _Ready()
        {
            LoadFarmLayout();
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
                    Id = (FARM_AREA)(int)(float)areaDict["id"],
                    Name = areaDict["name"].ToString(),
                    Description = areaDict["description"].ToString(),
                    IsPlantable = (bool)areaDict["isPlantable"],
                    HasWater = (bool)areaDict["hasWater"],
                    IsStorage = (bool)areaDict["isStorage"]
                };
                area.Instantiate();
                AutoloadManager.Instance.GameManager.Areas.AddNewArea(area.AreaId, area);
            }
        }
    }
}