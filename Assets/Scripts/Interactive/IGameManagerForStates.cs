using UnityEngine;
using System.Collections;
using Interactive.Detail;
using System;

namespace Interactive
{
	public interface IGameManagerForUI
	{
		GameStates CurrentState { get; }
		GameResults Result { get; }
	}

	public interface IGameManagerForStates
	{
		event Action StartedGame;
		GameStates CurrentState { get; }
		void Play ();
		void Goal ();
		void Lose ();
	}
}