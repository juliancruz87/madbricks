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
            TouchChecker.InputIsOverThisCollider(Camera.main, myCollider))
			gameManager.Play ();
	}
}