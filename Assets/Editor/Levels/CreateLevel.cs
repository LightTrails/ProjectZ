using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.IO;

public class CustomWindow  : EditorWindow 
{
     Vector2 levelDimensions = new Vector2(5,5);

    int Selected;
    TextAsset[] levels;

    [MenuItem("Tools/CreateLevel")]
    static void Create()
    {        
        EditorWindow.GetWindow(typeof(CustomWindow));        
    }

     void OnInspectorUpdate () {
         Repaint ();
     }

     void OnGUI()
     {         
        levels = Resources.LoadAll<TextAsset>("Levels");

        var level = FindObjectOfType<Level>() as Level;
         
        levelDimensions = EditorGUILayout.Vector2Field("Size:", levelDimensions);         

        string[] options = levels.Select(x=> x.name ).ToArray();

        Selected = EditorGUILayout.Popup("Input", Selected, options);

        if (GUILayout.Button("Create new level"))
        {
            level.CreateLevel(new SLevel(){
                Dimensions = levelDimensions
            });
        }

        if (GUILayout.Button("Load Level"))
        {
            var loadedContent = levels[Selected].text;
            var loadedJson = JsonUtility.FromJson<SLevel>(loadedContent);

            level.CreateLevel(loadedJson);
        }
    }
}
