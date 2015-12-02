using UnityEngine;
using System.Collections;
using Interactive.Detail;

namespace Interactive
{
	public interface IGameManagerForStates
	{
		GameResults Result { get; }
		GameStates CurrentState { get;}
		void Play ();
		void Goal ();
		void Lose ();
	}
	
}