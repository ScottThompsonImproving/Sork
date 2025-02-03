using Sork.Commands;
using Sork.World;

namespace Sork.Test;

[TestClass]
public sealed class WhistleCommandTests
{
    [TestMethod]
    public void Execute_ShouldOutputMessage()
    {
        // Arrange
        var io = new TestInputOutput();
        var command = new WhistleCommand(io);
        var gameState = GameState.Create();
        var player = new Player { Name = "TesterTheGreat", Location = gameState.RootRoom, Io = io };

        // Act
        command.Execute("whistle", player);

        // Assert
        Assert.AreEqual("You", io.Outputs[0]);
        Assert.AreEqual(" sound like a boiling tea kettle!", io.Outputs[1]);

        Assert.AreEqual("", io.SpeakOutputs[player.Location][0]);
        Assert.AreEqual(player.Name, io.SpeakOutputs[player.Location][1]);
        Assert.AreEqual(" sounds like a boiling tea kettle!", io.SpeakOutputs[player.Location][2]);
    }

    [TestMethod]
    public void Handles_ShouldReturnTrue_WhenCapitalizedInputIsProvided()
    {
        // Arrange
        var command = new WhistleCommand(new TestInputOutput());

        // Act
        var result = command.Handles("WHISTLE");

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Handles_ShouldReturnTrue_WhenLowercaseInputIsProvided()
    {
        // Arrange
        var command = new WhistleCommand(new TestInputOutput());

        // Act
        var result = command.Handles("whistle");

        // Assert
        Assert.IsTrue(result);
    }
}