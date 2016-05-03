using UnityEngine;
using System.Collections;
using Interactive.Detail;
using InteractiveObjects;
using System;
using Map;
using System.Collections.Generic;
using Sound;

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
		public event Action<GameStates> GameStateChanged;
        public event Action TotemsSet;

		[SerializeField]
		private SequencerManager startSequencer;

        [SerializeField]
        private SequencerManager tutorialSequencer;

        [SerializeField]
		private GameObject endSequencerPrefab;

		private SequencerManager endSequencer;

	    [SerializeField] 
        private float maxPlayTime = 8f;

        private float totemTargetToleranceDistance = 0.2f;
        private GameStates currentState;

		private bool wasEndGame;
		private int goals = 0;
        private List<ITotem> totems;

        [SerializeField]
        private bool isGamePaused;

        public GameStates CurrentState 
		{
			get { return currentState; }
			private set
            {
                currentState = value;
                OnGameStateChanged();
            }
		}

        public List<MapObject> Launchers
        {
            get;
            private set;
        }

		public List<ITotem> Totems 
		{
			set
            {
                totems = value;
                OnTotemsSet();
            }                

			get { return totems; }
		}

		public ITotem Boss
		{
			get
			{
				return Totems.Find(totem => totem.IsBoss);
            }
		}

		public GameResults Result 
		{
			get;
			private set;
		}

        public bool IsEveryTotemOnLauncher
        {
            get { return Totems.TrueForAll(totem => totem.IsInStartPoint); }
            
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
        }

		private void Start ()
		{
			endSequencer = Instantiate(endSequencerPrefab).GetComponent<SequencerManager>();

            Launchers = MapObject.GetMapObjectsOfType(MapObjectType.LauncherSticky, MapObjectType.LauncherNormal);
            StartGame();
		}

#if UNITY_EDITOR
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.W))
                PlayEndSequence(GameResults.Win);
        }
#endif

        private void StartGame()
        {
            CurrentState = GameStates.Introduction;
            startSequencer.SequenceEnded += StartPlanning;
            startSequencer.Play();
            InitializeUI();
        }

		private void StartPlanning ()
		{
			CurrentState = GameStates.Planning;

            if(tutorialSequencer != null)
                tutorialSequencer.Play();
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

			if (IsEveryTotemOnPlace() && IsBossJailed ())
	            PlayEndSequence(GameResults.Win);
	        else
	            Lose();
	    }

        


        private bool IsEveryTotemOnPlace() 
		{
	        Debug.LogWarning("This is a hack that was made to force the end of the game, please fix it");

	        Totem[] totems = FindObjectsOfType<Totem>();
            ArrayList targets = MapObject.GetMapObjectsOfType(MapObjectType.Totem_target);
            
	        foreach (Totem totem in totems) 
			{
	            bool isOverATarget = false;
	            foreach (MapObject target in targets) 
				{
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

			if(goals == Totems.Count && !wasEndGame && IsBossJailed ())
			{
				wasEndGame = true;
				PlayEndSequence (GameResults.Win);
			}
		}

		private bool IsBossJailed ()
		{
			ITotem boss = Boss;
			if (boss != null && !boss.IsJailed)
				return false;
			return true;
		}

        private void OnTotemsSet()
        {
            if (TotemsSet != null)
                TotemsSet();
        }

		public void Lose ()
		{
			if(!wasEndGame)
				PlayEndSequence (GameResults.Lose);
			wasEndGame = true;
		}

		private void PlayEndSequence (GameResults results)
		{
            if (results == GameResults.Win)
            {

            }
            else
            {

            }
			Result = results;
			endSequencer.Play ();
			SoundManager.Instance.PlayEndSound (results);
		}

        public void Pause ()
        {
            isGamePaused = !isGamePaused;
            if (isGamePaused)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }

		private void OnGameStateChanged()
		{
			if (GameStateChanged != null)
				GameStateChanged (CurrentState);	
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