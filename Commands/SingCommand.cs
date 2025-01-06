namespace Sork.Commands;
public class SingCommand : ICommand
{
    private readonly UserInputOutput io;
    public SingCommand(UserInputOutput io)
    {
        this.io = io;
    }
    public bool Handles(string userInput) => userInput == "sing";
    public CommandResult Execute()
    {
        io.WriteMessageLine("You wail like a banshee!");
        return new CommandResult { RequestExit = false, IsHandled = true };
    }
}