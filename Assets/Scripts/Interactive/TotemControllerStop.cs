using Drag;
using Path;
using UnityEngine;
using DG.Tweening;
using Interactive;
using Interactive.Detail;
using System.Collections;
using System.Collections.Generic;
using Sound;

namespace InteractiveObjects.Detail
{
	public class TotemControllerStop : MonoBehaviour
	{
		private Totem myTotem;

		private void Start ()
		{
			myTotem = GetComponent<Totem> ();
		}

		private void OnCollisionEnter(Collision collisionInfo)
		{
			Debug.Log ("collision");
			Totem totem =  collisionInfo.gameObject.GetComponent<Totem> ();
			if( totem != null)
			{
				totem.Stop ();
				myTotem.Stop ();
			}
		}
	}
	
}