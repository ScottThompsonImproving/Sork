using System.Net.Sockets;
using System.IO;

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
        // Read a single character
        int charRead = reader.Read();
        return charRead >= 0 ? ((char)charRead).ToString() : "";
    }
}
