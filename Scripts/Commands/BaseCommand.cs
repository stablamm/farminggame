namespace FarmingGame.Scripts.Commands
{
    public class BaseCommand
    {
        public string GetAreaDescription(FarmArea area)
        {
            string crops = area.Crops.Count > 0 ? "Growing here: " + string.Join(", ", area.Crops) : "Nothing is growing here yet.";
            string paths = $"Exits: {string.Join(", ", area.Paths.Keys)}";

            string areaDescription = area.Description;

            if (area.IsPlantable)
            {
                areaDescription += area.Crops.Count > 0 ? "\nGrowing here: " + string.Join(", ", area.Crops) : "\nNothing is growing here yet.";
            }

            areaDescription += $"\n{paths}";

            return areaDescription;
        }
    }
}
