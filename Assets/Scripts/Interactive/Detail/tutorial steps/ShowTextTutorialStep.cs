using System;
using Assets.Scripts.Util;
using UnityEngine;
using Drag;
using Graphics;
using Map;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Interactive.Detail
{
    public class ShowTextTutorialStep : TutorialStepBase {

        protected override void BeginTutorialStep()
        {
			GameManager.Instance.GameStateChanged += CheckState;
            ShowStartText();
        }

		private void CheckState(GameStates gameState)
		{
			if (gameState == GameStates.Play)
				CompleteStep();
		}

		protected override void CompleteStep()
		{
			GameManager.Instance.GameStateChanged -= CheckState;
			base.CompleteStep();
		}	
    }
}