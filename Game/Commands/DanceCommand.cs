using Sork.World;

namespace Sork.Commands;

public class DanceCommand : BaseCommand
{
    private readonly IUserInputOutput io;

    public DanceCommand(IUserInputOutput io)
    {
        this.io = io;
    }

    public override bool Handles(string userInput, Player player)
    {
        var paramsLength = GetParametersFromInput(userInput).Length;
        return GetCommandFromInput(userInput) == "dance" && (paramsLength == 0 || paramsLength == 1);
    }

    public override CommandResult Execute(string userInput, Player player)
    {
        var parameters = GetParametersFromInput(userInput);

        if (parameters.Length == 0)
        {
            io.WriteNoun("You");
            io.WriteMessageLine(" spin around in circles!");

            io.SpeakMessageLine("", player.Location);
            io.SpeakNoun(player.Name, player.Location);
            io.SpeakMessageLine(" spins around in circles!", player.Location);
        }
        else
        {
            io.WriteNoun("You");
            io.WriteMessage(" spin around in circles with ");
            io.WriteNoun(parameters[0]);
            io.WriteMessageLine("!");

            io.SpeakMessageLine("", player.Location);
            io.SpeakNoun(player.Name, player.Location);
            io.SpeakMessage(" spins around in circles with ", player.Location);
            io.SpeakNoun(parameters[0], player.Location);
            io.SpeakMessageLine("!", player.Location);
        }
        return new CommandResult { RequestExit = false, IsHandled = true };
    }
}