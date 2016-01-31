using UnityEngine;
using System.Collections;
using ManagerInput;
using Interactive;

public class Handler : MonoBehaviour 
{
	[SerializeField]
	private Collider myCollider;

	private IGameManagerForStates GameManagerForStates
	{
		get { return GameManager.Instance;}
	}

	private void Update () 
	{
		if ( GameManagerForStates.CurrentState == GameStates.Planning && 
		    InputManager.Instance.InputDevice.PrimaryTouch.ReleasedTapThisFrame &&
            TouchChecker.InputIsOverThisCollider(Camera.main, myCollider))
			GameManagerForStates.Play ();
	}
}