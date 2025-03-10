using Sork.World;

namespace Sork.Commands;

public abstract class BaseCommand : ICommand
{
    public abstract bool Handles(string userInput, Player player);
    public abstract CommandResult Execute(string userInput, Player player);

    public string GetCommandFromInput(string userInput)
    {
        return userInput.Split(' ')[0].ToLower();
    }

    public string[] GetParametersFromInput(string userInput)
    {
        return userInput.Split(' ').Skip(1).ToArray();
    }
}