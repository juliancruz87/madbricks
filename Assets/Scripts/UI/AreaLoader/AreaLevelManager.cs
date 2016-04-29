using UnityEngine;
using LevelLoaderController;

public class AreaLevelManager : MonoBehaviour {

    [SerializeField] 
    private Material[] areaMaterials;

    [SerializeField]
    private Material defaultMaterial;

    void Start()
    {
        DisableLevels();
        EnableLevels(int.Parse(SaveManager.Instance.GetClearedLevel()));
    }

    /*void OnMouseDown()
    {
        Debug.Log("Selected Level: " + levelID);
        SaveManager.Instance.SetSelectedLevel(levelID);
        //Application.LoadLevel(SceneProperties.SCENE_LOADER_LEVEL);
        LevelLoaderController.LevelLoader.Instance.LoadScene(GetSceneToLoad(SaveManager.Instance.GetSelectedArea(), levelID));
    }
    */
    public string GetSceneToLoad(string level)
    {
        string sceneName = "";

        int selectedArea = int.Parse(SaveManager.Instance.GetSelectedArea());
        int clearedArea = int.Parse(SaveManager.Instance.GetClearedArea());
        int clearedLevel = int.Parse(SaveManager.Instance.GetClearedLevel());
        int selectedLevel = int.Parse(level);

        bool condition1 = clearedArea > selectedArea;
        bool condition2 = clearedArea == selectedArea;
        bool condition3 = selectedLevel <= clearedLevel;

        if (condition1 || (condition2 && condition3))
            sceneName = LevelLoader.GetTutorialOrLevelName(selectedArea, selectedLevel);


        return sceneName;
    }

    public void EnableLevels(int levelAmount)
    {
        int selectedArea = int.Parse(SaveManager.Instance.GetSelectedArea());
        int clearedArea = int.Parse(SaveManager.Instance.GetClearedArea());

        if (clearedArea > selectedArea)
            levelAmount = 3;

        for (int i = 0; i < levelAmount; i++)
        {
            Transform level = transform.GetChild(i);
            level.GetComponent<Renderer>().material = areaMaterials[selectedArea - 1];
        }
    }

    public void DisableLevels()
    {
        //Debug.Log("Disable levels ");

        for (int i = 0; i < 3; i++)
        {
            Transform level = transform.GetChild(i);
            if (level.GetComponent<TextMesh>() == null)
            {
                level.GetComponent<Renderer>().material = defaultMaterial;
            }
        }
    }
}
