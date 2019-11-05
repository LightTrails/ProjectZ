using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileActionSelector : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        var levelSelector = GetComponentInParent<LevelSelector>();
        var tile = GetComponentInParent<Tile>();

        if (tile.CurrentAction != null)
        {
            tile.CurrentAction.Activate();

            levelSelector.UnselectAll();
        }
    }
}
