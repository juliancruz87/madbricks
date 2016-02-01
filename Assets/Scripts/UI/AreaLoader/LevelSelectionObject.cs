using UnityEngine;
using System.Collections;

public class LevelSelectionObject : MonoBehaviour
{

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
        LoadScene("", "");
    }

    public void LoadScene(string area, string level)
    {
        string sceneName = transform.parent.GetComponent<AreaLevelManager>().GetSceneToLoad(levelID);
        if (!sceneName.Equals(""))
        {
            LevelLoaderController.LevelLoader.Instance.LoadScene(sceneName);

        }
    }
}
