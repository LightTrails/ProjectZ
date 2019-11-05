using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackButton : LevelButton
{
    public override void OnClick()
    {
        FindObjectOfType<LevelScene>().GoBack();
    }
}
