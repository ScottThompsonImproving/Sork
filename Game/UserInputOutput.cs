namespace Sork;


using System.Net;
using System.Net.Sockets;

public class ClientConnectedEventArgs : EventArgs
{
    public TcpClient Client { get; }
    public ClientConnectedEventArgs(TcpClient client)
    {
        Client = client;
    }
}

public class NetworkGame
{
    public event EventHandler<ClientConnectedEventArgs>? ClientConnected;
    private TcpListener? listener;
    private const int Port = 1701;

    public async Task StartListening()
    {
        listener = new TcpListener(IPAddress.Any, Port);
        listener.Start();

        while (true)
        {
            try
            {
                Console.WriteLine("Waiting for client connection...");
                TcpClient client = await listener.AcceptTcpClientAsync();
                OnClientConnected(new ClientConnectedEventArgs(client));
            }
            catch (Exception)
            {
                Console.WriteLine("Client connection failed.");
            }
        }
    }

    protected virtual void OnClientConnected(ClientConnectedEventArgs e)
    {
        ClientConnected?.Invoke(this, e);
    }

    public void Stop()
    {
        listener?.Stop();
    }
}


public class NetworkInputOutput : IUserInputOutput
{
    private readonly TcpClient _client;
    private readonly StreamReader _reader;
    private readonly StreamWriter _writer;

    public NetworkInputOutput(TcpClient client)
    {
        _client = client;
        var stream = client.GetStream();
        _reader = new StreamReader(stream);
        _writer = new StreamWriter(stream);
        _writer.AutoFlush = true;
    }

    public void WritePrompt(string prompt)
    {
        _writer.Write($"\u001b[32m{prompt}\u001b[0m"); // Green text ANSI escape code
    }

    public void WriteMessage(string message)
    {
        _writer.Write(message);
    }

    public void WriteNoun(string noun)
    {
        _writer.Write($"\u001b[34m{noun}\u001b[0m"); // Blue text ANSI escape code
    }

    public void WriteMessageLine(string message)
    {
        _writer.WriteLine(message);
    }

    public string ReadInput()
    {
        return _reader.ReadLine()?.NetworkCleanup()?.Trim() ?? "";
    }

    public string ReadKey()
    {
        // Read a single character
        int charRead = _reader.Read();
        return charRead >= 0 ? ((char)charRead).ToString() : "";
    }
}

public interface IUserInputOutput
{
    void WritePrompt(string prompt);
    void WriteMessage(string message);
    void WriteNoun(string noun);
    void WriteMessageLine(string message);
    string ReadInput();
    string ReadKey();
}

public class UserInputOutput : IUserInputOutput
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
        return (Console.ReadLine() ?? "").Trim();
    }

    public string ReadKey()
    {
        return Console.ReadKey().KeyChar.ToString();
    }
}

