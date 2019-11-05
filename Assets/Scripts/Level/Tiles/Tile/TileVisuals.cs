using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileVisuals : MonoBehaviour
{
    public TileIcon frontIcon = TileIcon.Blank;
    public Color frontColor = new Color(1, 1, 1, 1);

    public TileIcon backIcon = TileIcon.Blank;
    public Color backColor = new Color(1, 1, 1, 1);

    private GameObject Front => transform.Find("Front").gameObject;
    private GameObject Back => transform.Find("Back").gameObject;

    void Start()
    {
        UpdateVisuals();
    }

    void Update()
    {
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        SetIconAndColor(Front, frontIcon, frontColor);
        SetIconAndColor(Back, backIcon, backColor);
    }

    public void CreateMaterialAndSetColor(Color frontColor, Color backColor)
    {
        this.frontColor = frontColor;
        this.backColor = backColor;

        CreateMaterial(Front, frontColor);
        CreateMaterial(Back, backColor);
    }

    public void SetSize(Vector2 size)
    {
        Front.GetComponentInChildren<BoxCollider2D>().size = size;
        Back.GetComponentInChildren<BoxCollider2D>().size = size;
        transform.Find("Cube").transform.localScale = new Vector3(size.x * 0.95f, size.y * 0.95f, 3.99f);
    }

    private void CreateMaterial(GameObject gameObject, Color color)
    {
        var newMaterial = new Material(Shader.Find("Shader Graphs/Tile"));
        newMaterial.SetColor("_Color", color);
        gameObject.GetComponentInChildren<TileBackground>().GetComponent<RawImage>().material = newMaterial;
    }

    private void SetIconAndColor(GameObject gameObject, TileIcon tileIcon, Color color)
    {
        SetIcon(gameObject, tileIcon);
        SetColor(gameObject, color);
    }

    private void SetColor(GameObject gameObject, Color color)
    {
        gameObject.GetComponentInChildren<TileBackground>().GetComponent<RawImage>().material.SetColor("_Color", color);
    }

    private void SetIcon(GameObject gameObject, TileIcon tileIcon)
    {
        var iconInfo = TileIconsRepository.TileIconRendererDictionary[tileIcon];
        var texture = Resources.Load("Level/Icons/" + iconInfo.ResourceName);
        var icon = gameObject.GetComponentInChildren<Icon>();
        icon.transform.localRotation = Quaternion.Euler(0, 0, iconInfo.Rotation);
        icon.GetComponent<RawImage>().texture = texture as Texture2D;
    }

    internal void SetShineLevel(float value)
    {
        Front.GetComponentInChildren<TileBackground>().GetComponent<RawImage>().material.SetFloat("_Shine", value);
        Back.GetComponentInChildren<TileBackground>().GetComponent<RawImage>().material.SetFloat("_Shine", value);
    }
}
