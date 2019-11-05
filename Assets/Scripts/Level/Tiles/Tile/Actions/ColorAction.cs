using System;
using UnityEngine;

public abstract class ColorAction : IAction
{
    public abstract TileIcon Icon { get; }

    protected abstract void UpdateRotation(Tile tile, float value);

    protected abstract Tile NextTile(Tile tile);

    protected Tile tile;
    private readonly int newState;

    public ColorAction(Tile tile, int newState)
    {
        this.tile = tile;
        this.newState = newState;
    }

    public void Activate()
    {
        var newState = this.newState;

        // Disable action so not to turn it over
        tile.CurrentAction = null;

        var delay = 0.0f;
        var currentTile = tile;

        while (currentTile != null)
        {
            var tileToRender = currentTile;

            if (currentTile != tile)
            {
                tileToRender.visual.backColor = tileToRender.visual.frontColor;
            }

            var animation = CreateTurnColorAnimation(tileToRender, newState);
            currentTile.StartAnimation(Animation.Delay(delay));
            currentTile.StartAnimation(animation);

            delay += AnimationConstants.TurnColorAnimationDelay;
            currentTile = NextTile(currentTile);
        }

        tile.LevelActions.CheckEndState();
        // tile.levelConstraints.MoveTaken();
    }

    public IAnimation CreateTurnColorAnimation(Tile tile, int newState)
    {
        tile.State = newState;

        return
                AnimationWithCallback.Create(
                    Animation.Create(value => UpdateRotation(tile, value), Easings.Functions.QuadraticEaseInOut, AnimationConstants.TurnAnimationSpeed * 2, 0.0f, 180.0f),
                    () =>
                    {
                        tile.visual.frontColor = tile.colorSchema[newState];
                        tile.visual.UpdateVisuals();
                    },
                    () =>
                    {
                        tile.visual.backColor = Color.white;
                        tile.visual.backIcon = TileIcon.Blank;
                    });
    }

    public IAnimation CreateFadeInAnimation()
    {
        return Animation.Create(x => UpdateRotation(tile, x), Easings.Functions.QuadraticEaseInOut, AnimationConstants.TurnAnimationSpeed, 180.0f, 0.0f);
    }

    public IAnimation CreateFadeOutAnimation()
    {
        return Animation.Create(x => UpdateRotation(tile, x), Easings.Functions.QuadraticEaseInOut, AnimationConstants.TurnAnimationSpeed, 0.0f, 180.0f);
    }
}