using System;
using UnityEngine;
using System.Collections;
using Sound;

namespace Interactive.Detail
{
	public class WinScreenStepBoss : BeginStepGameBase
	{
        [SerializeField]
        private WinScreenBossController screen;

		public override void StartStep ()
		{
            screen.AnimateGuardian();
            screen.GuardianAnimationCompleted += FusionGuardians;
		}

        private void FusionGuardians()
        {
            screen.FusionGuardians();
            screen.FusionAnimationCompleted += ShowScreen;
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