using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tile : AnimatedObject
{
    public int X;
    public int Y;

    public Tile Left;
    public Tile Right;
    public Tile Up;
    public Tile Down;

    public IAction CurrentAction = null;
    public TileVisuals visual;

    public int State;
    public int StartState;
    public int EndState;

    public Color[] colorSchema;

    public LevelActions LevelActions;

    void Start()
    {
        LevelActions = FindObjectOfType<LevelActions>();
        visual = gameObject.GetComponentInChildren<TileVisuals>();
        // ShowFront(0.1f);
        Delay(1);
        Delay(X * 0.1f);
        Animate(AnimationWithCallback.Create(
            Animation.Create(UpdateRotation, Easings.Functions.QuadraticEaseInOut, 1.0f, 0, 180.0f),
            null,
            () =>
            {
                visual.backColor = colorSchema[0];
                visual.backIcon = TileIcon.Blank;
            }
        ));
    }

    void Awake()
    {

    }

    void Update()
    {
        UpdateAnimation();
    }

    void UpdateRotation(float value)
    {
        gameObject.transform.rotation = Quaternion.Euler(0, value, 0);
    }

    void UpdateColor(float value)
    {
        visual.frontColor = Color.Lerp(colorSchema[State], colorSchema[EndState], value);
    }

    public void ShowGoalState()
    {
        AnimationQueue.Enqueue(Animation.Create(UpdateColor, Easings.Functions.QuadraticEaseInOut, 1.0f, 0.0f, 1.0f));
        AnimationQueue.Enqueue(Animation.Delay(2));
        AnimationQueue.Enqueue(Animation.Create(UpdateColor, Easings.Functions.QuadraticEaseOut, 1.0f, 1.0f, 0.0f));
    }

    public void InitializeStates(int currentState, int endState)
    {
        State = currentState;
        StartState = currentState;
        EndState = endState;

        var visual = gameObject.GetComponentInChildren<TileVisuals>();
        visual.CreateMaterialAndSetColor(colorSchema[currentState], colorSchema[endState]);
    }

    public IEnumerable<Tile> GetSurroundingTiles()
    {
        var result = new List<Tile>();
        if (Up != null)
        {
            result.Add(Up);
        }
        if (Down != null)
        {
            result.Add(Down);
        }
        if (Left != null)
        {
            result.Add(Left);
        }
        if (Right != null)
        {
            result.Add(Right);
        }
        return result;
    }

    public void InitializeNeighbours(IEnumerable<Tile> tiles, int maxX, int maxY)
    {
        var lookUp = tiles.ToDictionary(tile => (tile.X, tile.Y));

        if (X > 0)
        {
            Right = lookUp[(X - 1, Y)];
        }

        if (X < maxX - 1)
        {
            Left = lookUp[(X + 1, Y)];
        }

        if (Y > 0)
        {
            Up = lookUp[(X, Y - 1)];
        }

        if (Y < maxY - 1)
        {
            Down = lookUp[(X, Y + 1)];
        }

    }

    public void Shine()
    {
        Animate(SetShineLevel);
    }

    public void SetShineLevel(float value)
    {
        visual.SetShineLevel(value);
    }

    public void StartAnimation(IAnimation animation)
    {
        AnimationQueue.Enqueue(animation);
    }

    internal void ShowBack(float duration = 1.0f)
    {
        AnimationQueue.Enqueue(Animation.Create(UpdateRotation, Easings.Functions.QuadraticEaseInOut, duration, 180.0f, 0.0f));
    }

    internal void ShowFront(float duration = 1.0f)
    {
        AnimationQueue.Enqueue(Animation.Create(UpdateRotation, Easings.Functions.QuadraticEaseInOut, duration, 0, 180.0f));
    }

    internal void TurnToOriginalColor()
    {
        var animation = AnimationWithCallback.Create(
            Animation.Create(UpdateRotation, Easings.Functions.QuadraticEaseInOut, 1.0f, 0, 180.0f),
            () =>
            {
                visual.frontColor = colorSchema[StartState];

                visual.backColor = colorSchema[State];

                visual.UpdateVisuals();
                State = StartState;
            },
            () =>
            {
                visual.backColor = colorSchema[0];
                visual.backIcon = TileIcon.Blank;
            });

        AnimationQueue.Enqueue(animation);
    }

    internal void ShowColorSideAndHideAction()
    {
        if (CurrentAction != null)
        {

            var animation = AnimationWithCallback.Create(
                CurrentAction.CreateFadeOutAnimation(),
                null,
                () =>
                {
                    visual.backIcon = TileIcon.Blank;
                    visual.backColor = colorSchema[0];
                }
            );

            AnimationQueue.Enqueue(animation);

            CurrentAction = null;
        }
    }

    internal void SetActionAndShowActionSide(string actionType, int state)
    {
        var currentAction = ActionFactory.CreateAction(actionType, this, state);
        CurrentAction = currentAction;

        var animation = AnimationWithCallback.Create(
                currentAction.CreateFadeInAnimation(),
                () =>
                {
                    visual.backColor = colorSchema[state];
                    visual.backIcon = currentAction.Icon;
                }
            );

        AnimationQueue.Enqueue(animation);
    }
}
