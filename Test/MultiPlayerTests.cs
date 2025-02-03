using Sork.Commands;
using Sork.World;

namespace Sork.Test;

[TestClass]
public class MultiPlayerTests
{
    private TestInputOutput io = null!;
    private TakeCommand take = null!;
    private MoveCommand move = null!;
    private DropCommand drop = null!;
    private LookCommand look = null!;
    private GameState gameState = null!;
    private Player tester = null!;
    private Player qa = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        // Arrange
        io = new TestInputOutput();
        take = new TakeCommand(io);
        move = new MoveCommand(io);
        drop = new DropCommand(io);
        look = new LookCommand(io);
        gameState = GameState.Create();
        tester = new Player { Name = "TesterTheGreat", Location = gameState.RootRoom, Io = io };
        qa = new Player { Name = "Q'aTheLesser", Location = gameState.RootRoom, Io = io };
    }

    [TestMethod]
    public void Take_Move_Drop_ShouldAlterRoomInventory_WhenTwoPlayersArePresent()
    {
        // Act
        take.Execute("take sword", tester);
        move.Execute("move down", tester);
        drop.Execute("drop sword", tester);
        var qaViewsTavern = look.Execute("look", qa);
        move.Execute("move down", qa);
        var qaViewsDungeon = look.Execute("look", qa);

        // Assert
        Assert.IsFalse(gameState.RootRoom.Inventory.Any(i => i.Name == "Sword"));
        Assert.IsFalse(tester.Inventory.Any(i => i.Name == "Sword"));
        Assert.IsFalse(qa.Inventory.Any(i => i.Name == "Sword"));
        Assert.IsTrue(qaViewsTavern.IsHandled);
        Assert.IsTrue(qaViewsDungeon.IsHandled);
        Assert.IsTrue(io.Outputs.Any(o => o == "Tavern"));
        Assert.IsTrue(io.Outputs.Any(o => o == "Dungeon"));
    }
}
