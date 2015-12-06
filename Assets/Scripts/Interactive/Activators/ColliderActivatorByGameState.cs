using UnityEngine;
using System.Collections;
using Zenject;

namespace Interactive.Activators
{
	public class ColliderActivatorByGameState : MonoBehaviour, SetterGameManagerForStates 
	{
		[SerializeField]
		private GameStates gameState;

		[SerializeField]
		private Collider colliderToActivate;

		[Inject]
		private IGameManagerForStates gameManager;

		public IGameManagerForStates GameManager 
		{
			set { gameManager = value; }
		}

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