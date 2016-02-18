using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class TutorialContainer : MonoBehaviour {

    public Image mainPage;
    public List<Sprite> pages;
    public int currenPageIndex;

    public GameObject panelPages;
    public GameObject panelVideo;

    public Button buttonTurnRight;
    public Button buttonTurnLeft;

    public event Action CloseTutorial;

    private MoviePlayer moviePlayer;

#if !UNITY_ANDROID
    public MovieTexture tutorialMovie;
#endif

    void Start () 
    {
        buttonTurnLeft.interactable = false;
        CloseTutorial += Close;
        // gameObject.GetComponent<Canvas>().enabled = false;
        //gameObject.SetActive(false);
	}
	
    public void Initialize(bool isUsingVideoTutorials)
    {
        if (isUsingVideoTutorials)
        {
            panelPages.SetActive(false);
            moviePlayer = panelVideo.GetComponentInChildren<MoviePlayer>();
#if !UNITY_ANDROID
            moviePlayer.SetMovie(tutorialMovie);
#endif
            panelVideo.SetActive(true);

        }
        else
        {
            panelPages.SetActive(true);
            panelVideo.SetActive(false);
            mainPage.sprite = pages[currenPageIndex];
        }
    }

	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.B))
            Open();
	}

    public void TurnPageLeft()
    {
        currenPageIndex--;
        if (currenPageIndex >= 0)
        {
            //buttonTurnLeft.interactable = true;
            mainPage.sprite = pages[currenPageIndex];
        }
        else
        {
            buttonTurnLeft.interactable = false;
        }

        if (currenPageIndex <= 0)
            buttonTurnLeft.interactable = false;

        if (currenPageIndex < pages.Count)
            buttonTurnRight.interactable = true;

        //Debug.Log("Current index: " + currenPageIndex);
    }

    public void TurnPageRight()
    {
        currenPageIndex++;
        if (currenPageIndex < pages.Count)
        {
            buttonTurnRight.interactable = true;
            buttonTurnLeft.interactable = true;
            mainPage.sprite = pages[currenPageIndex];
        }
        else
            buttonTurnRight.interactable = false;

        /* if (currenPageIndex >= pages.Count - 1)
             buttonTurnRight.interactable = false;*/

        if (currenPageIndex >= pages.Count)
            CloseTutorial();

        if (currenPageIndex >= 0)
            buttonTurnLeft.interactable = true;

        //Debug.Log("Current index: " + currenPageIndex);

    }

#if !UNITY_ANDROID
    public void SetMovie(MovieTexture movie)
    {
        tutorialMovie = movie;
    }
#endif

    public void CloseVideo()
    {
        CloseTutorial();
    }

    public void Close()
    {
        //Destroy(this);
        //gameObject.GetComponent<Canvas>().enabled = false;
        CloseTutorial -= Close;
        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.GetComponent<Canvas>().enabled = true;
        Debug.Log("[Tutorial] Open");
    }
}
