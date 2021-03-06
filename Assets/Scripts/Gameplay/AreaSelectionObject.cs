﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AreaSelectionObject : MonoBehaviour {

    private string areaID;

    void Start()
    {
        int nameLength = transform.parent.name.Length;
        areaID = transform.parent.name.Substring(nameLength - 1, 1);
    }

    void OnMouseDown()
    {
        if (!FindObjectOfType<MainMenuManager>().isMenuActive)
        {
            Debug.Log("Selected Area: " + areaID);
            SaveManager.Instance.SetSelectedArea(areaID);
            SceneManager.LoadScene(SceneProperties.SCENE_LOADER_AREA);
        }
    }
}
