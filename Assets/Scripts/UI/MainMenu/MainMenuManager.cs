using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using UnityEngine.SceneManagement;

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

        Screen.sleepTimeout = SleepTimeout.SystemSetting;
    }

	// Use this for initialization
	void Start () {
	
	}


    public void StartGame()
    {
        int level = 1;
        if(int.TryParse(textLevel.text, out level))
            SceneManager.LoadScene("Level " + level);
        else
            SceneManager.LoadScene("Level " + 1);
    }

    public void StartLevel(int level)
    {
        SceneManager.LoadScene("Level " + level);
    }

    public void SwitchMenuState()
    {
        isMenuActive = !isMenuActive;
    }

    public void NewGame()
    {
        PlayerPrefs.SetString(PrefsProperties.CLEARED_AREA, "1");
        PlayerPrefs.SetString(PrefsProperties.CLEARED_LEVEL, "1");
        PlayerPrefs.SetString(PrefsProperties.CLEARED_TUTORIAL, "0");

        FindObjectOfType<AreaBoxesManager>().ResetAreaBoxes();
    }
}
