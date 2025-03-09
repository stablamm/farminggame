using FarmingGame.Autoloads;

namespace FarmingGame.Scripts.Commands
{
    public class GoCommand : BaseCommand, ICommand
    {
        public string Execute(string[] args)
        {
            if (args.Length == 0)
            {
                return "Go where?";
            }

            if (args.Length == 1)
            {
                var playerPos = AutoloadManager.Instance.GameManager.Player.Position;
                var a = args[0];

                if (a == "north")
                {
                    if (AutoloadManager.Instance.GameManager.Map.IsValidPosition((int)playerPos.X - 1, (int)playerPos.Y))
                    {
                        AutoloadManager.Instance.GameManager.Player.UpdatePosition(new Godot.Vector2(playerPos.X - 1, playerPos.Y));

                        return "Moved north";
                    }
                }
                else if (a == "east")
                {
                    if (AutoloadManager.Instance.GameManager.Map.IsValidPosition((int)playerPos.X, (int)playerPos.Y + 1))
                    {
                        AutoloadManager.Instance.GameManager.Player.UpdatePosition(new Godot.Vector2(playerPos.X, playerPos.Y + 1));

                        return "Moved east";
                    }
                }
                else if (a == "south")
                {
                    if (AutoloadManager.Instance.GameManager.Map.IsValidPosition((int)playerPos.X + 1, (int)playerPos.Y))
                    {
                        AutoloadManager.Instance.GameManager.Player.UpdatePosition(new Godot.Vector2(playerPos.X + 1, playerPos.Y));

                        return "Moved south";
                    }
                }
                else if (a == "west")
                {
                    if (AutoloadManager.Instance.GameManager.Map.IsValidPosition((int)playerPos.X, (int)playerPos.Y - 1))
                    {
                        AutoloadManager.Instance.GameManager.Player.UpdatePosition(new Godot.Vector2(playerPos.X, playerPos.Y - 1));
                        
                        return "Moved west";
                    }
                }

            }

            return "No path that way!";
        }
    }
}