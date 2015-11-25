using System;
using UnityEngine;
using System.Collections;

namespace Interactive.Detail
{
	public class BeginStepWithAnimation : BeginStepGameBase
	{
		[SerializeField]
		private Animator animator;
		[SerializeField]
		private AnimationClip clip;
		[SerializeField]
		private string animationName = "Start";

		public override void StartStep ()
		{
			animator.SetTrigger (animationName);
			StartCoroutine (OnEndStepWithTimer (clip.length));
		}
	}	
}