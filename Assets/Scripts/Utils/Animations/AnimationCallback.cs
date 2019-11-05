using System;

public class AnimationCallback : IAnimation
{
    public Action Callback { get; }

    public static AnimationCallback Create(Action callback) => new AnimationCallback(callback);

    public AnimationCallback(Action callback)
    {
        Callback = callback;
    }

    public bool Update(float deltaTime)
    {
        Callback();
        return true;
    }
}