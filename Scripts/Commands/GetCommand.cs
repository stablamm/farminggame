using System.Collections.Generic;

namespace FarmingGame.Scripts.Commands
{
    public class GetCommand : ICommand
    {
        public string Execute(Farmer farmer, string[] args, Dictionary<int, FarmArea> farmAreas)
        {
            if (args[1] == "water")
            {
                if (!farmer.CurrentArea.HasWater)
                {
                    return "There’s no water here!";
                }
                if (farmer.HasWater)
                {
                    return "You’re already carrying water!";
                }

                farmer.HasWater = true;
                return "You fetch water from the well.";
            }

            return "Get what? Try 'get water'.";
        }
    }
}
