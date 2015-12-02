using UnityEngine;
using System.Collections;
using Interactive.Detail;
using InteractiveObjects;

namespace Interactive
{
	public class GameManager : MonoBehaviour , IGameManagerForStates
	{
		[SerializeField]
		private SequencerManager startSequencer;

		[SerializeField]
		private SequencerManager endSequencer;

		[SerializeField]
		private TotemInstantiator totemInstantiator;
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
		}

		public void Goal ()
		{
			goals++;
			if(goals == totemInstantiator.TotemsNum && !wasEndGame)
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