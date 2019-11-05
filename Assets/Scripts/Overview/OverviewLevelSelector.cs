using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OverviewLevelSelector : AnimatedObject, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private TextMeshProUGUI _textMesPro;
    private OverviewStar[] _stars;
    public Material Material { get; set; }
    public SOverviewLevel SLevel { get; private set; }
    public int Number => SLevel.Number;

    private int _componentNumberOfStars;
    public int NumberOfStars => SLevel.NumberOfStars;

    private bool _componentHasBeenCompleted;
    public bool HasBeenCompleted => SLevel.Completed;

    void Awake()
    {
        _stars = GetComponentsInChildren<OverviewStar>();
        _textMesPro = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        _textMesPro.SetText(Number.ToString());

        if (HasBeenCompleted && !_componentHasBeenCompleted)
        {
            Animate(SetAnimation, Easings.Functions.Linear, 1.5f);
            _componentHasBeenCompleted = HasBeenCompleted;
        }

        if (_componentNumberOfStars < NumberOfStars && _componentHasBeenCompleted)
        {
            float waitTime = 0;
            for (int i = _componentNumberOfStars; i < NumberOfStars; i++)
            {
                _stars[i].FillStarByAnimation(waitTime);
                waitTime += 0.25f;
            }
            _componentNumberOfStars = NumberOfStars;

        }

        UpdateAnimation();
    }

    internal void FadeInDelay()
    {
        Delay(1.0f);
    }

    public void SetLevelState(SOverviewLevel level)
    {
        GetComponent<RawImage>().material = new Material(GetComponent<RawImage>().material);
        Material = GetComponent<RawImage>().material;

        SLevel = level;

        _componentHasBeenCompleted = HasBeenCompleted;
        _componentNumberOfStars = NumberOfStars;

        if (HasBeenCompleted)
        {
            SetAnimation(1);
        }
        else
        {
            SetAnimation(0);
        }

        for (int i = 0; i < _stars.Length; i++)
        {
            _stars[i].Initialize(i < level.NumberOfStars);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            SLevel.Completed = true;

            if (SLevel.NumberOfStars < 3)
            {
                SLevel.NumberOfStars = 3;
            }

            Overview.UpdateSelectedWorldAndSaveState();
        }
        else
        {
            Overview.LoadLevel(SLevel);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Material.SetInt("_hover", 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Material.SetInt("_hover", 0);
    }

    private void SetAnimation(float value)
    {
        Material.SetFloat("_animation", value);
    }
}
