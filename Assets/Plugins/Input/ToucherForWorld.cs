using UnityEngine;
using System.Collections;

namespace ManagerInput
{
	public abstract class ToucherForWorld : MonoBehaviour 
	{
		private Collider myCollider;

		private void Awake ()
		{
			myCollider = GetComponent<Collider> ();
			if (myCollider == null)
				Debug.LogWarning ("GameObject ["+gameObject.name+"] needs a collider to be touched.");
		}

		protected virtual void Start ()
		{
		}

		private void Update()
		{
			if (TouchChecker.WasTappingFromCollider (Camera.main, myCollider, false))
				OnClickPressed ();
		}

		protected abstract void OnClickPressed ();
	}	
}