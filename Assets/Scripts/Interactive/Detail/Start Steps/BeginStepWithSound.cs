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

		[SerializeField]
		private AudioSource audioSource;

		public override void StartStep ()
		{
			if (audioSource != null)
				audioSource.Play ();
			else if (audioSource == null)
				SoundManager.Instance.Play (clip);
			else if (clip != null && audioSource != null)
				audioSource.PlayOneShot (clip);
			FinishStep (0F + delay);
		}

		protected virtual void FinishStep (float time)
		{
			StartCoroutine (OnEndStepWithTimer (time));
		}
	}
	
}