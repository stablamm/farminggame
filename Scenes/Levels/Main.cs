using FarmingGame.Autoloads;
using FarmingGame.Scripts;
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
            // Get UI nodes
            outputText = GetNode<RichTextLabel>("%OutputText");
            inputField = GetNode<LineEdit>("%InputField");

            // Initialize game
            farmer = new Farmer 
            { 
                Name = "Farmer Joe", 
                CurrentArea = AutoloadManager.Instance.FarmGenerator.GetArea(0), 
                Inventory = new() { "wheat seeds" } 
            };
            
            UpdateOutput();

            // Connect input signal
            inputField.Connect("text_submitted", new Callable(this, nameof(OnInputSubmitted)));
        }

        private void OnInputSubmitted(string text)
        {
            string result = CommandParser.Parse(text, farmer, AutoloadManager.Instance.FarmGenerator.FarmAreas);
            outputText.Text += $"\n> {text}\n{result}";
            inputField.Clear();
        }

        private void UpdateOutput()
        {
            outputText.Text = $"You are in: {farmer.CurrentArea.Name}\n{CommandParser.Parse("look", farmer, AutoloadManager.Instance.FarmGenerator.FarmAreas)}";
        }
    }
}