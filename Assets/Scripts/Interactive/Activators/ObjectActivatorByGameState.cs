using UnityEngine;
using System.Collections;

namespace Interactive.Activators
{
	public class ObjectActivatorByGameState : MonoBehaviour 
	{
		[SerializeField]
		private GameStates gameState;
		
		[SerializeField]
		private GameObject objectToActivate;
		
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
			objectToActivate.SetActive (ShouldActivate);
		}
	}
	
}