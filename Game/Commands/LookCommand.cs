using Sork.World;

namespace Sork.Commands;

public class LookCommand : BaseCommand
{
    private readonly IUserInputOutput io;
    public LookCommand(IUserInputOutput io)
    {
        this.io = io;
    }

    public override bool Handles(string userInput, Player player)
    {
        return GetCommandFromInput(userInput) == "look";
    }

    public override CommandResult Execute(string userInput, Player player)
    {
        io.WriteNoun(player.Location.Name);
        io.WriteMessageLine("");
        io.WriteMessageLine(player.Location.Description);

        if (player.Location.Exits.Count > 0)
        {
            io.WriteMessageLine("");
            io.WriteNoun("Exits");
            io.WriteMessageLine("");
            foreach (var exit in player.Location.Exits)
            {
                io.WriteMessageLine($"{exit.Name} - {exit.Description}");
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
            io.WriteMessageLine("There is nothing of interest here.");
        }

        if (player.Location.Players.Count > 0)
        {
            io.WriteMessageLine("");
            io.WriteNoun("Players");
            io.WriteMessageLine("");
            foreach (var p in player.Location.Players)
            {
                io.WriteMessageLine($"{p.Name} is here.");
            }
        }
        else
        {
            io.WriteMessageLine("");
            io.WriteMessageLine("You are alone, and suddenly feel very uncomfortable.");
        }

        return new CommandResult { RequestExit = false, IsHandled = true };
    }
}