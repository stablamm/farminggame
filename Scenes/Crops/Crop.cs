using FarmingGame.Autoloads;
using Godot;
using static FarmingGame.Scenes.Crops.ICrop;

namespace FarmingGame.Scenes.Crops
{
    public partial class Crop : Node2D, ICrop
    {
        public string CropID { get; set; }
        public string CropName { get; set; }
        public CROP_STATE CropState { get; set; }

        public int SeedlingFrame = 1;
        public int GrowingStages;
        public int GrowthStage = 0;
        public int[] GrowingFrame;
        public int GrownFrame;
        public int DecayedFrame;

        public Sprite2D Sprite;
        public Timer GrowTimer;

        public override void _Ready()
        {
            Sprite = GetNode<Sprite2D>("Sprite");
            GrowTimer = GetNode<Timer>("GrowTimer");

            if (CropState == CROP_STATE.SEEDLING)
            {
                Sprite.Frame = SeedlingFrame;
            }
        }

        public virtual void OnGrowTimerTimeout()
        {
            if (CropState == CROP_STATE.SEEDLING)
            {
                CropState = CROP_STATE.GROWING;
                Sprite.Frame = GrowingFrame[0];
                GrowthStage++;
            }
            else if (CropState == CROP_STATE.GROWING)
            {
                if (GrowthStage < GrowingStages)
                {
                    Sprite.Frame = GrowingFrame[GrowthStage];
                    GrowthStage++;
                }
                else
                {
                    CropState = CROP_STATE.GROWN;
                    Sprite.Frame = GrownFrame;
                }
            }
            else if (CropState == CROP_STATE.GROWN)
            {
                CropState = CROP_STATE.DEAD;
                Sprite.Frame = DecayedFrame;
            }
            else if (CropState == CROP_STATE.DEAD)
            {
                GrowTimer.Stop();
            }
        }

        public virtual void HarvestCrop()
        {
            AutoloadManager.Instance.SignalManager.EmitCropHarvested(CropID);
            QueueFree();
        }
    }
}