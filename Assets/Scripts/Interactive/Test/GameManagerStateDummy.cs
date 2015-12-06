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

	public void Goal ()
	{

	}

	public void Lose ()
	{

	}
}
