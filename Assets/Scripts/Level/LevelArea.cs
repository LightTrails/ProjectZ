using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelArea : AnimatedObject
{
    private void Awake()
    {

    }

    private void Update()
    {
        UpdateAnimation();
    }

    internal void ResetAndActivate()
    {
        gameObject.SetActive(true);
        UpdateYPostion(0);
    }

    public void UpdateYPostion(float position)
    {
        transform.localPosition = new Vector3(0.0f, position, 0.0f);
    }

    public void UpdateXPostion(float position)
    {
        transform.localPosition = new Vector3(position, 0.0f, 0.0f);
    }

    internal void FadeInFromRight()
    {
        Delay(0.3f);
        Animate(UpdateXPostion, Easings.Functions.ExponentialEaseOut, 0.6f, 1000, 0);
    }

    internal void FadeOutDown()
    {
        Animate(UpdateYPostion, Easings.Functions.ExponentialEaseIn, 0.6f, 0, -1000);
    }
}
