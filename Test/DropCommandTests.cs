using Sork.Commands;
using Sork.World;

namespace Sork.Test;

[TestClass]
public class DropCommandTests
{
    [TestMethod]
    public void Handles_ShouldReturnTrue_WhenInputIsDrop()
    {
        // Arrange
        var command = new DropCommand(new TestInputOutput());

        // Act
        var result = command.Handles("drop sword");

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Execute_ShouldReturnError_WhenNoItemSpecified()
    {
        // Arrange
        var io = new TestInputOutput();
        var command = new DropCommand(io);
        var gameState = GameState.Create();
        var player = new Player { Name = "Tester the Great", Location = gameState.RootRoom, Io = io };

        // Act
        var result = command.Execute("drop", player);

        // Assert
        Assert.IsTrue(result.IsHandled);
        Assert.IsFalse(result.RequestExit);
        Assert.AreEqual("What are you trying to drop again?", io.Outputs.Last());
    }

    [TestMethod]
    public void Execute_ShouldReturnError_WhenItemNotInInventory()
    {
        // Arrange
        var io = new TestInputOutput();
        var command = new DropCommand(io);
        var gameState = GameState.Create();
        var player = new Player { Name = "Tester the Great", Location = gameState.RootRoom, Io = io };

        // Act
        var result = command.Execute("drop nonexistent", player);

        // Assert
        Assert.IsTrue(result.IsHandled);
        Assert.IsFalse(result.RequestExit);
        Assert.AreEqual("You have to have to be holding a nonexistent to drop it.", io.Outputs.Last());
    }

    [TestMethod]
    public void Execute_ShouldDropItem_WhenItemInInventory()
    {
        // Arrange
        var io = new TestInputOutput();
        var command = new DropCommand(io);
        var gameState = GameState.Create();
        var player = new Player { Name = "Tester the Great", Location = gameState.RootRoom, Io = io };
        var sword = player.Location.Inventory.First(i => i.Name == "Sword");
        player.Location.Inventory.Remove(sword);
        player.Inventory.Add(sword);

        // Act
        var result = command.Execute("drop Sword", player);

        // Assert
        Assert.IsTrue(result.IsHandled);
        Assert.IsFalse(result.RequestExit);
        Assert.AreEqual("The Sword falls from your hands.", io.Outputs.Last());
        Assert.IsFalse(player.Inventory.Any(i => i.Name == "Sword"));
        Assert.IsTrue(player.Location.Inventory.Any(i => i.Name == "Sword"));
    }
}