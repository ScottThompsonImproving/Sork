using Sork.World;
using Sork.Commands;

namespace Sork.Test;

[TestClass]
public class DanceCommandTests
{
    [TestMethod]
    public void Execute_ShouldOutputMessage()
    {
        // Arrange
        var io = new TestInputOutput();
        var command = new DanceCommand(io);
        var gameState = GameState.Create();
        var tester = new Player { Name = "TesterTheGreat", Location = gameState.RootRoom, Io = io };

        // Act
        command.Execute("dance", tester);

        // Assert
        Assert.AreEqual("You", io.Outputs[0]);
        Assert.AreEqual(" spin around in circles!", io.Outputs[1]);

        Assert.AreEqual("", io.SpeakOutputs[tester.Location][0]);
        Assert.AreEqual(tester.Name, io.SpeakOutputs[tester.Location][1]);
        Assert.AreEqual(" spins around in circles!", io.SpeakOutputs[tester.Location][2]);
    }

    [TestMethod]
    public void Execute_ShouldOutputMessageWithParameter()
    {
        // Arrange
        var io = new TestInputOutput();
        var command = new DanceCommand(io);
        var gameState = GameState.Create();
        var tester = new Player { Name = "TesterTheGreat", Location = gameState.RootRoom, Io = io };
        var partner = "nobody";

        // Act
        command.Execute("dance nobody", tester);

        // Assert
        Assert.AreEqual("You", io.Outputs[0]);
        Assert.AreEqual(" spin around in circles with ", io.Outputs[1]);
        Assert.AreEqual(partner, io.Outputs[2]);
        Assert.AreEqual("!", io.Outputs[3]);

        Assert.AreEqual("", io.SpeakOutputs[tester.Location][0]);
        Assert.AreEqual(tester.Name, io.SpeakOutputs[tester.Location][1]);
        Assert.AreEqual($" spins around in circles with ", io.SpeakOutputs[tester.Location][2]);
        Assert.AreEqual(partner, io.SpeakOutputs[tester.Location][3]);
        Assert.AreEqual("!", io.SpeakOutputs[tester.Location][4]);
    }

    [TestMethod]
    public void Handles_ShouldReturnTrue_WhenCapitalizedInputIsProvided()
    {
        // Arrange
        var command = new DanceCommand(new TestInputOutput());

        // Act
        var result = command.Handles("DANCE");

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Handles_ShouldReturnTrue_WhenLowercaseInputIsProvided()
    {
        // Arrange
        var command = new DanceCommand(new TestInputOutput());

        // Act
        var result = command.Handles("dance");

        // Assert
        Assert.IsTrue(result);
    }
}
