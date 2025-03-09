namespace FarmingGame.Scripts.Items
{
    public class WateringCan : Item
    {
        public override string Name => "Watering Can";
        public override string Description => "A rusted, old, watering can.";
        public override bool IsStackable => false;
        public override int MaxStack => 1;

        public bool IsFull { get; private set; } = false;

        public void Fill()
        {
            IsFull = true;
        }

        public override bool Use()
        {
            if (IsFull)
            {
                IsFull = false;
                return true;
            }

            return false;
        }

        public override string ToString() => $"{Name} {(IsFull ? "(Full)" : "(Empty)")}";
    }
}
