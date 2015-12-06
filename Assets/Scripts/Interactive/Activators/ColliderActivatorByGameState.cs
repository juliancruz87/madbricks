using UnityEngine;
using System.Collections;
using Zenject;

namespace Interactive.Activators
{
	public class ColliderActivatorByGameState : MonoBehaviour 
	{
		[SerializeField]
		private GameStates gameState;

		[SerializeField]
		private Collider colliderToActivate;

		[Inject]
		private IGameManagerForUI gameManager;

		private bool ShouldActivate
		{
			get 
			{
				if(gameManager == null)
					return false;
				else 
					return gameManager.CurrentState == gameState;
			}
		}

		private void Update ()
		{
			colliderToActivate.enabled = ShouldActivate;
		}
	}
}