using UnityEngine;
using System.Collections;
using System;


public class SaveManager : MonoBehaviour
{
    [SerializeField]
    private bool showLogger;
    private static SaveManager instance;

    public static SaveManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<SaveManager>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != instance)
                Destroy(this.gameObject);
        }
    }
    

    //Area
    public void SetSelectedArea(string selectedArea)
    {
        LogSetOperation("Area " + selectedArea.ToString());
        PlayerPrefs.SetString(PrefsProperties.SELECTED_AREA, selectedArea.ToString());
    }

    public string GetSelectedArea()
    {
        string selectedArea = PlayerPrefs.GetString(PrefsProperties.SELECTED_AREA, "1");
        LogGetOperation("Area " + selectedArea);
        return selectedArea;
    }

    //Level
    public void SetSelectedLevel(string selectedLevel)
    {
        LogSetOperation("Level " + selectedLevel.ToString());
        PlayerPrefs.SetString(PrefsProperties.SELECTED_LEVEL, selectedLevel.ToString());
    }

    public string GetSelectedLevel()
    {
        string selectedLevel = PlayerPrefs.GetString(PrefsProperties.SELECTED_LEVEL, "1");
        LogGetOperation("Level " + selectedLevel);
        return selectedLevel;
    }

    //Cleared Area
    public void SetClearedArea(string clearedArea)
    {
        LogSetOperation("Area " + clearedArea.ToString());
        PlayerPrefs.SetString(PrefsProperties.CLEARED_AREA, clearedArea.ToString());
    }

    public string GetClearedArea()
    {
        string clearedArea = PlayerPrefs.GetString(PrefsProperties.CLEARED_AREA, "1");
        LogGetOperation("Area " + clearedArea);
        return clearedArea;
    }

    //Cleared Level
    public void SetClearedLevel(string clearedLevel)
    {
        LogSetOperation("Level " + clearedLevel.ToString());
        PlayerPrefs.SetString(PrefsProperties.CLEARED_LEVEL, clearedLevel.ToString());
    }

    public string GetClearedLevel()
    {
        string clearedLevel = PlayerPrefs.GetString(PrefsProperties.CLEARED_LEVEL, "1");
        LogGetOperation("Level " + clearedLevel);
        return clearedLevel;
    }


    //Logging
    private void LogOperation(object info)
    {
        if (showLogger)
            Debug.Log("<b> Save Manager =></b>  " + info.ToString());
    }

    private void LogSetOperation(object info)
    {
        LogOperation("<color=lime>Setting " + info + "</color>");
    }

    private void LogGetOperation(object info)
    {
        LogOperation("<color=green>Getting " + info + "</color>");
    }


}
