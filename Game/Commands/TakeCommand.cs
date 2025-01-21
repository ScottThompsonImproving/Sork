namespace Sork.Commands;
using Sork.World;

public sealed class TakeCommand : BaseCommand
{
    private readonly IUserInputOutput io;
    public TakeCommand(IUserInputOutput io)
    {
        this.io = io;
    }

    public override bool Handles(string userInput)
    {
        return GetCommandFromInput(userInput) == "take";
    }

    public override CommandResult Execute(string userInput, GameState gameState)
    {
        var parameters = GetParametersFromInput(userInput);
        if (parameters.Length == 0)
        {
            io.WriteMessageLine("What do you want to take?");
            return new CommandResult() { IsHandled = false, RequestExit = false };
        }

        return new CommandResult() { IsHandled = true, RequestExit = false };
    }
}