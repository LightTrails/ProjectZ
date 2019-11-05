using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileSelector : MonoBehaviour, IPointerClickHandler
{
    public bool Selected;
    public bool IsAction;
    Tile tile;

    void Start()
    {
        tile = GetComponentInParent<Tile>();
    }

    void Update()
    {
    }

    internal void UnSelect()
    {
        Selected = false;
        // GetComponentInChildren<Overlay>().DarkenColor = true;

        foreach (var item in tile.GetSurroundingTiles())
        {
            tile.ShowColorSideAndHideAction();
        }
    }

    internal void Select()
    {
        Selected = true;
        // GetComponentInChildren<Overlay>().DarkenColor = false;        
        var state = GetComponentInParent<Tile>().State;

        if (tile.Up != null)
        {
            ShowActionWithIconAndColor(tile.Up, nameof(UpAction), state);
        }

        if (tile.Down != null)
        {
            ShowActionWithIconAndColor(tile.Down, nameof(DownAction), state);
        }

        if (tile.Left != null)
        {
            ShowActionWithIconAndColor(tile.Left, nameof(LeftAction), state);
        }

        if (tile.Right != null)
        {
            ShowActionWithIconAndColor(tile.Right, nameof(RightAction), state);
        }
    }

    private void ShowActionWithIconAndColor(Tile tile, string action, int state)
    {
        tile.SetActionAndShowActionSide(action, state);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var levelSelector = GetComponentInParent<LevelSelector>();
        if (tile.State > 1)
        {
            if (Selected)
            {
                levelSelector.UnselectAll();
            }
            else
            {
                levelSelector.SelectTile(GetComponentInParent<TileSelector>());
            }
        }
    }
}
