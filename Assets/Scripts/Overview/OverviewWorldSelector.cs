using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OverviewWorldSelector : AnimatedObject, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private TextMeshProUGUI _textMesPro;

    public Material Material;

    private bool _isComponentOpen = false;
    public bool IsOpen => World.IsOpen;

    public bool IsSelected => World.Selected;

    public int SortOrder;

    public int WorldNumber => World.Number;

    public SOverviewWorld World { get; private set; }

    void Start()
    {
        _textMesPro = GetComponentInChildren<TextMeshProUGUI>(true);
    }

    public void InitializeState(SOverviewWorld world)
    {
        World = world;
        _isComponentOpen = World.IsOpen;

        Material = new Material(GetComponent<RawImage>().material);
        GetComponent<RawImage>().material = Material;

        if (world.IsOpen)
        {
            UpdateMaterialAnimation(1);
            DisableChildren();
        }
    }

    void Update()
    {
        _textMesPro.SetText(World.StarsLeftToOpen.ToString());

        if (_isComponentOpen != IsOpen)
        {
            _isComponentOpen = true;
            OpenWorld();
        }

        base.UpdateAnimation();
    }

    internal void FadeInDelay()
    {
        Delay(1.0f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Material.SetFloat("_hover", 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Material.SetFloat("_hover", 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Input.GetKey(KeyCode.LeftControl))
        {
            if (!IsSelected && IsOpen)
            {
                var selectors = FindObjectsOfType<OverviewWorldSelector>();

                foreach (var selector in selectors)
                {
                    selector.UnSelect();
                }

                Select();
            }
        }
        else
        {
            Activate();
        }

    }

    public void UnSelect()
    {
        World.Selected = false;
        Material.SetInt("_selected", IsSelected ? 1 : 0);
    }

    public void Select()
    {
        World.Selected = true;
        Material.SetInt("_selected", IsSelected ? 1 : 0);
        FindObjectOfType<Overview>().ChangeWorld(World);
    }

    private void Activate()
    {
        if (IsOpen)
        {
            return;
        }


        OpenWorld();
    }

    private void UpdateMaterialAnimation(float value)
    {
        Material.SetFloat("_activeanimation", value);
    }

    internal void OpenWorld()
    {
        World.IsOpen = true;

        var materialAnimation = Animation.Create(UpdateMaterialAnimation, Easings.Functions.Linear, 0.6f);
        var callBack = AnimationWithCallback.Create(materialAnimation, () =>
        {
            DisableChildren();
        });

        var fullAnimation = new CompositeAnimation(callBack);

        AnimationQueue.Enqueue(fullAnimation);
    }

    private void DisableChildren()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
