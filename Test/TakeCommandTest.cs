using Sork.Commands;
using Sork.World;

namespace Sork.Test;

[TestClass]
public sealed class TakeCommandTests
{
    [TestMethod]
    public void Handle_ShouldReturnTrue_WhenInputIsCapitalized()
    {
        // Arrange
        var io = new TestInputOutput();
        var command = new TakeCommand(io);
        var gameState = GameState.Create();
        var tester = new Player { Name = "TesterTheGreat", Location = gameState.RootRoom, Io = io };

        // Act
        var result = command.Handles("TAKE sword", tester);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Execute_ShouldAddItemToPlayerInventory_WhenItemIsInRoom()
    {
        // Arrange
        var io = new TestInputOutput();
        var command = new TakeCommand(io);
        var gameState = GameState.Create();
        var tester = new Player { Name = "TesterTheGreat", Location = gameState.RootRoom, Io = io };

        // Act
        var result = command.Execute("TAKE sword", tester);

        // Assert
        Assert.IsTrue(result.IsHandled);
        Assert.IsFalse(result.RequestExit);
        Assert.AreEqual(1, tester.Inventory.Count);
        Assert.AreEqual("Sword", tester.Inventory[0].Name);
    }

    [TestMethod]
    public void Execute_ShouldError_WhenItemIsNotInRoom()
    {
        // Arrange
        var io = new TestInputOutput();
        var command = new TakeCommand(io);
        var gameState = GameState.Create();
        var tester = new Player { Name = "TesterTheGreat", Location = gameState.RootRoom, Io = io };

        // Act
        var result = command.Execute("TAKE candle", tester);

        // Assert
        Assert.IsFalse(result.IsHandled);
        Assert.IsFalse(result.RequestExit);
        Assert.AreEqual(0, tester.Inventory.Count);
        Assert.AreEqual(1, io.Outputs.Count);
        Assert.AreEqual("You don't see that item here.", io.Outputs[0]);
    }

    [TestMethod]
    public void Execute_ShouldOutputError_WhenNoParametersAreProvided()
    {
        // Arrange
        var io = new TestInputOutput();
        var command = new TakeCommand(io);
        var gameState = GameState.Create();
        var tester = new Player { Name = "TesterTheGreat", Location = gameState.RootRoom, Io = io };

        // Act
        var result = command.Execute("TAKE", tester);

        // Assert
        Assert.AreEqual(1, io.Outputs.Count);
        Assert.AreEqual("You must specify an item to take.", io.Outputs[0]);
        Assert.IsFalse(result.IsHandled);
        Assert.IsFalse(result.RequestExit);
    }
}