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
	public class TotemToNodeTutorialStep : BeginStepGameBase {

        [SerializeField]
		private int totemStartNodeID;
		[SerializeField]
		private int totemEndNodeID;

        private List<ITotem> totems;

		private ITotem activeTotem;

        public override void StartStep()
        {
			totems = GameManager.Instance.Totems;
			ConfigTotems(totemStartNodeID);
            SetListeners(activeTotem);
            activeTotem.SetHighlight(true); 
        }

        private void IdleStep()
        {
			activeTotem.SetHighlight(true);
        }
			

		private void SetListeners(ITotem totem)
		{
			totem.DragObject.OnObjectDragged += TotemDragged;
			totem.DragObject.OnSnap += TotemReleased;	
		}

		private void StopListeners(ITotem totem)
		{
			totem.DragObject.OnObjectDragged -= TotemDragged;
			totem.DragObject.OnSnap -= TotemReleased;
		}

		private void TotemDragged(Vector3 initialPosition, Vector3 candidatePosition)
		{
			activeTotem.SetHighlight(false);
        }

		private void TotemReleased()
		{
			if (activeTotem.CurrentNode.Id == totemEndNodeID)
				CompleteStep ();

			else 
				IdleStep();
		}

		private void ConfigTotems (int totemPosition) 
		{
			foreach(ITotem totem in totems)
			{
				if (totem.CurrentNode.Id == totemPosition) 
				{
					SetToggleTotem (totem, true);
					activeTotem = totem;
                }
				else
					SetToggleTotem (totem, false);
			}
		}

		private void FreeTotems()
		{
			foreach (ITotem totem in totems) {
				SetToggleTotem (totem, true);
			}
		}
			
		protected void CompleteStep()
        {
			activeTotem.SetHighlight(false);
			StopListeners (activeTotem);
			FreeTotems ();
			EndStep ();
        }

		private void SetToggleTotem(ITotem totem, bool canBeDragged)
		{
			totem.DragObject.CanBeDragged = canBeDragged;
		}
    }
}