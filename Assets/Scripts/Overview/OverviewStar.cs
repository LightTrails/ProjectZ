using UnityEngine;
using UnityEngine.UI;

public class OverviewStar : AnimatedObject
{

    private bool _filled;
    private Material _material;

    void Start()
    {
        SetMaterialIfNotSet();
    }

    private void SetMaterialIfNotSet()
    {
        if (_material == null)
        {
            GetComponent<RawImage>().material = new Material(GetComponent<RawImage>().material);
            _material = GetComponent<RawImage>().material;
        }
    }

    void Update()
    {
        UpdateAnimation();
    }

    public void FadeInDelay()
    {
        Delay(1.0f);
    }

    public void FillStarByAnimation(float waitTime)
    {
        if (!_filled)
        {
            _filled = true;
            AnimationQueue.Enqueue(Animation.Delay(waitTime));
            AnimationQueue.Enqueue(Animation.Create(SetAnimation, Easings.Functions.Linear, 0.75f));
        }
    }

    internal void Initialize(bool starsFilled)
    {
        SetMaterialIfNotSet();

        _filled = starsFilled;

        if (starsFilled)
        {
            SetAnimation(1);
        }
        else
        {
            SetAnimation(0);
        }
    }

    private void SetAnimation(float value)
    {
        _material.SetFloat("_animation", value);
    }
}
