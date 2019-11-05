using UnityEngine;
using ow = Overview;

public class LevelScene : AnimatedObject
{
    public OverviewScene Overview;
    public Background Background;
    public LevelArea Level;
    public WinOverlay WinOverlay;

    private Vector2 _anchoredPosition;

    public void Awake()
    {
        WinOverlay.ResetAndHide();
        Level.ResetAndActivate();

        _anchoredPosition = ((RectTransform)transform).anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoBack();
        }

        if (Input.GetKeyDown(KeyCode.Q) && Input.GetKey(KeyCode.LeftControl))
        {
            AnimateWinScreen();
        }

        UpdateAnimation();
    }

    public void FadeToNextLevel()
    {
        ow.LoadNextLevel();
        FindObjectOfType<Level>().LoadCurrentLevel();
        WinOverlay.FadeOutToLeft();
        Level.FadeInFromRight();
    }

    public void AnimateWinScreen()
    {
        Level.FadeOutDown();
        WinOverlay.FadeInFromTop();
        ow.SetCompletedOnSelectedLevel(3);
    }

    public void GoBack()
    {
        FadeOut();
        Overview.FadeIn();
        Background.Brighten();
    }

    public void FadeOut()
    {
        AnimationQueue.Enqueue(AnimationWithCallback.Create(Animation.Create(SetRelativeXPosition, Easings.Functions.ExponentialEaseIn, 0.5f, 0, 1000), null, () => gameObject.SetActive(false)));
    }

    public void FadeIn()
    {
        WinOverlay.ResetAndHide();
        Level.ResetAndActivate();
        gameObject.SetActive(true);
        SetRelativeXPosition(1000);
        Delay(0.4f);
        Animate(SetRelativeXPosition, Easings.Functions.ExponentialEaseOut, 0.6f, 1000, 0);
    }

    public void SetRelativeXPosition(float position)
    {
        var t = (RectTransform)transform;
        t.anchoredPosition = _anchoredPosition + new Vector2(position, 0);
    }
}
