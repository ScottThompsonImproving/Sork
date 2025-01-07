namespace Sork.Commands;
public class SingCommand : BaseCommand
{
    private readonly UserInputOutput io;

    public SingCommand(UserInputOutput io)
    {
        this.io = io;
    }

    public override bool Handles(string userInput)
    {
        return GetCommandFromInput(userInput) == "sing";
    }

    public override CommandResult Execute(string userInput, GameState gameState)
    {
        io.WriteNoun("You");
        io.WriteMessageLine(" wail like a banshee!");
        return new CommandResult { RequestExit = false, IsHandled = true };
    }
}