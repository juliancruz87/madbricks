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

        if (int.Parse(level) <= int.Parse(SaveManager.Instance.GetClearedLevel()))
        {
            sceneName = LevelLoader.GetTutorialOrLevelName(int.Parse(SaveManager.Instance.GetSelectedArea()), int.Parse(level));
        }
        return sceneName;
    }

    public void EnableLevels(int levelAmount)
    {
        int selectedArea = int.Parse(SaveManager.Instance.GetSelectedArea()) - 1;

        //Debug.Log("Enabling levels " + levelAmount);
        for (int i = 0; i < levelAmount; i++)
        {
            Transform level = transform.GetChild(i);
            // if (level.GetComponent<TextMesh>() == null)
            {
                //Debug.Log("Enabling level " + (i));
                level.GetComponent<Renderer>().material = areaMaterials[selectedArea];
            }
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
