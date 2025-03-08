using Godot;

namespace FarmingGame.Scripts.Items
{
    public class WheatSeed : Item
    {
        public override string Name => "Wheat Seed";
        public override string Description => "A seed that grows into wheat.";
        public override bool IsStackable => true;
        public override int MaxStack => 50;

        public override void Use()
        {
            // Custom logic for planting wheat.
            GD.Print("You plant the wheat seed.");
        }
    }
}
