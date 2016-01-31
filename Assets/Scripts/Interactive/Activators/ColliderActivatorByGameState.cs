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

		private IGameManagerForStates GameManagerForStates
		{
			get { return GameManager.Instance;}
		}

		private bool ShouldActivate
		{
			get 
			{
				if(GameManagerForStates == null)
					return false;
				else 
					return GameManagerForStates.CurrentState == gameState;
			}
		}

		private void Update ()
		{
			colliderToActivate.enabled = ShouldActivate;
		}
	}
}