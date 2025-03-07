# Additional Commands

The homework assignment is to plan how we could have `Aliases` for `Commands`, `Items`, etc.

Generally, creating a class for each command would allow for similar customization as the [`Exit`](Game/World/Exit.cs) class.

## `Dance` Class

The structure of the `Dance` class would be similar to the `Exit` class:

- A `Name` property, identifying the command
- A `Description` property, describing the command
- An `Aliases` property, listing the different ways the command can be written
- A `Partner` property, listing the different people the player can dance with

## `Sing` Class

The `Sing` class would also have a similar structure to the `Dance` class, but with a different parameter:

- A `Name` property, identifying the command
- A `Description` property, describing the command
- An `Aliases` property, listing the different ways the command can be written
- A `Song` property, listing the song or style the player sings

## `Laugh` Class

The `Laugh` command adds a different property to the common ones listed above:

- A `Name` property, identifying the command
- A `Description` property, describing the command
- An `Aliases` property, listing the different ways the command can be written
- A `At` property, listing the person or object the player is laughing at
