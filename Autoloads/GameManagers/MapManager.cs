namespace FarmingGame.Autoloads.GameManagers
{
    public class MapManager
    {
        // A 2D array representing the map grid.
        public string[,] Map { get; private set; }

        public int MapHeight { get; set; } = 1;
        public int MapWidth { get; set; } = 3;

        /// <summary>
        /// Initializes the map with the given dimensions.
        /// </summary>
        public void Instantiate()
        {
            Map = new string[MapHeight, MapWidth];
            
            // Optionally initialize cells with a default value.
            for (int row = 0; row < MapHeight; row++)
            {
                for (int col = 0; col < MapWidth; col++)
                {
                    Map[row, col] = string.Empty;
                }
            }

            // Hardcode the temp map
            // Very simple world for now
            // [Field] <-> [Stone Well] <-> [Old Barn]
            foreach (var area in AutoloadManager.Instance.GameManager.Areas.AllAreas)
            {
                if (area.Value.Id == FARM_AREA.FIELD)
                {
                    UpdateMap(0, 0, area.Value.AreaId);
                }
                else if (area.Value.Id == FARM_AREA.STONE_WELL)
                {
                    UpdateMap(0, 1, area.Value.AreaId);
                }
                else if (area.Value.Id == FARM_AREA.OLD_BARN)
                {
                    UpdateMap(0, 2, area.Value.AreaId);
                }
            }
        }

        /// <summary>
        /// Updates a specific cell in the map grid with the provided id.
        /// </summary>
        /// <param name="row">Row index (0-based).</param>
        /// <param name="col">Column index (0-based).</param>
        /// <param name="id">The identifier to store in the cell.</param>
        public void UpdateMap(int row, int col, string id)
        {
            if (row < 0 || row >= MapHeight || col < 0 || col >= MapWidth)
                throw new System.IndexOutOfRangeException("Map coordinates out of range.");
            Map[row, col] = id;
        }

        /// <summary>
        /// Retrieves the value of a specific cell in the map grid.
        /// </summary>
        public string GetMapCell(int row, int col)
        {
            if (row < 0 || row >= MapHeight || col < 0 || col >= MapWidth)
                throw new System.IndexOutOfRangeException("Map coordinates out of range.");
            return Map[row, col];
        }

        /// <summary>
        /// Returns true if the specified position is within the map boundaries.
        /// </summary>
        public bool IsValidPosition(int row, int col)
        {
            return row >= 0 && row < MapHeight && col >= 0 && col < MapWidth;
        }

        public (bool north, bool east, bool south, bool west) GetExits(int row, int col)
        {
            // Check north (row - 1, col)
            bool north = IsValidPosition(row - 1, col) &&
                         !string.IsNullOrEmpty(GetMapCell(row - 1, col));

            // Check east (row, col + 1)
            bool east = IsValidPosition(row, col + 1) &&
                        !string.IsNullOrEmpty(GetMapCell(row, col + 1));

            // Check south (row + 1, col)
            bool south = IsValidPosition(row + 1, col) &&
                         !string.IsNullOrEmpty(GetMapCell(row + 1, col));

            // Check west (row, col - 1)
            bool west = IsValidPosition(row, col - 1) &&
                        !string.IsNullOrEmpty(GetMapCell(row, col - 1));

            return (north, east, south, west);
        }

        public override string ToString()
        {
            string output = string.Empty;
            
            for(int row = 0; row < MapHeight; row++)
            {
                for(int col = 0; col < MapWidth; col++)
                {
                    var cellId = GetMapCell(row, col);
                    var cell = AutoloadManager.Instance.GameManager.Areas.AllAreas[cellId];
                    output += $"{cell.Id.ToString()} ";
                }
                output += "\n";
            }

            return output;
        }
    }
}
