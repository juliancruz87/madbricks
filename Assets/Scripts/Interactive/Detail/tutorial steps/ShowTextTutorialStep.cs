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
            ShowStartText();
        }
			
    }
}