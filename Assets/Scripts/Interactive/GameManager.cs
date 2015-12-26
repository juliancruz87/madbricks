using UnityEngine;
using System.Collections;
using Interactive.Detail;
using InteractiveObjects;
using System;
using Map;

namespace Interactive
{
	public class GameManager : MonoBehaviour , IGameManagerForStates, IGameManagerForUI
	{
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

		public GameStates CurrentState 
		{
			get;
			private set;
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
		}

		private void Start ()
		{
			CurrentState = GameStates.Introduction;
			startSequencer.EndIntroduction += StartPlanning;
			startSequencer.Play ();
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
            Invoke("EndGame", maxPlayTime);
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
	}
}