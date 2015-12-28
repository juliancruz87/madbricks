using UnityEngine;
using Interactive;

namespace Map 
{
	public class DoorSwitcher : MonoBehaviour 
	{
		[SerializeField]
		private Door doorToHandle;

		private void  OnTriggerEnter(Collider collision)
		{
			if (GameManager.Instance.CurrentState != GameStates.Play)
				return;

			ITotem totem = collision.gameObject.GetComponent<ITotem> ();

			if (totem != null)
				doorToHandle.ToggleState();
		}
	}
}