using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Background : AnimatedObject
{
    private Material _material;

    void Start()
    {
        _material = GetComponent<RawImage>().material;
        SetMaterialDarkness(0);
        _material.SetFloat("_world1", 1);
    }

    void Update()
    {
        UpdateAnimation();
    }

    public void Darken()
    {
        Animate(SetMaterialDarkness, Easings.Functions.ExponentialEaseIn, 0.5f, 0, 1);
    }

    public void Brighten()
    {
        Animate(SetMaterialDarkness, Easings.Functions.ExponentialEaseIn, 0.5f, 1, 0);
    }

    public void FadeToLevel(int fadeInLevel, int fadeOutLevel)
    {
        Animate(value => FadeLevel(value, fadeInLevel, fadeOutLevel), Easings.Functions.Linear, 0.5f, 1, 0);
    }

    public void FadeLevel(float value, int fadeInLevel, int fadeOutLevel)
    {
        _material.SetFloat("_world" + fadeInLevel, 1 - value);
        _material.SetFloat("_world" + fadeOutLevel, value);
    }

    public void SetMaterialDarkness(float value)
    {
        _material.SetFloat("_darken", value);
    }
}
