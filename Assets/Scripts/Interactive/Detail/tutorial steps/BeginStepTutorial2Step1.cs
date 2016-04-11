using System;
using Assets.Scripts.Util;
using UnityEngine;
using Drag;
using Graphics;

namespace Interactive.Detail
{
    public class BeginStepTutorial2Step1 : BeginStepGameBase {

        [SerializeField]
		private int activeTotemPosition;
		[SerializeField]
		private int finalTotemPosition;

		private Totem[] totems;

		private Totem activeTotem;
		private HighlightObject highlight;

		private Renderer totemRenderer;

        public override void StartStep() 
		{
			totems = FindObjectsOfType<Totem> ();
			ConfigTotems (activeTotemPosition);
			highlight = activeTotem.GetComponent<HighlightObject> ();
			highlight.ActivateHighlight();
			SetListeners (activeTotem);
        }

		private void SetListeners(Totem totem)
		{
			DraggableObject draggabble = totem.GetComponent<DraggableObject> ();
			draggabble.OnObjectDragged += totemDragged;
			draggabble.OnSnap += totemReleased;
			draggabble.OnNodeUpdated += updatedNode;
			
		}

		private void updatedNode()
		{
			if (activeTotem.CurrentNode.Id == finalTotemPosition) 
			{
				highlight.DeactivateHighlight ();
			} 
		}

		private void totemDragged(Vector3 initialPosition, Vector3 candidatePosition)
		{
			highlight.DeactivateHighlight ();
		}

		private void totemReleased()
		{
			if (activeTotem.CurrentNode.Id == finalTotemPosition) 
			{
				highlight.DeactivateHighlight ();
			} 
			else 
			{
				highlight.ActivateHighlight ();
			}
		}

		private void ConfigTotems (int totemPosition) 
		{
			foreach(Totem totem in totems)
			{
				if (totem.InitialPosition == totemPosition) 
				{
					SetToggleTotem (totem, true);
					activeTotem = totem;
				}
				else
					SetToggleTotem (totem, false);
			}
		}

		private void SetToggleTotem(Totem totem, bool canBeDragged)
		{
			totem.GetComponent<DraggableObject>().CanBeDragged = canBeDragged;
		}

    }
}