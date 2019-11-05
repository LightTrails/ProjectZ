using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class LevelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Material _material;

    public abstract void OnClick();

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _material.SetFloat("_hover", 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _material.SetFloat("_hover", 0);
    }

    void Start()
    {
        var rawImage = GetComponentInChildren<RawImage>();
        rawImage.material = new Material(rawImage.material);
        _material = rawImage.material;
    }
}
