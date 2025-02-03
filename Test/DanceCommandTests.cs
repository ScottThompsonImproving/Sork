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
        Assert.AreEqual(" spin around in circles!", io.Outputs.Last());
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

    [TestMethod]
    public void Execute_ShouldOutputMessage_WhenPlayerDancesWithThemself()
    {
        // Arrange
        var io = new TestInputOutput();
        var command = new DanceCommand(io);
        var gameState = GameState.Create();
        var tester = new Player { Name = "TesterTheGreat", Location = gameState.RootRoom, Io = io };
        var qa = new Player { Name = "Q'aTheLesser", Location = gameState.RootRoom, Io = io };

        // Act
        command.Execute($"dance {tester.Name}", tester);

        // Assert
        Assert.AreEqual("You", io.Outputs[0]);
        Assert.AreEqual(" spin around in circles with your invisible clone!", io.Outputs[1]);

        Assert.AreEqual("", io.SpeakOutputs[qa.Location][0]);
        Assert.AreEqual(tester.Name, io.SpeakOutputs[qa.Location][1]);
        Assert.AreEqual(" spins around in circles with their invisible clone!", io.SpeakOutputs[qa.Location][2]);
        Assert.AreEqual(" > ", io.SpeakOutputs[qa.Location][3]);
    }

    [TestMethod]
    public void Execute_ShouldOutputMessage_WhenPlayerDancesWithAnotherPlayer()
    {
        // Arrange
        var io = new TestInputOutput();
        var ioQa = new TestInputOutput();
        var command = new DanceCommand(io);
        var gameState = GameState.Create();
        var tester = new Player { Name = "TesterTheGreat", Location = gameState.RootRoom, Io = io };
        var qa = new Player { Name = "Q'aTheLesser", Location = gameState.RootRoom, Io = ioQa };

        // Act
        command.Execute($"dance {qa.Name}", tester);

        // Assert
        Assert.AreEqual("You", io.Outputs[0]);
        Assert.AreEqual($" spin around in circles with ", io.Outputs[1]);
        Assert.AreEqual(qa.Name, io.Outputs[2]);
        Assert.AreEqual("!", io.Outputs[3]);

        Assert.AreEqual("", io.SpeakOutputs[tester.Location][0]);
        Assert.AreEqual(tester.Name, io.SpeakOutputs[tester.Location][1]);
        Assert.AreEqual(" spins around in circles with you!", io.SpeakOutputs[tester.Location][2]);
        Assert.AreEqual(" > ", io.SpeakOutputs[tester.Location][3]);
    }

    [TestMethod]
    public void Execute_ShouldOutputMessage_WhenPlayerDancesWithNobody()
    {
        // Arrange
        var io = new TestInputOutput();
        var command = new DanceCommand(io);
        var gameState = GameState.Create();
        var tester = new Player { Name = "TesterTheGreat", Location = gameState.RootRoom, Io = io };
        var qa = new Player { Name = "Q'aTheLesser", Location = gameState.RootRoom, Io = io };
        var nobody = "emptiness";

        // Act
        command.Execute($"dance {nobody}", tester);

        // Assert
        Assert.AreEqual("You", io.Outputs[0]);
        Assert.AreEqual(" spin around in circles with your invisible friend ", io.Outputs[1]);
        Assert.AreEqual(nobody, io.Outputs[2]);
        Assert.AreEqual("!", io.Outputs[3]);

        Assert.AreEqual("", io.SpeakOutputs[qa.Location][0]);
        Assert.AreEqual(tester.Name, io.SpeakOutputs[qa.Location][1]);
        Assert.AreEqual($" spins around in circles with their invisible friend ", io.SpeakOutputs[qa.Location][2]);
        Assert.AreEqual(nobody, io.SpeakOutputs[qa.Location][3]);
        Assert.AreEqual("!", io.SpeakOutputs[qa.Location][4]);
        Assert.AreEqual(" > ", io.SpeakOutputs[qa.Location][5]);
    }
}
