using System;
using UnityEngine;
using System.Collections;
using ManagerInput;

namespace Interactive.Detail
{
	public class BeginStepWithInputTouch : BeginStepGameBase 
	{
		[SerializeField]
		private Collider myCollider;

        public override void StartStep () {

		}

		private void Update ()
		{
			if (TouchChecker.WasTappingFromCollider (Camera.main, myCollider, false))
				EndStep ();
		}
	}
}