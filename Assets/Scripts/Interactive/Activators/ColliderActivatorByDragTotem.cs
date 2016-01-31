using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Interactive.Activators
{
	public class ColliderActivatorByDragTotem : MonoBehaviour
	{
		[SerializeField]
		private Collider colliderToActivate;

		private List<ITotem> totems;

		private IGameManagerForStates GameManagerForStates
		{
			get{ return GameManager.Instance; }
		}

		private bool ShouldActivate
		{
			get 
			{
				if(GameManagerForStates == null)
					return false;
				else 
					return CheckIfTotemIsDragged ();
			}
		}

		private bool CheckIfTotemIsDragged ()
		{
			if (totems == null && GameManagerForStates.Totems != null)
				totems = GameManagerForStates.Totems;

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