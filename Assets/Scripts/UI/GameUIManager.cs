using UnityEngine;
using System.Collections;

public class GameUIManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Initialize()
    {

    }

    public void ReturnToLevelSelection()
    {
        LevelLoaderController.LevelLoader.Instance.LoadScene(SceneProperties.SCENE_LOADER_AREA);
    }

    public void RestartGame()
    {
        LevelLoaderController.LevelLoader.Instance.LoadScene(Application.loadedLevelName);
    }
}
