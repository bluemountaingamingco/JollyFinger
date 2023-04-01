using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevel
{
    public int NextLevelToUnlock { get; set; }
    public List<LevelDetails> LevelDetails { get; set; }

    public GameLevel() { }

    public GameLevel(int nextLevelToUnlock, List<LevelDetails> levelDetails)
    {
        NextLevelToUnlock = nextLevelToUnlock;
        LevelDetails = levelDetails;
    }
}

public class LevelDetails
{
    public int LevelIndex { get; set; }
    public int Stars { get; set; }

    public LevelDetails(int levelIndex, int stars)
    {
        LevelIndex = levelIndex;
        Stars = stars;
    }
}

public static class GameLevelBuilder
{
    public static GameLevel GameLevelWithDefaultValue()
    {
        var defaultLevelDetail = new List<LevelDetails>()
        {
            new LevelDetails(1, 0),
            new LevelDetails(2, 0),
            new LevelDetails(3, 0)
        };

        return new GameLevel(1, defaultLevelDetail);
    }
}

