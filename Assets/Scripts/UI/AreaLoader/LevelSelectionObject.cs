using UnityEngine;
using System.Collections;

public class LevelSelectionObject : MonoBehaviour {

    private string levelID;

    void Start()
    {
        int nameLength = name.Length;
        levelID = name.Substring(nameLength - 1, 1);
    }

    void OnMouseDown()
    {
        Debug.Log("Selected Level: " + levelID);
        SaveManager.Instance.SetSelectedLevel(levelID);
        //Application.LoadLevel(SceneProperties.SCENE_LOADER_LEVEL);
        LevelLoaderController.LevelLoader.Instance.LoadScene(GetSceneToLoad(SaveManager.Instance.GetSelectedArea(), levelID));
    }

    public string GetSceneToLoad(string area, string level)
    {
        string sceneName = "W" + area + "_L" + level;

       

        return sceneName;
    }
}
