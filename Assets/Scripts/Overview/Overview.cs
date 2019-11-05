using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class Overview : AnimatedObject
{
    public static TextAsset SelectedTextAsset;
    public static SOverviewLevel SelectedLevel;
    public static SOverviewWorld SelectedWorld;
    public static SOverview SelectedGame;

    public int Test;
    private Vector2 _anchoredPosition;
    private OverviewWorldSelector[] _worldSelectors;

    private const string levelFileName = "Overview.json";

    private static string FilePath;

    void Awake()
    {
        FilePath = Path.Combine(Application.persistentDataPath, levelFileName);

        _worldSelectors = FindObjectsOfType<OverviewWorldSelector>().OrderBy(x => x.SortOrder).ToArray();

        if (SelectedGame == null)
        {
            Load();
        }

        SetWorldSelectors();
        _anchoredPosition = ((RectTransform)transform).anchoredPosition;
    }

    internal static void SetCompletedOnSelectedLevel(int stars)
    {
        SelectedLevel.Completed = true;
        SelectedLevel.NumberOfStars = Math.Max(SelectedLevel.NumberOfStars, stars);
        UpdateSelectedWorldAndSaveState();
    }

    internal static void LoadNextLevel()
    {
        SelectedLevel = SelectedWorld.GetNextLevel(SelectedLevel);
        SelectedTextAsset = Resources.Load<TextAsset>(string.Format("Levels/Level{0:000}", +SelectedLevel.Number));
    }

    void Start()
    {
        SetLevelNumbers(SelectedWorld);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.LeftControl))
        {
            CreateNewLevelAndStoreIt();
        }

        UpdateAnimation();
    }

    public void ChangeWorld(SOverviewWorld world)
    {
        var newWorld = world.Number;
        var oldWorld = SelectedWorld.Number;

        FindObjectOfType<Background>().FadeToLevel(newWorld, oldWorld);

        AnimationQueue.Enqueue(AnimationWithCallback.Create(Animation.Create(SetRelativeYPosition, Easings.Functions.ExponentialEaseIn, 0.5f, 0, -1000), null, () => SetLevelNumbers(world)));
        AnimationQueue.Enqueue(Animation.Create(SetRelativeXPosition, Easings.Functions.ExponentialEaseOut, 0.5f, 1000, 0));
    }

    public void SetLevelNumbers(SOverviewWorld world)
    {
        SelectedWorld.Selected = false;
        world.Selected = true;
        SelectedWorld = world;

        var levelSelectors = GetComponentsInChildren<OverviewLevelSelector>();

        for (int i = 0; i < levelSelectors.Length; i++)
        {
            levelSelectors[i].SetLevelState(world.Levels[i]);
        }
    }

    internal static void LoadLevel(SOverviewLevel sLevel)
    {
        SelectedLevel = sLevel;
        SelectedTextAsset = Resources.Load<TextAsset>(string.Format("Levels/Level{0:000}", +sLevel.Number));
        FindObjectOfType<OverviewScene>().LoadLevel();
    }

    public void SetRelativeXPosition(float position)
    {
        var t = (RectTransform)transform;
        t.anchoredPosition = _anchoredPosition + new Vector2(position, 0);
    }

    public void SetRelativeYPosition(float position)
    {
        var t = (RectTransform)transform;
        t.anchoredPosition = _anchoredPosition + new Vector2(0, position);
    }

    private void Load()
    {
        if (!File.Exists(FilePath))
        {
            SelectedGame = new SOverview().Initialize();
            StoreLevel(SelectedGame);
        }
        else
        {
            SelectedGame = JsonUtility.FromJson<SOverview>(File.ReadAllText(FilePath));
        }
    }

    private void SetWorldSelectors()
    {
        SelectedWorld = SelectedGame.Worlds.FirstOrDefault(x => x.Selected);

        for (int i = 0; i < _worldSelectors.Length; i++)
        {
            _worldSelectors[i].InitializeState(SelectedGame.Worlds[i]);
        }
    }

    private void CreateNewLevelAndStoreIt()
    {
        var soverview = new SOverview().Initialize();
        StoreLevel(soverview);
        Load();
    }

    public static void UpdateSelectedWorldAndSaveState()
    {
        SelectedGame.UpdateStars();
        StoreState();
    }

    public static void StoreState()
    {
        StoreLevel(SelectedGame);
    }

    private static void StoreLevel(SOverview overview)
    {
        var json = JsonUtility.ToJson(overview);
        File.WriteAllText(FilePath, json);
    }
}