using Sork.World;

namespace Sork.Commands;

public class SingCommand : BaseCommand
{
    private readonly IUserInputOutput io;

    public SingCommand(IUserInputOutput io)
    {
        this.io = io;
    }

    public override bool Handles(string userInput)
    {
        return GetCommandFromInput(userInput) == "sing";
    }

    public override CommandResult Execute(string userInput, Player player)
    {
        io.WriteNoun("You");
        io.WriteMessageLine(" wail like a banshee!");
        io.SpeakNoun(player.Name, player.Location);
        io.SpeakMessageLine(" wails like a banshee!", player.Location);
        return new CommandResult { RequestExit = false, IsHandled = true };
    }
}