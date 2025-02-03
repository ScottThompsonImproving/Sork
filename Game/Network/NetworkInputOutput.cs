using System.Net.Sockets;
using System.IO;
using Sork.World;

namespace Sork.Network;

public class NetworkInputOutput : IUserInputOutput
{
    private readonly TcpClient client;
    private readonly StreamReader reader;
    private readonly StreamWriter writer;

    public NetworkInputOutput(TcpClient client)
    {
        this.client = client;
        var stream = client.GetStream();
        reader = new StreamReader(stream);
        writer = new StreamWriter(stream);
        writer.AutoFlush = true;
    }

    public void WritePrompt(string prompt)
    {
        // writer.Write($"\u001b[32m{prompt}\u001b[0m"); // Green text ANSI escape code
        writer.Write(prompt);
    }

    public void WriteMessage(string message)
    {
        writer.Write(message);
    }

    public void WriteNoun(string noun)
    {
        // writer.Write($"\u001b[34m{noun}\u001b[0m"); // Blue text ANSI escape code
        writer.Write(noun);
    }

    public void WriteMessageLine(string message)
    {
        writer.WriteLine(message);
    }

    public string ReadInput()
    {
        return reader.ReadLine()?.NetworkCleanup()?.Trim() ?? "";
    }

    public string ReadKey()
    {
        int charRead = reader.Read();
        return charRead >= 0 ? ((char)charRead).ToString() : "";
    }

    public void SpeakMessage(string message, Room room)
    {
        foreach (var player in room.Players)
        {
            if (player.Io == this)
                continue;
            player.Io.WriteMessage($"{player.Name} says: {message}");
        }
    }

    public void SpeakNoun(string noun, Room room)
    {
        foreach (var player in room.Players)
        {
            if (player.Io == this)
                continue;
            player.Io.WriteNoun(noun);
        }
    }

    public void SpeakMessageLine(string message, Room room)
    {
        foreach (var player in room.Players)
        {
            if (player.Io == this)
                continue;
            player.Io.WriteMessageLine($"{player.Name} says: {message}");
        }
    }
}
