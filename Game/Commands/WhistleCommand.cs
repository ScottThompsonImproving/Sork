using Sork.World;

namespace Sork.Commands;

public class WhistleCommand : BaseCommand
{
    private readonly IUserInputOutput io;

    public WhistleCommand(IUserInputOutput io)
    {
        this.io = io;
    }
    public override bool Handles(string userInput, Player player)
    {
        return GetCommandFromInput(userInput) == "whistle";
    }
    public override CommandResult Execute(string userInput, Player player)
    {
        io.WriteNoun("You");
        io.WriteMessageLine(" sound like a boiling tea kettle!");

        io.SpeakMessageLine("", player.Location);
        io.SpeakNoun(player.Name, player.Location);
        io.SpeakMessageLine(" sounds like a boiling tea kettle!", player.Location);
        return new CommandResult { RequestExit = false, IsHandled = true };
    }
}