using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public Vector2 Dimensions;
    public Color[] Schema;

    public void Awake()
    {
        LoadCurrentLevel();
    }

    public void LoadCurrentLevel()
    {
        if (Overview.SelectedTextAsset != null)
        {
            var loadedJson = JsonUtility.FromJson<SLevel>(Overview.SelectedTextAsset.text);
            CreateLevel(loadedJson);
        }
        else
        {
            var loadedJson = JsonUtility.FromJson<SLevel>(Resources.Load<TextAsset>("Levels/Level 1").text);
            CreateLevel(loadedJson);
        }
    }

    public void CreateLevel(SLevel sLevel)
    {
        Dimensions = sLevel.Dimensions;

        // var distance = 1.15f;

        var previewImage = FindObjectOfType<ResultImage>().GetComponentInChildren<GridLayoutGroup>();

        RemoveChildrensUnderGameObject(this.gameObject);
        RemoveChildrensUnderGameObject(previewImage.gameObject);

        var tiles = new List<Tile>();

        var tileByCoordinates = sLevel.Tiles.ToDictionary(x => x.Coordinate);

        var maxX = (int)sLevel.Dimensions.x;
        var maxY = (int)sLevel.Dimensions.y;

        // ((RectTransform)resultImage.transform).sizeDelta = Dimensions * (10 * distance) + new Vector2(10, 10);
#if UNITY_ANDROID
        var maxHeight = 550;
        var minCellSize = 100;
#else
        var maxHeight = 450;
        var minCellSize = 80;
#endif

        var totalCell = maxHeight / maxY;

        if (totalCell > minCellSize)
        {
            totalCell = minCellSize;
        }

        var spacing = totalCell * 0.1f;
        var cellSize = totalCell - spacing;

        var layout = GetComponent<GridLayoutGroup>();
        layout.constraintCount = maxX;
        layout.cellSize = new Vector2(cellSize, cellSize);
        layout.spacing = new Vector2(spacing, spacing);

        var resultImageSize = 80 * 0.9f;

        var exampleCell = resultImageSize / (float)(Math.Max(maxY, maxX));

        var exampleSpace = exampleCell * 0.20f;
        var exampleCellSize = exampleCell - exampleSpace;

        previewImage.cellSize = new Vector2(exampleCellSize, exampleCellSize);
        previewImage.spacing = new Vector2(exampleSpace, exampleSpace);
        previewImage.constraintCount = maxX;

        for (int i = 0; i < maxX; i++)
        {
            for (int j = 0; j < maxY; j++)
            {
                var prefab = Resources.Load("Level/Tile/TileNew") as GameObject;

                var tempobj = Instantiate(prefab, Vector3.zero, Quaternion.Euler(0, 0, 0));
                var tile = tempobj.GetComponent<Tile>();
                tile.X = i;
                tile.Y = j;

                tile.colorSchema = Schema;

                var coordinate = new Vector2(i, j);
                if (tileByCoordinates.ContainsKey(coordinate))
                {
                    tile.InitializeStates(tileByCoordinates[coordinate].State, tileByCoordinates[coordinate].EndState);
                }
                else
                {
                    tile.InitializeStates(0, 0);
                }

                tiles.Add(tile);

                tempobj.name = (i + 1) + "-" + (j + 1);
                tempobj.transform.SetParent(gameObject.transform);
                tempobj.transform.localPosition = new Vector3(0, 0, 0);
                tempobj.transform.localScale = new Vector3(1, 1, 1);

                tempobj.GetComponentInChildren<TileVisuals>().SetSize(new Vector2(cellSize, cellSize));
                // tempobj.transform.localPosition =  ((sLevel.Dimensions - new Vector2(1,1)) / 2 - new Vector2(i,j)) * distance;

                var previewTilePrefab = Resources.Load("Level/Buttons/ShowImageTile") as GameObject;
                var previewTile = Instantiate(previewTilePrefab, Vector3.zero, Quaternion.Euler(0, 0, 0));
                previewTile.transform.SetParent(previewImage.transform);
                previewTile.transform.localScale = new Vector3(1, 1, 1);
                previewTile.transform.localPosition = new Vector3(0, 0, 0);
                previewTile.name = (i + 1) + "-" + (j + 1);
                previewTile.GetComponent<RawImage>().color = Schema[tileByCoordinates[coordinate].EndState];



                /*var rtf = (RectTransform)previewTile.transform;
                rtf.localScale = new Vector3(1,1,1);
                rtf.anchoredPosition3D = ((sLevel.Dimensions - new Vector2(1,1)) / 2 - new Vector2(i,j)) * (10 * distance);
                previewTile.GetComponent<RawImage>().color = Schema[tileByCoordinates[coordinate].EndState];
                previewTile.name = (i + 1) + "-" + (j + 1);*/
            }
        }

        // var constraints = GetComponent<LevelConstraints>();
        // constraints.Constraints = sLevel.Constraints;

        foreach (var tile in tiles)
        {
            tile.InitializeNeighbours(tiles, maxX, maxY);
        }
    }

    void RemoveChildrensUnderGameObject(GameObject gameObject)
    {
        var objectsToRemove = new List<GameObject>();
        foreach (Transform item in gameObject.transform)
        {
            objectsToRemove.Add(item.gameObject);
        }

        foreach (GameObject child in objectsToRemove)
        {
            GameObject.DestroyImmediate(child);
        }
    }
}
