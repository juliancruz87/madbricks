using System;
using UnityEngine;
using System.Collections;

namespace Interactive.Detail
{
	public abstract class BeginStepGameBase : MonoBehaviour 
	{
		public Action EndStep; 
		public abstract void StartStep ();

		protected IEnumerator OnEndStepWithTimer (float time)
		{
			yield return new WaitForSeconds (time);
			if(EndStep != null)
				EndStep ();
		}

		public virtual void EndStep_Debug ()
		{
			if(EndStep != null)
				EndStep ();
		}
	}
}