using FarmingGame.Autoloads.GameManagers;
using Godot;

namespace FarmingGame.Autoloads
{
    public partial class GameManager : Node
    {
        public const string NODE_PATH = "/root/GameManager";

        public InventoryManager Inventory { get; private set; } = new();
        public AreaManager Areas { get; private set; } = new();
        public MapManager Map { get; private set; } = new();
        public PlayerManager Player { get; private set; } = new();

        public override void _Ready()
        {
            Map.Instantiate();
        }
    }
}
