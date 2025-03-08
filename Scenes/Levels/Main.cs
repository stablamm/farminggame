using FarmingGame.Autoloads;
using FarmingGame.Scripts;
using FarmingGame.Scripts.Items;
using Godot;

namespace FarmingGame.Scenes.Levels
{
    public partial class Main : Node2D
    {
        private RichTextLabel outputText;
        private LineEdit inputField;
        private Farmer farmer;

        public override void _Ready()
        {
            outputText = GetNode<RichTextLabel>("%OutputText");
            inputField = GetNode<LineEdit>("%InputField");

            farmer = new();
            farmer.Instantiate();

            foreach (var area in AutoloadManager.Instance.GameManager.Areas.AllAreas)
            {
                if (area.Value.Id == FARM_AREA.FIELD)
                {
                    area.Value.AddItem(new WheatSeed(), 1);
                }
            }
            
            inputField.Connect("text_submitted", new Callable(this, nameof(OnInputSubmitted)));
            AutoloadManager.Instance.SignalManager.Connect(nameof(SignalManager.SendMessage_EventHandler), new Callable(this, nameof(OnMessageSent)));
        }

        private void OnInputSubmitted(string text)
        {
            string result = CommandParser.Parse(text, farmer);
            outputText.Text += $"\n> {text}\n{result}";
            inputField.Clear();
        }

        private void OnMessageSent(string text) => outputText.Text += $"\n{text}\n";
    }
}