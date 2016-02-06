using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class MainMenuManager : MonoBehaviour {

    public Text textLevel;
    public bool isMenuActive;
    public Camera mainCamera;
    public bool unlockLevels;

    void Awake()
    {
        if (unlockLevels)
        {
            PlayerPrefs.SetString(PrefsProperties.CLEARED_AREA, "6");
            PlayerPrefs.SetString(PrefsProperties.CLEARED_LEVEL, "3");
        }
    }

	// Use this for initialization
	void Start () {
	
	}


    public void StartGame()
    {
        int level = 1;
        if(int.TryParse(textLevel.text, out level))
            Application.LoadLevel("Level " + level);
        else
            Application.LoadLevel("Level " + 1);
    }

    public void StartLevel(int level)
    {
        Application.LoadLevel("Level " + level);
    }

    public void SwitchMenuState()
    {
        isMenuActive = !isMenuActive;
    }

    public void NewGame()
    {
        PlayerPrefs.SetString(PrefsProperties.CLEARED_AREA, "1");
        PlayerPrefs.SetString(PrefsProperties.CLEARED_LEVEL, "1");

        //Application.LoadLevel(Application.loadedLevel);
        FindObjectOfType<AreaBoxesManager>().ResetAreaBoxes();
    }
}
