namespace FarmingGame.Scenes.Crops
{
    public interface ICrop
    {
        public enum CROP_STATE
        {
            SEEDLING,
            GROWING,
            GROWN,
            DEAD
        }

        public string CropID { get; set; }
        public string CropName { get; set; }
        public CROP_STATE CropState { get; set; }
    }
}
