using Sork.World;

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
        var paramsLength = GetParametersFromInput(userInput).Length;
        return GetCommandFromInput(userInput) == "dance" && (paramsLength == 0 || paramsLength == 1);
    }

    public override CommandResult Execute(string userInput, GameState gameState)
    {
        var parameters = GetParametersFromInput(userInput);

        if (parameters.Length == 0)
        {
            io.WriteNoun("You");
            io.WriteMessageLine(" spin around in circles!");
        }
        else
        {
            io.WriteNoun("You");
            io.WriteMessage(" spin around in circles with ");
            io.WriteNoun(parameters[0]);
            io.WriteMessageLine("!");
        }
        return new CommandResult { RequestExit = false, IsHandled = true };
    }
}