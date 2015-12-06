using UnityEngine;
using System.Collections;
using Interactive.Detail;
using InteractiveObjects;
using System;

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