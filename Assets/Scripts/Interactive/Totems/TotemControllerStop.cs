using UnityEngine;
using System;
using System.Collections.Generic;

namespace Interactive.Detail
{
	public class TotemControllerStop : MonoBehaviour
	{
		public event Action<GameObject> CollidedWithTotem;
		private Collider myCollider;
		private List<GameObject> forbbidenObjects = new List<GameObject> ();

		private void Start ()
		{
			myCollider =  GetComponent<Collider> ();
		}

		public void TurnOnColliderToDetect ()
		{
			myCollider.enabled = true;
		}

		public void SetTotems (List<GameObject> totemsCreated)
		{
			forbbidenObjects = totemsCreated.FindAll (c => c != gameObject);
		}

		private void  OnTriggerEnter(Collider collisionInfo)
		{
			if (forbbidenObjects.Contains(collisionInfo.gameObject)) 
			{
				if(CollidedWithTotem != null)
					CollidedWithTotem (collisionInfo.gameObject);
			}
		}
	}
}