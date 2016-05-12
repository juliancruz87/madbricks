using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour {

    public GameObject foreground;

	void Start () 
    {
	    foreground.SetActive(false);
        PlayerPrefs.SetInt(PrefsProperties.IS_AUDIO_ENABLED, 1);
        PlayerPrefs.SetInt(PrefsProperties.IS_SFX_ENABLED, 1);

	}

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(SceneProperties.SCENE_MAIN_MENU);
    }
}
