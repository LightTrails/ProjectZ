using System;
using UnityEngine;
using static Easings;

public class Animation : IAnimation
{
    private float interpolationValue = 0;
    private float duration;
    private Action<float> callback;
    private Functions easeFunction;

    private float minValue;
    private float maxValue;

    public static Animation Create(Action<float> callback, Functions easeFunction = Functions.Linear, float duration = 1.0f, float minValue = 0.0f, float maxValue = 1.0f) => new Animation(callback, easeFunction, duration, minValue, maxValue);
    public static Animation Delay(float duration = 1.0f) => new Animation(f => {}, Functions.Linear, duration);

    public Animation(Action<float> callback, Functions easeFunction, float duration = 1.0f, float minValue= 0.0f, float maxValue= 1.0f)
    {
        this.duration = duration;
        this.easeFunction = easeFunction;
        this.callback = callback;
        this.minValue = minValue;
        this.maxValue = maxValue;
    }

    public bool Update(float deltaTime)
    {
        interpolationValue += deltaTime / duration;
        
        if(interpolationValue > 1){
            interpolationValue = 1.0f;
        }

        callback(GetInterpolatedEasedValue());
        return interpolationValue >= 1;
    }

    private float GetInterpolatedEasedValue(){

        var diff = maxValue - minValue;        
        return minValue + Easings.Interpolate(interpolationValue, this.easeFunction) * diff;
    }
}
