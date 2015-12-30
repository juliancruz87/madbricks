using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour {

    public Text labeLevellName;
    private Interactive.GameManager gameManager;

	void Start ()
    {
        Initialize();
	}
	

    public void Initialize()
    {
        gameManager = FindObjectOfType<Interactive.GameManager>();
        string levelNameLine = LevelNameParser.ParseNames()[gameManager.levelInfo.area-1];
        string levelName = levelNameLine.Split(',')[gameManager.levelInfo.level-1];
        labeLevellName.text = levelName;
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
