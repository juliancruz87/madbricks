using UnityEngine;
using System.Collections;

namespace Interactive.Activators
{
	public class ColliderActivatorByGameState : MonoBehaviour 
	{
		[SerializeField]
		private GameStates gameState;

		[SerializeField]
		private Collider colliderToActivate;

		private GameManager GameManagerInstance
		{
			get { return GameManager.Instance;}
		}

		private void Start ()
		{
			ActivateCollider (GameManagerInstance.CurrentState);
			GameManagerInstance.GameStateChanged += ActivateCollider;
		}

		private void ActivateCollider (GameStates gameState)
		{
			colliderToActivate.enabled = this.gameState == gameState;
		}

		private void OnDestroy ()
		{
			GameManagerInstance.GameStateChanged -= ActivateCollider;
		}

	}
}