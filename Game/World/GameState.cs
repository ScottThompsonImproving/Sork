namespace Sork.World;

public class GameState
{
    public required Player Player { get; set; }
    public required Room RootRoom { get; set; }

    public static GameState Create(IUserInputOutput io)
    {
        var tavern = new Room { Name = "Tavern", Description = "A warm, friendly establishment serving food, beverage, and entertainment." };
        var dungeon = new Room { Name = "Dungeon", Description = "A dark, dank crypt that is filled with silence and dread." };

        var sword = new Item { Name = "Sword", Description = "A double-edged blade that would cleave flesh like a knife through butter." };
        var mug = new Item { Name = "Mug", Description = "An empty piece of pottery with a handle that could hold enough liquid to quench any thirst." };

        tavern.Inventory.Add(mug);
        tavern.Inventory.Add(sword);

        tavern.Exits.Add("down", dungeon);
        dungeon.Exits.Add("up", tavern);

        io.WritePrompt("What is your name? ");
        string name = io.ReadInput();

        var player = new Player { Name = name, Location = tavern };
        return new GameState { Player = player, RootRoom = tavern };
    }
}
