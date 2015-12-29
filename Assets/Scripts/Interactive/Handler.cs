using UnityEngine;
using System.Collections;
using ManagerInput;
using Interactive;
using Zenject;

public class Handler : MonoBehaviour 
{
	[SerializeField]
	private Collider myCollider;

	[Inject]
	private IGameManagerForStates gameManager;

	private void Update () 
	{
		if ( gameManager.CurrentState == GameStates.Planning && 
		    InputManager.Instance.InputDevice.PrimaryTouch.ReleasedTapThisFrame &&
            TouchChecker.InputIsOverThisCollider(Camera.main, myCollider))
			gameManager.Play ();
	}
}