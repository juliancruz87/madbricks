using UnityEngine;
using System.Collections;
using Interactive;

public class GameManagerStateDummy : MonoBehaviour , IGameManagerForStates
{
	public event System.Action StartedGame;

	public GameStates CurrentState 
	{
		get { return GameStates.Planning; }
	}

	public void Play ()
	{

	}

	public System.Collections.Generic.List<ITotem> Totems {
		get;
		set;
	}

	public void Goal ()
	{

	}

	public void Lose ()
	{

	}
}
