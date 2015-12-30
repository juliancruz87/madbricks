﻿using UnityEngine;
using System.Collections;

public class GameManager2 : MonoBehaviour {

    private bool hasMistakeHappened;
    private int objectiveCount;


    public HudContainer hudContainer;
	// Use this for initialization

    void Awake()
    {

    }

	void Start () 
    {
        //scenePillars = FindObjectsOfType<Pillar>();
        FadeManager.Instance.OnFadeOut += RestartLevel;
	}

    void Update()
    {
    }

   

    private void WinGame()
    {
        Debug.Log("Win");

        GameObject.Find("BoxTop").GetComponent<Animator>().SetTrigger("moveUp");


    }

    private void GameOver()
    {

        Debug.Log("Game Over");
        FadeManager.Instance.FadeOut();
        hudContainer.HideButtons();
    }

    private void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void LoadMenu()
    {
        StartCoroutine(LoadMenuCoroutine());
    }

    public IEnumerator LoadMenuCoroutine()
    {
        FadeManager.Instance.FadeOut();
        hudContainer.HideButtons();
        yield return new WaitForSeconds(1.88f);
        Application.LoadLevel(0);
    }
}