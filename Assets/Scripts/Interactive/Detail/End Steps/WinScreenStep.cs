using System;
using UnityEngine;
using System.Collections;
using Sound;

namespace Interactive.Detail
{
	public class WinScreenStep : BeginStepGameBase
	{
		[SerializeField]
		private WinScreenController screen;

		public override void StartStep ()
		{
			screen.ButtonClicked += CompleteStep;
		}

		private void CompleteStep()
		{
			EndStep();
		}
	}
}