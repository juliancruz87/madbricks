using UnityEngine;
using System.Collections;

public class AreaLevelManager : MonoBehaviour {

    [SerializeField] 
    private Material[] areaMaterials;

    void Start()
    {
        int selectedArea = int.Parse(SaveManager.Instance.GetSelectedArea()) - 1;

        foreach (Transform level in transform)
        {
            level.GetComponent<Renderer>().material = areaMaterials[selectedArea];
        }
    }

    /*void OnMouseDown()
    {
        Debug.Log("Selected Level: " + levelID);
        SaveManager.Instance.SetSelectedLevel(levelID);
        //Application.LoadLevel(SceneProperties.SCENE_LOADER_LEVEL);
        LevelLoaderController.LevelLoader.Instance.LoadScene(GetSceneToLoad(SaveManager.Instance.GetSelectedArea(), levelID));
    }
    */
    public string GetSceneToLoad(string area, string level)
    {
        string sceneName = "W" + area + "_L" + level;



        return sceneName;
    }
}
