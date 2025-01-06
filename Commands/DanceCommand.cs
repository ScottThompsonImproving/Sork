namespace Sork.Commands;
public class DanceCommand : BaseCommand
{
    private readonly UserInputOutput io;

    public DanceCommand(UserInputOutput io)
    {
        this.io = io;
    }

    public override bool Handles(string userInput)
    {
        return GetCommandFromInput(userInput) == "dance";
    }

    public override CommandResult Execute()
    {
        io.WriteNoun("You");
        io.WriteMessageLine(" spin around in circles!");
        return new CommandResult { RequestExit = false, IsHandled = true };
    }
}