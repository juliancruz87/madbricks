using System.Collections.Generic;
using UnityEngine;

namespace Interactive.Detail
{
	public class WaitForEventTutorialStep : BeginStepGameBase 
	{
        [SerializeField]
        private BossColorController controller;

        private List<ITotem> totems;

        public override void StartStep()
        {
            totems = GameManager.Instance.Totems;
            SetActiveTotems(false);
            controller.AnimationCompleted += OnAnimationComplete;
        }

        private void OnAnimationComplete()
        {
            SetActiveTotems(true);
            EndStep();
        }


        private void SetActiveTotems(bool value)
        {
            foreach (ITotem totem in totems)
                SetToggleTotem(totem, value);
        }


        private void SetToggleTotem(ITotem totem, bool canBeDragged)
        {
            if (totem.DragObject != null)
                totem.DragObject.CanBeDragged = canBeDragged;
        }
    }
}