using Godot;

namespace FarmingGame.Autoloads.GameManagers
{
    public class PlayerManager
    {
        public string ID { get; set; }
        public Vector2 Position { get; private set; } = Vector2.Zero;

        public void UpdatePosition(Vector2 position) => Position = position;
    }
}
