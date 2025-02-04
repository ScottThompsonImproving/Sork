namespace Sork.World;

public class GameState
{
    public List<Player> Players { get; set; } = [];
    public required Room RootRoom { get; set; }

    public static GameState Create()
    {
        var tavern = new Room { Name = "Tavern", Description = "A warm, friendly establishment serving food, beverage, and entertainment." };
        var dungeon = new Room { Name = "Dungeon", Description = "A dark, dank crypt that is filled with silence and dread." };

        var sword = new Item { Name = "Sword", Description = "A double-edged blade that would cleave flesh like a knife through butter." };
        var mug = new Item { Name = "Mug", Description = "An empty piece of pottery with a handle that could hold enough liquid to quench any thirst." };

        tavern.Inventory.Add(mug);
        tavern.Inventory.Add(sword);

        tavern.Exits.Add(new Exit
        {
            Name = "Dungeon",
            Destination = dungeon,
            Aliases = { "d", "down", "do", "downstairs" },
            Description = "Those creepy kinda stairs that beckon but cause you pause."
        });
        dungeon.Exits.Add(new Exit
        {
            Name = "Tavern Door",
            Destination = tavern,
            Aliases = { "u", "up", "do", "upstairs" },
            Description = "A sturdy wooden door with a brass knocker."
        });

        return new GameState { RootRoom = tavern };
    }
}
