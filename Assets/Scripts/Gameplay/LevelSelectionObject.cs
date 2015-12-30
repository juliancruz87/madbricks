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
        LevelLoaderController.LevelLoader.Instance.LoadScene(GetSceneToLoad(SaveManager.Instance.GetSelectedArea(), SaveManager.Instance.GetSelectedLevel()));



    }

    public string GetSceneToLoad(string area, string level)
    {
        string sceneName = "";

        switch (area)
        {
            case "1":
                sceneName = "level_0";
                switch (level)
                {
                    case "1":
                        sceneName += 1;
                        break;

                    case "2":
                        sceneName += 2;
                        break;

                    case "3":
                        sceneName += 3;
                        break;
                }
                break;


            default:
                break;
        }

        return sceneName;
    }
}
