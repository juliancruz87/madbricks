using UnityEngine;
using System.Collections;
using Interactive.Detail;

namespace Interactive
{
	public class GameManager : MonoBehaviour , IGameManagerForStates
	{
		[SerializeField]
		private BeginGameManager startSequencer;

		public GameStates CurrentState 
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
			startSequencer.Init ();
		}

		private void StartPlanning ()
		{
			CurrentState = GameStates.Planning;
		}

		public void Play ()
		{
			Debug.Log ("Play");
			CurrentState = GameStates.Play;
		}
	}
}