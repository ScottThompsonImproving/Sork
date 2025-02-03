using Sork.Commands;
using Sork.World;

namespace Sork.Test;

[TestClass]
public sealed class LaughCommandTests
{
    [TestMethod]
    public void Execute_ShouldOutputMessage()
    {
        // Arrange
        var io = new TestInputOutput();
        var command = new LaughCommand(io);
        var gameState = GameState.Create();
        var tester = new Player { Name = "TesterTheGreat", Location = gameState.RootRoom, Io = io };

        // Act
        command.Execute("LOL", tester);

        // Assert
        Assert.AreEqual("You", io.Outputs[0]);
        Assert.AreEqual(" laugh out loud!", io.Outputs[1]);

        Assert.AreEqual("", io.SpeakOutputs[tester.Location][0]);
        Assert.AreEqual(tester.Name, io.SpeakOutputs[tester.Location][1]);
        Assert.AreEqual(" laughs out loud!", io.SpeakOutputs[tester.Location][2]);
    }

    [TestMethod]
    public void Handles_ShouldReturnTrue_WhenCapitalizedInputIsProvided()
    {
        // Arrange
        var command = new LaughCommand(new TestInputOutput());

        // Act
        var result = command.Handles("LOL");

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Handles_ShouldReturnTrue_WhenLowercaseInputIsProvided()
    {
        // Arrange
        var command = new LaughCommand(new TestInputOutput());

        // Act
        var result = command.Handles("lol");

        // Assert
        Assert.IsTrue(result);
    }
}