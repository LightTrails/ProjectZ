using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SLevel
{
    public Vector2 Dimensions;
    public List<STile> Tiles = new List<STile>();

    public SConstraints Constraints;
}

[Serializable]
public class SConstraints
{
    public int MaxMoves;
}

[Serializable]
public class STile
{
    public Vector2 Coordinate;
    public int State;
    public int EndState;
}

[Serializable]
public class SOverview
{
    private const int NumberOfWorlds = 4;

    public int Version = 1;

    public List<SOverviewWorld> Worlds;

    public int AllStars => Worlds.SelectMany(x => x.Levels).Select(x => x.NumberOfStars).Sum();

    public SOverview Initialize()
    {
        Worlds = new List<SOverviewWorld>();

        Worlds.Add(new SOverviewWorld() { IsOpen = true, StarsRequired = 0, Number = 1, Selected = true });
        Worlds.Add(new SOverviewWorld() { StarsRequired = 25, Number = 2, IsNextWorld = true });
        Worlds.Add(new SOverviewWorld() { StarsRequired = 60, Number = 3 });
        Worlds.Add(new SOverviewWorld() { StarsRequired = 100, Number = 4 });

        foreach (var world in Worlds)
        {
            world.Initialize();
        }

        return this;
    }

    public void UpdateStars()
    {
        var starsLeft = AllStars;

        for (int i = 0; i < Worlds.Count; i++)
        {
            var world = Worlds[i];

            if (starsLeft > 0)
            {
                if (!world.IsOpen)
                {
                    world.StarsLeftToOpen = world.StarsRequired > starsLeft ? world.StarsRequired - starsLeft : 0;

                    if (world.StarsLeftToOpen == 0)
                    {
                        world.IsOpen = true;
                    }
                }

                starsLeft -= world.StarsRequired;
            }
        }
    }
}

[Serializable]
public class SOverviewWorld
{
    private const int NumberOfLevels = 20;

    public List<SOverviewLevel> Levels;

    public int Number;

    public bool IsOpen;

    public bool Selected;

    public bool IsNextWorld;

    public int StarsLeftToOpen;

    public int StarsRequired;

    internal SOverviewLevel GetNextLevel(SOverviewLevel selectedLevel)
    {
        var nextIndex = Levels.Select(x => x.Number).ToList().IndexOf(selectedLevel.Number) + 1;
        return Levels.Count > nextIndex ? Levels[nextIndex] : null;
    }

    internal SOverviewWorld Initialize()
    {
        Levels = new List<SOverviewLevel>();

        for (int i = 0; i < NumberOfLevels; i++)
        {
            Levels.Add(new SOverviewLevel().Initialize(i + 1 + 20 * (Number - 1)));
        }

        StarsLeftToOpen = StarsRequired;

        return this;
    }
}

[Serializable]
public class SOverviewLevel
{
    public int Number;

    public bool Completed;

    public int NumberOfStars;

    internal SOverviewLevel Initialize(int number)
    {
        Number = number;
        return this;
    }
}