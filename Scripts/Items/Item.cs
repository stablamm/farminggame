namespace FarmingGame.Scripts.Items
{
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsStackable { get; set; }
        public int MaxStack { get; set; } = 99;

        public override string ToString()
        {
            return $"{Name}: {Description} (Stackable: {IsStackable}, MaxStack: {MaxStack})";
        }
    }
}
