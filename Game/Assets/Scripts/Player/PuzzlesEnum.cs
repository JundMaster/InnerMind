using System;

/// <summary>
/// List of puzzle names ingame
/// </summary>
[Flags]
public enum PuzzlesEnum
{
    Default = 0,
    Puzzle1 = 1,
    Puzzle2 = 2,
    Puzzle3 = 4,
    Puzzle4 = 8,
    Puzzle5 = 16,
    Puzzle6 = 32,
    Puzzle7 = 64,
    Puzzle8 = 128,
    Puzzle9 = 256,
    Puzzle10 = 512
}
