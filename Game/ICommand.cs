using Sork.World;

namespace Sork;

public interface ICommand
{
    bool Handles(string userInput, Player player);
    CommandResult Execute(string userInput, Player player);
}
