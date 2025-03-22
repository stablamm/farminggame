using Godot;
using System;

namespace FarmingGame.Scenes.Levels
{
    public partial class GroundLayer : TileMapLayer
    {
        public Vector2I GetMouseCell() => LocalToMap(ToLocal(GetGlobalMousePosition()));

        public T GetCustomTileData<[MustBeVariant] T>(Vector2I cell, string customData)
        {
            TileData tileData = GetCellTileData(cell);
            if (tileData == null)
            {
                GD.PrintErr($"No tile data found at position {cell}");
                return default;
            }
            return tileData.GetCustomData(customData).As<T>();
        }
    }
}