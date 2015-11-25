using System;
using UnityEngine;
using System.Collections;
using Sound;

namespace Interactive.Detail
{
	public class BeginStepWithSound : BeginStepGameBase
	{
		[SerializeField]
		protected AudioClip clip;
		[SerializeField]
		protected float delay = 0F;
		
		public override void StartStep ()
		{
			SoundManager.Instance.Play (clip);
			FinishStep (0F + delay);
		}

		protected virtual void FinishStep (float time)
		{
			StartCoroutine (OnEndStepWithTimer (time));
		}
	}
	
}