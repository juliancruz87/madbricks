using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static void LoadLevel(int level)
    {
        PlayerPrefs.SetInt(PrefsProperties.LEVEL_TO_LOAD, level);
        Application.LoadLevel("LoadingScene");
    }

    public static void LoadLevel(string level)
    {
        PlayerPrefs.SetString(PrefsProperties.LEVEL_TO_LOAD, level);
        Application.LoadLevel("LoadingScene");
    }

}
