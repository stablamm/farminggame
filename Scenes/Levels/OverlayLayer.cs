using Godot;

namespace FarmingGame.Scenes.Levels
{
    public partial class OverlayLayer : TileMapLayer
    {
        // Direct reference to the ground layer (which should have tiles with a custom "IsSoil" property).
        [Export]
        public TileMapLayer GroundLayer;

        private Vector2I previousCell = new Vector2I(-1, -1);
        private bool hoverActive = false;

        public override void _Ready()
        {
            SetProcessInput(true);
            SetProcess(true);
            ZIndex = 999;
        }

        public override void _Input(InputEvent @event)
        {
            // Toggle hover effect when the Tab key is pressed (ignoring repeats).
            if (@event is InputEventKey keyEvent && keyEvent.Pressed && !keyEvent.Echo)
            {
                if (keyEvent.Keycode == Key.Tab)
                {
                    hoverActive = !hoverActive;
                    GD.Print("Hover toggled: " + hoverActive);

                    // If turning off, clear any previously highlighted cell.
                    if (!hoverActive && previousCell != new Vector2I(-1, -1))
                    {
                        SetCell(previousCell, -1, Vector2I.Zero);
                        previousCell = new Vector2I(-1, -1);
                    }
                }
            }
        }

        public override void _Process(double delta)
        {
            if (!hoverActive)
                return;

            Vector2 localPos = ToLocal(GetGlobalMousePosition());
            Vector2I cell = LocalToMap(localPos);

            if (GroundLayer != null)
            {
                int sourceId = GroundLayer.GetCellSourceId(cell);
                if (sourceId == -1)
                {
                    ClearPreviousCell();
                    return;
                }

                Vector2I atlasCoords = GroundLayer.GetCellAtlasCoords(cell);
                int alternative = GroundLayer.GetCellAlternativeTile(cell);

                TileSet tileSet = GroundLayer.TileSet;
                if (tileSet != null)
                {
                    TileSetSource source = tileSet.GetSource(sourceId);
                    if (source != null)
                    {
                        if (source is TileSetAtlasSource atlasSource)
                        {
                            TileData tileData = atlasSource.GetTileData(atlasCoords, alternative);
                            if (tileData != null)
                            {
                                var isSoil = (bool)tileData.GetCustomData("IsSoil");
                                var isWater = (bool)tileData.GetCustomData("IsWater");
                                if (!isSoil && !isWater)
                                {
                                    ClearPreviousCell();
                                    return;
                                }
                            }
                            else
                            {
                                ClearPreviousCell();
                                return;
                            }
                        }
                        else
                        {
                            ClearPreviousCell();
                            return;
                        }
                    }
                    else
                    {
                        ClearPreviousCell();
                        return;
                    }
                }
            }

            if (cell != previousCell)
            {
                if (previousCell != new Vector2I(-1, -1))
                {
                    SetCell(previousCell, -1, Vector2I.Zero);
                }
                SetCell(cell, 1, Vector2I.Zero);
                previousCell = cell;
            }
        }

        private void ClearPreviousCell()
        {
            if (previousCell != new Vector2I(-1, -1))
            {
                SetCell(previousCell, -1, Vector2I.Zero);
                previousCell = new Vector2I(-1, -1);
            }
        }
    }
}
