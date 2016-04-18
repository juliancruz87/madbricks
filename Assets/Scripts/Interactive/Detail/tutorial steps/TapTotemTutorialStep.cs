using UnityEngine;
using Drag;
using Map;
using System.Collections.Generic;
using ManagerInput;

namespace Interactive.Detail
{
    public class TapTotemTutorialStep : TutorialStepBase {

        [SerializeField]
		private int activeTotemPosition;

        private Totem[] totems;

		private Collider totemCollider;

        protected override void BeginTutorialStep()
        {
            totems = FindObjectsOfType<Totem>();
            ConfigTotems(activeTotemPosition);
            ShowStartText();
        }

        private void Update()
        {
            if(totemCollider != null && TouchChecker.WasTappingFromCollider(Camera.main, totemCollider))
            {
                ShowIdleText();
                totemCollider = null;
            }
        }

		private void ConfigTotems (int totemPosition) 
		{
			foreach(Totem totem in totems)
			{
				if (totem.InitialPosition == totemPosition)
                    totemCollider = totem.GetComponentInChildren<Collider>();
			}
		}
    }
}