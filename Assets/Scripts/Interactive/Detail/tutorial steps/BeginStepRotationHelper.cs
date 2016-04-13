using System;
using Assets.Scripts.Util;
using UnityEngine;
using Drag;
using Graphics;
using Map;
using System.Collections.Generic;

namespace Interactive.Detail
{
    public class BeginStepRotationHelper : BeginStepGameBase {

		[SerializeField]
		private GameObject rotateAnimation;

		[SerializeField]
		private Collider rotatorCollider;

		[SerializeField]
		private Transform world;

		private Collider musicalBoxCollider; 

  	    public override void StartStep() 
		{
			rotateAnimation.SetActive (true);
			rotatorCollider.enabled = true;
		}

		private void Update()
		{
			if (world.rotation.eulerAngles.y < 190 && world.rotation.eulerAngles.y > 170) 
			{
				rotateAnimation.SetActive (false);
				EndStep ();
			}
		}
    }
}