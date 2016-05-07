using System;
using UnityEngine;
using System.Collections;
using Sound;

namespace Interactive.Detail
{
	public class BeginStepWithSoundLatent : BeginStepWithSound
	{
		protected override void FinishStep (float time)
		{
			base.FinishStep (clip.length + delay);
		}
	}
}