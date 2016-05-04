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
            screen.AnimateGuardian();
            screen.GuardianAnimationCompleted += ShowScreen;
		}

        private void ShowScreen()
        {
            screen.ButtonClicked += CompleteStep;
            screen.AnimateWinScreen();
        }

        private void CompleteStep()
        {
            EndStep();
        }
	}
}