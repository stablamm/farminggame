using Godot;

namespace FarmingGame.Autoloads
{
    public partial class SignalManager : Node
    {
        public const string NODE_PATH = "/root/SignalManager";

        [Signal]
        public delegate void SendMessage_EventHandler(string message);

        public override void _Ready()
        {
            AddUserSignal(nameof(SendMessage_EventHandler));
        }
    }
}
