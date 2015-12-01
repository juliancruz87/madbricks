using UnityEngine;
using System.Collections;
using Interactive.Detail;

namespace Interactive
{
	public interface IGameManagerForStates
	{
		GameStates CurrentState { get;}
		void Play ();
	}
	
}