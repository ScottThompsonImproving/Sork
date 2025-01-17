using Microsoft.Extensions.DependencyInjection;
using Sork.Commands;
using Sork.World;

namespace Sork;

public class Program
{
    public static void Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddSingleton<UserInputOutput>();
        services.AddSingleton<GameState>(sp => GameState.Create(sp.GetRequiredService<UserInputOutput>()));
        var commandTypes = typeof(ICommand).Assembly.GetTypes()
            .Where(t => typeof(ICommand).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        foreach (var commandType in commandTypes)
        {
            services.AddSingleton(typeof(ICommand), commandType);
        }
        var provider = services.BuildServiceProvider();

        var gameState = provider.GetRequiredService<GameState>();
        var commands = provider.GetServices<ICommand>().ToList();
        var io = provider.GetRequiredService<UserInputOutput>();

        do
        {
            io.WritePrompt(" > ");
            string input = io.ReadInput();

            var result = new CommandResult { RequestExit = false, IsHandled = false };
            var handled = false;

            foreach (var command in commands)
            {
                if (command.Handles(input))
                {
                    handled = true;
                    result = command.Execute(input, gameState);
                    if (result.RequestExit) { break; }
                }
            }

            if (!handled) { io.WriteMessageLine("Unkown command"); }
            if (result.RequestExit) { break; }
        } while (true);
    }
}

public class UserInputOutput
{
    public void WritePrompt(string prompt)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(prompt);
        Console.ResetColor();
    }

    public void WriteMessage(string message)
    {
        Console.Write(message);
    }

    public void WriteNoun(string noun)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write(noun);
        Console.ResetColor();
    }

    public void WriteMessageLine(string message)
    {
        Console.WriteLine(message);
    }

    public string ReadInput()
    {
        return Console.ReadLine().Trim();
    }

    public string ReadKey()
    {
        return Console.ReadKey().KeyChar.ToString();
    }
}
