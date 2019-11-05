using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    internal void SelectTile(TileSelector tileSelected)
    {
        UnselectAll();    
        tileSelected.Select();        
    }

    internal void UnselectAll(){
        var tiles = GetComponentsInChildren<TileSelector>();
        foreach (var item in tiles)
        {
            item.UnSelect();
        }
    }
}
