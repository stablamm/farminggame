using Godot;

namespace FarmingGame.Scripts.Items
{
    public abstract class Item
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract bool IsStackable { get; }
        public abstract int MaxStack { get; }

        public virtual bool Use()
        {
            GD.Print($"{Name} used.");
            return true;
        }

        public override string ToString()
        {
            return Name;
            //return $"{Name}: {Description} (Stackable: {IsStackable}, MaxStack: {MaxStack})";
        }
    }
}
