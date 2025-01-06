namespace Sork;
public class DanceCommand : ICommand
{
    public bool Handles(string userInput) => userInput == "dance";
    public CommandResult Execute()
    {
        Console.WriteLine("You spin around in circles!");
        return new CommandResult { RequestExit = false, IsHandled = true };
    }
}