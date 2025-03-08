using Godot;

namespace FarmingGame.Autoloads
{
    public partial class AutoloadManager : Node
    {
        public const string NODE_PATH = "/root/AutoloadManager";

        public static AutoloadManager Instance { get; private set; }
        public SignalManager SignalManager { get; private set; }
        public FarmGenerator FarmGenerator { get; private set; }
        public GameManager GameManager { get; private set; }

        public override void _Ready()
        {
            Instance = this;
            SignalManager = GetNode<SignalManager>(SignalManager.NODE_PATH);
            FarmGenerator = GetNode<FarmGenerator>(FarmGenerator.NODE_PATH);
            GameManager = GetNode<GameManager>(GameManager.NODE_PATH);
        }
    }
}