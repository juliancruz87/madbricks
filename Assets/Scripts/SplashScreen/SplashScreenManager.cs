using UnityEngine;
using System.Collections;

public class SplashScreenManager : MonoBehaviour {

    public GameObject foreground;

	void Start () 
    {
	    foreground.SetActive(false);
        PlayerPrefs.SetInt(PrefsProperties.IS_AUDIO_ENABLED, 1);
        PlayerPrefs.SetInt(PrefsProperties.IS_SFX_ENABLED, 1);

	}

	void Update () {
	
	}

    public void LoadFirstTutorial()
    {
        Application.LoadLevel(SceneProperties.SCENE_MAIN_MENU);
    }


}
