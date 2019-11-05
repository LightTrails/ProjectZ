using UnityEngine;

public class OverviewScene : AnimatedObject
{
    public GameObject Level;
    public GameObject Background;

    private Vector2 _anchoredPosition;

    public void Awake()
    {
        _anchoredPosition = ((RectTransform)transform).anchoredPosition;
    }

    public void LoadLevel()
    {
        FadeOut();
        Level.GetComponent<LevelScene>().FadeIn();
        Level.GetComponentInChildren<Level>().LoadCurrentLevel();
        Background.GetComponent<Background>().Darken();
    }

    public void Update()
    {
        UpdateAnimation();
    }

    public void FadeOut()
    {
        AnimationQueue.Enqueue(AnimationWithCallback.Create(Animation.Create(SetRelativeXPosition, Easings.Functions.ExponentialEaseIn, 0.5f, 0, -1000), null, () => gameObject.SetActive(false)));
    }

    public void FadeIn()
    {
        CreateFadeInDelay();

        gameObject.SetActive(true);
        SetRelativeXPosition(-1000);
        Delay(0.4f);
        Animate(SetRelativeXPosition, Easings.Functions.ExponentialEaseOut, 0.6f, -1000, 0);
    }

    public void SetRelativeXPosition(float position)
    {
        var t = (RectTransform)transform;
        t.anchoredPosition = _anchoredPosition + new Vector2(position, 0);
    }

    #region Fade in delay
    private void CreateFadeInDelay()
    {
        foreach (var selector in GetComponentsInChildren<OverviewLevelSelector>())
        {
            selector.FadeInDelay();
        }

        foreach (var selector in GetComponentsInChildren<OverviewWorldSelector>())
        {
            selector.FadeInDelay();
        }

        foreach (var star in GetComponentsInChildren<OverviewStar>())
        {
            star.FadeInDelay();
        }
    }
    #endregion

}

