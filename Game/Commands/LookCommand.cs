using Sork.World;

namespace Sork.Commands;

public class LookCommand : BaseCommand
{
    private readonly IUserInputOutput io;
    public LookCommand(IUserInputOutput io)
    {
        this.io = io;
    }

    public override bool Handles(string userInput)
    {
        return GetCommandFromInput(userInput) == "look";
    }

    public override CommandResult Execute(string userInput, GameState gameState)
    {
        io.WriteNoun(gameState.Player.Location.Name);
        io.WriteMessageLine("");
        io.WriteMessageLine(gameState.Player.Location.Description);
        if (player.Location.Exits.Count > 0)
        {
            io.WriteMessageLine("");
            io.WriteNoun("Exits");
            io.WriteMessageLine("");
            foreach (var exit in player.Location.Exits)
            {
                io.WriteMessageLine($"{exit.Key} - {exit.Value.Description}");
            }
        }
        else
        {
            io.WriteMessageLine("");
            io.WriteMessageLine("You might be trapped because you cannot see a way out.");
        }

        if (player.Location.Inventory.Count > 0)
        {
            io.WriteMessageLine("");
            io.WriteNoun("Inventory");
            io.WriteMessageLine("");
            foreach (var item in player.Location.Inventory)
            {
                io.WriteMessageLine($"{item.Name} - {item.Description}");
            }
        }
        else
        {
            io.WriteMessageLine("");
            io.WriteMessageLine("There is nothing else of interest here.");
        }

        return new CommandResult { RequestExit = false, IsHandled = true };
    }
}