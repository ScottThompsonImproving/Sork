namespace Sork;
public class LaughCommand : ICommand
{
    public bool Handles(string userInput) => userInput == "lol";
    public CommandResult Execute()
    {
        Console.WriteLine("You laugh out loud!");
        return new CommandResult { RequestExit = false, IsHandled = true };
    }
}