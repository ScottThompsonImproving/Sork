public class WhistleCommand : ICommand
{
    public bool Handles(string userInput) => userInput == "whistle";
    public CommandResult Execute()
    {
        Console.WriteLine("You sound like a boiling tea kettle!");
        return new CommandResult { RequestExit = false, IsHandled = true };
    }
}