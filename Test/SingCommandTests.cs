using Sork.Commands;
using Sork.World;

namespace Sork.Test;

[TestClass]
public sealed class SingCommandTests
{
    [TestMethod]
    public void Execute_ShouldOutputMessage()
    {
        // Arrange
        var io = new TestInputOutput();
        var command = new SingCommand(io);
        var gameState = GameState.Create();
        var tester = new Player { Name = "TesterTheGreat", Location = gameState.RootRoom, Io = io };

        // Act
        command.Execute("sing", tester);

        // Assert
        Assert.AreEqual("You", io.Outputs[0]);
        Assert.AreEqual(" wail like a banshee!", io.Outputs[1]);

        Assert.AreEqual("", io.SpeakOutputs[tester.Location][0]);
        Assert.AreEqual(tester.Name, io.SpeakOutputs[tester.Location][1]);
        Assert.AreEqual(" wails like a banshee!", io.SpeakOutputs[tester.Location][2]);
    }

    [TestMethod]
    public void Handles_ShouldReturnTrue_WhenCapitalizedInputIsProvided()
    {
        // Arrange
        var command = new SingCommand(new TestInputOutput());

        // Act
        var result = command.Handles("SING");

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Handles_ShouldReturnTrue_WhenLowercaseInputIsProvided()
    {
        // Arrange
        var command = new SingCommand(new TestInputOutput());

        // Act
        var result = command.Handles("sing");

        // Assert
        Assert.IsTrue(result);
    }
}