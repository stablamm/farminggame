using System.Collections.Generic;
using Godot;

namespace FarmingGame.Scenes.Levels
{
    public partial class NavigationLayer : TileMapLayer
    {
        [Export]
        public GroundLayer GroundLayer;

        private AStar2D _astar;
        private Dictionary<Vector2I, bool> cellWalkable = new Dictionary<Vector2I, bool>();
            
        public override void _Ready()
        {
            _astar = new AStar2D();
            InitializeGrid();
            UpdateNavigation();
        }

        private void UpdateNavigation()
        {
            foreach (var tile in GroundLayer.GetUsedCells())
            {
                TileSet tileSet = GroundLayer.TileSet;
                TileSetSource source = tileSet.GetSource(GroundLayer.GetCellSourceId(tile));
                if (source is TileSetAtlasSource atlasSource)
                {
                    TileData tileData = atlasSource.GetTileData(GroundLayer.GetCellAtlasCoords(tile), GroundLayer.GetCellAlternativeTile(tile));
                    if (tileData != null)
                    {
                        var isWater = (bool)tileData.GetCustomData("IsWater");

                        if (isWater)
                        {
                            UpdateMap(tile, false);
                        }
                        else
                        {
                            UpdateMap(tile, true);
                        }
                    }
                }
            }
        }

        // Initialize the A* grid based on the TileMapLayer's walkable tiles.
        private void InitializeGrid()
        {
            Rect2I usedRect = GetUsedRect();
            int gridWidth = usedRect.Size.X;
            int gridHeight = usedRect.Size.Y;
            Vector2I gridOffset = usedRect.Position;

            // Add walkable tiles to the A* grid.
            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    Vector2I gridPos = new Vector2I(x, y) + gridOffset;
                    if (IsWalkable(gridPos))
                    {
                        int id = GetPointId(gridPos, gridWidth);
                        _astar.AddPoint(id, MapToLocal(gridPos));
                    }
                }
            }

            // Connect walkable tiles to their neighbors.
            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    Vector2I gridPos = new Vector2I(x, y) + gridOffset;
                    if (IsWalkable(gridPos))
                    {
                        int id = GetPointId(gridPos, gridWidth);
                        ConnectNeighbors(id, gridPos, gridWidth, gridHeight, gridOffset);
                    }
                }
            }
        }

        // Calculate a unique ID for a tile based on its grid position.
        private int GetPointId(Vector2I gridPos, int gridWidth)
        {
            return (gridPos.X - GetUsedRect().Position.X) +
                   (gridPos.Y - GetUsedRect().Position.Y) * gridWidth;
        }

        public bool IsWalkable(Vector2I gridPos)
        {
            if (cellWalkable.TryGetValue(gridPos, out bool walkable))
                return walkable;
            return false;
        }

        // Connect a tile to its walkable neighbors.
        private void ConnectNeighbors(int currentId, Vector2I gridPos, int gridWidth, int gridHeight, Vector2I gridOffset)
        {
            Vector2I[] directions = new Vector2I[]
            {
                new Vector2I(0, -1), // Up
                new Vector2I(0, 1),  // Down
                new Vector2I(-1, 0), // Left
                new Vector2I(1, 0)   // Right
            };

            foreach (Vector2I dir in directions)
            {
                Vector2I neighborPos = gridPos + dir;
                Vector2I relativePos = neighborPos - gridOffset;

                if (relativePos.X >= 0 && relativePos.X < gridWidth &&
                    relativePos.Y >= 0 && relativePos.Y < gridHeight &&
                    IsWalkable(neighborPos))
                {
                    int neighborId = GetPointId(neighborPos, gridWidth);
                    if (_astar.HasPoint(neighborId))
                    {
                        _astar.ConnectPoints(currentId, neighborId);
                    }
                }
            }
        }

        // Update a tile's walkability and adjust the A* grid.
        // This method now uses our per-cell dictionary to ensure only the specific cell is updated.
        public void UpdateMap(Vector2I gridPos, bool isWalkable)
        {
            Rect2I usedRect = GetUsedRect();
            int gridWidth = usedRect.Size.X;
            int id = GetPointId(gridPos, gridWidth);

            // Update our per-cell walkability state.
            cellWalkable[gridPos] = isWalkable;

            if (isWalkable)
            {
                // Add the tile to the A* grid if it's not already there.
                if (!_astar.HasPoint(id))
                {
                    _astar.AddPoint(id, MapToLocal(gridPos));
                    ConnectNeighbors(id, gridPos, gridWidth, usedRect.Size.Y, usedRect.Position);
                }
            }
            else
            {
                // Remove the tile from the A* grid if it’s present.
                if (_astar.HasPoint(id))
                {
                    Vector2I[] directions = new Vector2I[]
                    {
                        new Vector2I(0, -1),
                        new Vector2I(0, 1),
                        new Vector2I(-1, 0),
                        new Vector2I(1, 0)
                    };

                    foreach (Vector2I dir in directions)
                    {
                        Vector2I neighborPos = gridPos + dir;
                        int neighborId = GetPointId(neighborPos, gridWidth);
                        if (_astar.HasPoint(neighborId))
                        {
                            _astar.DisconnectPoints(id, neighborId);
                        }
                    }
                    _astar.RemovePoint(id);
                }
            }
        }

        // Find a path between two world positions.
        public Vector2[] FindPath(Vector2I startPos, Vector2I targetPos)
        {
            Vector2I startGrid = startPos;   //LocalToMap(startPos);
            Vector2I targetGrid = targetPos; //LocalToMap(targetPos);

            Rect2I usedRect = GetUsedRect();
            int gridWidth = usedRect.Size.X;

            int startId = GetPointId(startGrid, gridWidth);
            int targetId = GetPointId(targetGrid, gridWidth);

            if (!_astar.HasPoint(startId) || !_astar.HasPoint(targetId))
            {
                GD.Print("Start or target is not walkable!");
                return new Vector2[0];
            }

            return _astar.GetPointPath(startId, targetId);
        }
    }
}
