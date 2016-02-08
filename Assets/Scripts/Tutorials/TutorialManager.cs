using UnityEngine;
using System.Collections;
using System;

public class TutorialManager : MonoBehaviour
{

    [SerializeField]
    private TutorialSequence tutorialSequence;
    [SerializeField]
    private TutorialContainer tutorialContainer;
    private string tutorialName;
    public GameObject tutorialGO;

    public event Action FinishTutorial;

    [SerializeField]
    private bool isUsingVideoTutorials = true;

    public void Initialize()
    {
        tutorialContainer = Instantiate(Resources.Load<TutorialContainer>("Prefabs/Tutorial/TutorialVideoManager"));
        tutorialGO = FindObjectOfType<TutorialContainer>().gameObject;
        tutorialGO.SetActive(false);
        Debug.Log("[Tutorial] " + tutorialContainer.name);

        tutorialContainer.CloseTutorial += CloseTutorial;
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.T))
        {
            if (CheckForTutorialInLevel(1, 1))
            {
                ShowTutorial();
            }
        }*/
    }

    public void ShowTutorial()
    {
        tutorialGO.SetActive(true);
        Debug.Log("[Tutorial] ShowTutorial");
        
        tutorialContainer.Initialize(isUsingVideoTutorials);

        //tutorialContainer.Open(); //Bug accesing reference
    }

    public bool CheckForTutorialInLevel(int area, int level)
    {
        TutorialSequence[] sequences = Resources.LoadAll<TutorialSequence>("Tutorials");
        tutorialName = "Tutorial_T" + area + "_" + level;
        foreach (TutorialSequence sequence in sequences)
        {
            if (sequence.name.Equals(tutorialName))
            {
                Debug.Log("Level has tutorial");
                if (!isUsingVideoTutorials)
                {
                    Debug.Log("Show tutorial");
                    tutorialSequence = sequence;
                    tutorialContainer.pages = tutorialSequence.tutorialPages;
                    Debug.Log("[Tutorial] " + tutorialName + " exists!");
                    return true;
                }
                else
                {
                    if (sequence.movie != null)
                    {
                        Debug.Log("Show tutorial video");
                        tutorialContainer.SetMovie(sequence.movie);
                        return true;
                    }
                }
            }
        }
        Debug.Log("Dont show tutorial");
        return false;
    }

    public void CloseTutorial()
    {
        FinishTutorial();
    }

    public void DestroyTutorialContainer()
    {
        Destroy(tutorialContainer.gameObject);
        Resources.UnloadUnusedAssets();
    }
}
