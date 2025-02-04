using Sork.Commands;
using Sork.World;

namespace Sork.Test;

[TestClass]
public sealed class ExitCommandTests
{
    [TestMethod]
    public void Execute_ShouldOutputMessage()
    {
        // Arrange
        var io = new TestInputOutput();
        var command = new ExitCommand(io);
        var gameState = GameState.Create();
        var player = new Player { Name = "TesterTheGreat", Location = gameState.RootRoom, Io = io };

        // Act
        var result = command.Execute("exit", player);

        // Assert
        Assert.AreEqual("Fare thee well, chummer!", io.Outputs[0]);
        Assert.IsTrue(result.RequestExit);
        Assert.IsTrue(result.IsHandled);
    }

    [TestMethod]
    public void Handles_ShouldReturnTrue_WhenCapitalizedInputIsProvided()
    {
        // Arrange
        var io = new TestInputOutput();
        var command = new ExitCommand(io);
        var gameState = GameState.Create();
        var tester = new Player { Name = "TesterTheGreat", Location = gameState.RootRoom, Io = io };

        // Act
        var result = command.Handles("EXIT", tester);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Handles_ShouldReturnTrue_WhenLowercaseInputIsProvided()
    {
        // Arrange
        var io = new TestInputOutput();
        var command = new ExitCommand(io);
        var gameState = GameState.Create();
        var tester = new Player { Name = "TesterTheGreat", Location = gameState.RootRoom, Io = io };

        // Act
        var result = command.Handles("exit", tester);

        // Assert
        Assert.IsTrue(result);
    }
}