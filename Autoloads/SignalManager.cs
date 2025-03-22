using Godot;

namespace FarmingGame.Autoloads
{
    public partial class SignalManager : Node
    {
        public const string NODE_PATH = "/root/SignalManager";

        [Signal]
        public delegate void SendMessage_EventHandler(string message);

        [Signal]
        public delegate void CropHarvested_EventHandler(string cropId);

        public override void _Ready()
        {
            AddUserSignal(nameof(SendMessage_EventHandler));
            AddUserSignal(nameof(CropHarvested_EventHandler));
        }

        public void EmitCropHarvested(string cropId)
            => EmitSignal(nameof(CropHarvested_EventHandler), cropId);
    }
}
