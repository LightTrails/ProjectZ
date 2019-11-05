using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelActions : AnimatedObject
{
    public void CheckEndState()
    {
        bool endGame = true;

        foreach (var tile in GetComponentsInChildren<Tile>())
        {
            endGame = endGame && tile.EndState == tile.State;
        }

        if (endGame)
        {
            AnimationCallBack(() =>
            {
                foreach (var tile in GetComponentsInChildren<Tile>())
                {
                    tile.Shine();
                }
            });

            Delay(1.2f);

            AnimationCallBack(() =>
            {
                FindObjectOfType<LevelScene>().AnimateWinScreen();
            });
        }
    }

    private void Update()
    {
        UpdateAnimation();
    }

    public void ResetLevel()
    {
        GetComponent<LevelSelector>().UnselectAll();

        foreach (var tile in GetComponentsInChildren<Tile>())
        {
            tile.TurnToOriginalColor();
        }
    }


    public void ShowEndState()
    {
        GetComponent<LevelSelector>().UnselectAll();

        foreach (var tile in GetComponentsInChildren<Tile>())
        {
            tile.ShowGoalState();
        }
    }
}
