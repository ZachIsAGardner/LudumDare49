using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : SingleInstance<Game>
{
    public static bool IsPaused = false;
    public static bool IsTransitioning = false;
    public static bool GotCowPart = false;
    public static bool GotButtonPart = false;
    public static bool GotBossPart = false;

    private Image cowPartImage;
    private Image bossPartImage;
    private Image buttonPartImage;

    public static GameObject Dynamic
    {
        get
        {
            if (dynamic == null)
            {
                dynamic = GameObject.Find("_Dynamic");
                if (dynamic == null) dynamic = new GameObject("_Dynamic");
            }
            return dynamic;
        }
    }
    private static GameObject dynamic;
    private static GameObject dynamicCanvasInstance;

    private SceneTransition sceneTransitionInstance;
    private static int spawnPoint = 0;

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (GotCowPart)
        {
            cowPartImage = cowPartImage ?? GameObject.Find("PartOne").GetComponentInChildren<Image>();
            cowPartImage.color = Color.white;
        }

        if (GotBossPart)
        {
            bossPartImage = bossPartImage ?? GameObject.Find("PartTwo").GetComponentInChildren<Image>();
            bossPartImage.color = Color.white;
        }

        if (GotButtonPart)
        {
            buttonPartImage = buttonPartImage ?? GameObject.Find("PartThree").GetComponentInChildren<Image>();
            buttonPartImage.color = Color.white;
        }
    }

    /// <summary>
    /// Load a scene with the provided name.
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    public static async Task LoadAsync(string sceneName, SceneTransition sceneTransitionPrefab, int spawnPoint = 0)
    {
        if (IsTransitioning) return;

        Game.spawnPoint = spawnPoint;

        IsTransitioning = true;

        // Transition out
        Instance.sceneTransitionInstance = Instantiate(sceneTransitionPrefab);
        Instance.sceneTransitionInstance.Out();

        while (!Instance.sceneTransitionInstance.DidReachHalfway)
        {
            await new WaitForUpdate();
        }

        await new WaitForSeconds(0.25f);

        SceneManager.LoadScene(sceneName);

        // Transition in
        Instance.sceneTransitionInstance.In();

        IsTransitioning = false;
    }

    /// <summary>
    /// Create a new GameObject in the _Dynamic folder.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static GameObject New(string name)
    {
        GameObject go = new GameObject();
        go.name = name;
        go.transform.parent = Dynamic.transform;
        return go;
    }

    /// <summary>
    /// Create a new GameObject in the _Dynamic folder.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static GameObject New(GameObject prefab)
    {
        var inst = Instantiate(prefab, Dynamic.transform);
        return inst;
    }

    /// <summary>
    /// Spawns a copy of the provided prefab inside the DynamicCanvasFolder GameObject.
    /// <param name="prefab">The GameObject to create a copy of.</param>
    /// <param name="hierarchy">The sorting layer to place the prerab instance into. Higher numbers have priority of lower ones.</param>
    /// </summary>
    public static GameObject NewCanvasElement(GameObject prefab, int hierarchy = 0)
    {
        List<GameObject> folders = new List<GameObject>();

        int i = 0;
        while (i <= hierarchy)
        {
            string name = $"_DynamicCanvasFolder_{i}";
            GameObject folder = GameObject.Find(name);

            // Create new folder
            if (folder == null)
            {
                if (dynamicCanvasInstance == null)
                {
                    dynamicCanvasInstance = Instantiate(Prefabs.Get("Canvas"));
                    dynamicCanvasInstance.name = "DynamicCanvas";
                }

                folder = new GameObject(name);
                folder.AddComponent<RectTransform>();

                folder.GetComponent<RectTransform>().SetParent(dynamicCanvasInstance.GetComponent<RectTransform>());

                folder.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
                folder.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
                folder.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
                folder.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                folder.GetComponent<RectTransform>().localScale = Vector3.one;
            }

            folders.Add(folder);

            i++;
        }

        return Instantiate(
            original: prefab,
            parent: folders[hierarchy].GetComponent<RectTransform>()
        );
    }
}
