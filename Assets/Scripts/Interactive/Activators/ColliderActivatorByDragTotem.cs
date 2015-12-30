using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;

namespace Interactive.Activators
{
	public class ColliderActivatorByDragTotem : MonoBehaviour , SetterGameManagerForStates 
	{
		[Inject]
		private IGameManagerForStates gameManager;
		[SerializeField]
		private Collider colliderToActivate;
		private List<ITotem> totems;

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
					return CheckIfTotemIsDragged ();
			}
		}

		private bool CheckIfTotemIsDragged ()
		{
			if (totems == null && gameManager.Totems != null)
				totems = gameManager.Totems;

			if (totems != null)
				return !totems.Exists (c => c.IsDragged);

			return false;
		}
		
		private void Update ()
		{
			colliderToActivate.enabled = ShouldActivate;
		}
		
	}
}