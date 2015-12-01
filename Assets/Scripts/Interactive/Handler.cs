using UnityEngine;
using System.Collections;
using ManagerInput;
using Interactive;

public class Handler : MonoBehaviour 
{
	[SerializeField]
	private Collider myCollider;

	private void Update ()
	{
		if (InputManager.Instance.InputDevice.PrimaryTouch.ReleasedTapThisFrame && TouchChecker.WasTappingFromCollider (Camera.main, myCollider))
			GameManagerAccess.GameManagerState.Play ();
	}
}