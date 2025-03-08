using Godot;

namespace FarmingGame.Scripts.Items
{
    public abstract class Item
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract bool IsStackable { get; }
        public abstract int MaxStack { get; }

        public virtual void Use()
        {
            GD.Print($"{Name} used.");
        }

        public override string ToString()
        {
            return $"{Name}: {Description} (Stackable: {IsStackable}, MaxStack: {MaxStack})";
        }
    }
}
