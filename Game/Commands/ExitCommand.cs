using Sork.World;

namespace Sork.Commands;

public class ExitCommand : BaseCommand
{
    private readonly IUserInputOutput io;

    public ExitCommand(IUserInputOutput io)
    {
        this.io = io;
    }

    public override bool Handles(string userInput)
    {
        return GetCommandFromInput(userInput) == "exit";
    }

    public override CommandResult Execute(string userInput, Player player)
    {
        io.WriteMessageLine("Fare thee well, chummer!");
        return new CommandResult { RequestExit = true, IsHandled = true };
    }
}