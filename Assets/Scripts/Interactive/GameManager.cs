using UnityEngine;
using System.Collections;
using Interactive.Detail;
using InteractiveObjects;
using System;
using Map;
using System.Collections.Generic;

namespace Interactive
{
    public class GameManager : MonoBehaviour , IGameManagerForStates, IGameManagerForUI
	{
        [System.Serializable]
        public class LevelInfo
        {
            public int area;
            public int level;
        }

        public LevelInfo levelInfo;

		public event Action StartedGame;

		[SerializeField]
		private SequencerManager startSequencer;

		[SerializeField]
		private SequencerManager endSequencer;

		[SerializeField]
		private int maxNumTotems;

	    [SerializeField] 
        private float maxPlayTime = 8f;

        private float totemTargetToleranceDistance = 0.2f;

		private bool wasEndGame;
		private int goals = 0;

        [SerializeField]
        private bool isGamePaused;

        private TutorialManager tutorialManager;

        [SerializeField]
        private bool areTutorialsActive;

        public GameStates CurrentState 
		{
			get;
			private set;
		}

		public List<ITotem> Totems 
		{
			set;
			get;
		}

		public GameResults Result 
		{
			get;
			private set;
		}

		public static GameManager Instance 
		{
			get;
			private set;
		}

		private void Awake ()
		{
			if (Instance == null)
				Instance = this;

            tutorialManager = gameObject.AddComponent<TutorialManager>();
            tutorialManager.Initialize();
        }

		private void Start ()
		{
            if (tutorialManager.CheckForTutorialInLevel(levelInfo.area, levelInfo.level))
            {
                tutorialManager.FinishTutorial += Pause;
                tutorialManager.FinishTutorial += StartGame;
                tutorialManager.ShowTutorial();
                Pause();
            }
            else
            {
                StartGame();
            }
            
		}

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.W))
            {
                PlayEndSequence(GameResults.Win);
            }
        }

        private void StartGame()
        {
            tutorialManager.FinishTutorial -= Pause;
            CurrentState = GameStates.Introduction;
            startSequencer.EndIntroduction += StartPlanning;
            startSequencer.Play();
            InitializeUI();
        }

		private void StartPlanning ()
		{
			CurrentState = GameStates.Planning;
		}

		public void Play ()
		{
			CurrentState = GameStates.Play;
			if (StartedGame != null)
				StartedGame ();

            //TODO: Fix this hack
            Invoke("ForceEndGame", maxPlayTime);
		}

        private void ForceEndGame() {
            if (!wasEndGame)
                EndGame();
        }

	    private void EndGame()
	    {
            wasEndGame = true;

	        if (IsEveryTotemOnPlace())
	        {
	            PlayEndSequence(GameResults.Win);
	        }
	        else
	        {
	            Lose();
	        }
	    }

	    private bool IsEveryTotemOnPlace() {
	        Debug.LogWarning("This is a hack that was made to force the end of the game, please fix it");

	        Totem[] totems = FindObjectsOfType<Totem>();
            ArrayList targets = MapObject.GetMapObjectsOfType(MapObjectType.Totem_target);
            
	        foreach (Totem totem in totems) {
	            bool isOverATarget = false;
	            foreach (MapObject target in targets) {
                    isOverATarget = Vector3.Distance(totem.transform.position, target.transform.position) < totemTargetToleranceDistance;

	                if (isOverATarget) {
                        Debug.Log("Totem " + totem.name + " is over " + target.name);
                        break;
	                }
	            }
                if (!isOverATarget)
                    return false;
	        }
            return true;
	    }

	    public void Goal ()
		{
			goals++;
			if(goals == maxNumTotems && !wasEndGame)
			{
				wasEndGame = true;
				PlayEndSequence (GameResults.Win);
			}
		}

		public void Lose ()
		{
			if(!wasEndGame)
				PlayEndSequence (GameResults.Lose);
			wasEndGame = true;
		}

		private void PlayEndSequence (GameResults results)
		{
			Result = results;
			endSequencer.Play ();
		}

        public void Pause ()
        {
            isGamePaused = !isGamePaused;
            if (isGamePaused)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }

        public void InitializeUI()
        {
            Debug.Log("[UI]Initializing UI..");
            Instantiate(Resources.Load("Prefabs/UI/CanvasGameUI"));
        }

        public void ReturnToLevelSelection()
        {
            LevelLoaderController.LevelLoader.Instance.LoadScene(SceneProperties.SCENE_LOADER_AREA);
        }

        public void RestartGame()
        {
            LevelLoaderController.LevelLoader.Instance.LoadScene(Application.loadedLevelName);
        }
    }
}