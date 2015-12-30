using UnityEngine;
using System.Collections;
using Interactive.Detail;
using System;
using System.Collections.Generic;

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
		List<ITotem> Totems { get; set; }

		void Play ();
		void Goal ();
		void Lose ();
	}
}