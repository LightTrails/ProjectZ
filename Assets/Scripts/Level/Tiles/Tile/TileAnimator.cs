using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Easings;

public class AnimatedObject : MonoBehaviour
{
    protected Queue<IAnimation> AnimationQueue = new Queue<IAnimation>();

    protected void UpdateAnimation()
    {
        if (AnimationQueue.Count == 0)
        {
            return;
        }

        var currentAnimation = AnimationQueue.Peek();

        if (currentAnimation != null)
        {
            if (currentAnimation.Update(Time.deltaTime))
            {
                AnimationQueue.Dequeue();
            }
        }
    }

    protected void Animate(IAnimation animation)
    {
        AnimationQueue.Enqueue(animation);
    }

    protected void Delay(float duration = 1.0f)
    {
        AnimationQueue.Enqueue(Animation.Delay(duration));
    }

    protected void AnimationCallBack(Action callback)
    {
        AnimationQueue.Enqueue(AnimationCallback.Create(callback));
    }

    protected void Animate(Action<float> callback, Functions easeFunction = Functions.Linear, float duration = 1.0f, float minValue = 0.0f, float maxValue = 1.0f)
    {
        AnimationQueue.Enqueue(Animation.Create(callback, easeFunction, duration, minValue, maxValue));
    }
}
