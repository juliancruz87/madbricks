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
    [SerializeField]
    private Canvas tutorialContainerCanvas;
    public GameObject tutorialGO;

    public event Action FinishTutorial;

    public void Initialize()
    {
        tutorialContainer = Instantiate(Resources.Load<TutorialContainer>("Prefabs/Tutorial/TutorialManager"));
        tutorialContainerCanvas = tutorialContainer.GetComponent<Canvas>();
        tutorialGO = FindObjectOfType<TutorialContainer>().gameObject;
        tutorialGO.SetActive(false);
        Debug.Log("[Tutorial] " + tutorialContainer.name);

        tutorialContainer.CloseTutorial += CloseTutorial;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (CheckForTutorialInLevel(1, 1))
            {
                ShowTutorial();
            }
        }
    }

    public void ShowTutorial()
    {
        tutorialGO.SetActive(true);
        Debug.Log("[Tutorial] ShowTutorial");
        tutorialContainer.pages = tutorialSequence.tutorialPages;
        tutorialContainer.Initialize();

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
                tutorialSequence = sequence;
                Debug.Log("[Tutorial] " + tutorialName + " exists!");
                return true;
            }
        }
        return false;
    }

    public void CloseTutorial()
    {
        FinishTutorial();
    }
}
