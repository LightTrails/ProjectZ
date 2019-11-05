using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinOverlay : AnimatedObject
{
    internal void FadeInFromTop()
    {        
        gameObject.SetActive(true);
        Delay(0.3f);
        UpdateYPostion(1000);

        var stars = GetComponentsInChildren<OverviewStar>();

        foreach (var star in stars)
        {
            star.Initialize(false);
        }

        Animate(AnimationWithCallback.Create(Animation.Create(UpdateYPostion, Easings.Functions.QuadraticEaseOut, 0.6f, 1000, 0), null, () =>
        {
            foreach (var star in stars)
            {
                star.FillStarByAnimation(0);
            }
        }
        ));
    }

    internal void FadeOutToLeft()
    {
        Animate(AnimationWithCallback.Create(Animation.Create(UpdateXPostion, Easings.Functions.QuadraticEaseIn, 0.6f, 0, -1000), null, ResetAndHide));
    }

    public void UpdateYPostion(float position)
    {
        transform.localPosition = new Vector3(0.0f, position, 0.0f);
    }

    public void UpdateXPostion(float position)
    {
        transform.localPosition = new Vector3(position, 0.0f, 0.0f);
    }

    public void ResetPosition()
    {
        transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void Update()
    {
        UpdateAnimation();
    }

    internal void ResetAndHide()
    {
        ResetPosition();
        ResetStars();

        gameObject.SetActive(false);
    }

    private void ResetStars()
    {
        var stars = GetComponentsInChildren<OverviewStar>();
        foreach (var star in stars)
        {
            star.Initialize(false);
        }
    }
}

