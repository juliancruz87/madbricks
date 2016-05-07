using System;
using UnityEngine;
using System.Collections;
using Sound;

namespace Interactive.Detail
{
	public class DelayStep : BeginStepGameBase
	{
		[SerializeField]
		private float delay;

		public override void StartStep ()
		{
			StartCoroutine (Delay ());
		}

		private IEnumerator Delay ()
		{
			yield return new WaitForSeconds (delay);
			EndStep ();
		}
	}
}