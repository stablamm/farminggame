using FarmingGame.Autoloads;
using FarmingGame.Scenes.Crops;
using Godot;
using System;
using System.Collections.Generic;

namespace FarmingGame.Scenes.Levels
{
    public partial class Test : Node2D
    {
        [Export]
        public OverlayLayer OverlayLayer;

        [Export]
        public NavigationLayer NavigationLayer;

        [Export]
        public GroundLayer GroundLayer;

        private Dictionary<Vector2I, Crop> plantedCrops = new();
        private PackedScene packedLettuce;

        public override void _Ready()
        {
            GroundLayer = GetNode<GroundLayer>("GroundLayer");
            packedLettuce = ResourceLoader.Load<PackedScene>("res://Scenes/Crops/Inherited/lettuce.tscn");

            AutoloadManager.Instance.SignalManager
                .Connect(
                    nameof(SignalManager.CropHarvested_EventHandler), 
                    new Callable(this, nameof(OnCropHarvested))
                );
        }

        public override void _Input(InputEvent @event)
        {
            if (@event is InputEventMouseButton mouseButton)
            {
                if (mouseButton.ButtonIndex == MouseButton.Left 
                    && mouseButton.IsPressed()
                    && !mouseButton.IsEcho())
                {
                    Crop c = packedLettuce.Instantiate() as Crop;
                    Vector2I mCell = GroundLayer.GetMouseCell();

                    if (plantedCrops.ContainsKey(mCell))
                    {
                        // Clear out existing crop
                        plantedCrops[mCell].QueueFree();
                        GD.Print("Cleared out existing crop");
                    }

                    c.GlobalPosition = new Vector2((mCell.X * 32) + 16, (mCell.Y * 32) + 16);
                    plantedCrops[mCell] = c;
                    GetTree().CurrentScene.AddChild(c);
                }
                else if (mouseButton.ButtonIndex == MouseButton.Right
                         && mouseButton.IsPressed()
                         && !mouseButton.IsEcho())
                {
                    Vector2I mCell = GroundLayer.GetMouseCell();

                    if (plantedCrops.ContainsKey(mCell))
                    {
                        plantedCrops[mCell].HarvestCrop();
                    }

                    var isSoil = GroundLayer.GetCustomTileData<bool>(mCell, "IsSoil");
                    var isWater = GroundLayer.GetCustomTileData<bool>(mCell, "IsWater");
                    GD.Print($"Cell: {mCell}, IsSoil: {isSoil}, IsWater: {isWater}");
                }
            }
        }

        public void OnCropHarvested(string cropId)
        {
            foreach (KeyValuePair<Vector2I, Crop> kvp in plantedCrops)
            {
                if (kvp.Value.CropID == cropId)
                {
                    plantedCrops.Remove(kvp.Key);
                    break;
                }
            }
            GD.Print($"Crop Harvested: {cropId}");
        }
    }
}
