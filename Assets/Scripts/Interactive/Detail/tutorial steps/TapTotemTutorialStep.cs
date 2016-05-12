using UnityEngine;
using Drag;
using Map;
using System.Collections.Generic;
using ManagerInput;
using System.Collections;
using System;

namespace Interactive.Detail
{
    public class TapTotemTutorialStep : BeginStepGameBase {

        [SerializeField]
		private int totemPosition;

        private ITotem totem;

        private Transform totemTransform;

        private bool stepActive;


        public override void StartStep()
        {
            totem = GetTotem(totemPosition);
            totemTransform = totem.DragObject.gameObject.transform;
            stepActive = true;
        }

        private void Update ()
        {
            if (stepActive && totem.CurrentNode.Id == totemPosition)
                CheckTotemPosition();
        }

        private void CheckTotemPosition()
        {
            if (totemTransform.localEulerAngles.y < 271 && totemTransform.localEulerAngles.y > 269)
            {
                stepActive = false;
                EndStep();
            }
        }

        private ITotem GetTotem(int totemPosition)
        {
            List<ITotem> totems = GameManager.Instance.Totems;

            return totems.Find(totem => totem.CurrentNode.Id == totemPosition);
        }
    }
}