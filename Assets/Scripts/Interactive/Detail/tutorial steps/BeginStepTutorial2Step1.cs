using System;
using Assets.Scripts.Util;
using UnityEngine;
using Drag;
using Graphics;
using Map;
using System.Collections.Generic;

namespace Interactive.Detail
{
    public class BeginStepTutorial2Step1 : BeginStepGameBase {

        [SerializeField]
		private int activeTotemPosition;
		[SerializeField]
		private int finalTotemPosition;
		[SerializeField]
		private Texture launcherHighlightTexture;

		private Totem[] totems;

		private Totem activeTotem;
		private MapObject currentLauncher;
		private HighlightObject highlight;

        public override void StartStep() 
		{
			totems = FindObjectsOfType<Totem> ();

			ConfigLauncher ();
			ConfigTotems (activeTotemPosition);
			SetListeners (activeTotem);
			highlight = activeTotem.GetComponent<HighlightObject> ();
			highlight.ActivateHighlight();

        }

		private void ConfigLauncher()
		{
			List<MapObject> launchers = MapObject.GetMapObjectsOfType (MapObjectType.LauncherSticky, MapObjectType.LauncherNormal);
			currentLauncher = launchers.Find (mapObject => mapObject.ParentNode.Id == finalTotemPosition);

		}

		private void SetListeners(Totem totem)
		{
			DraggableObject draggabble = totem.GetComponent<DraggableObject> ();
			draggabble.OnObjectDragged += totemDragged;
			draggabble.OnSnap += totemReleased;	
		}

		private void StopListeners(Totem totem)
		{
			DraggableObject draggabble = totem.GetComponent<DraggableObject> ();
			draggabble.OnObjectDragged -= totemDragged;
			draggabble.OnSnap -= totemReleased;
		}

		private void totemDragged(Vector3 initialPosition, Vector3 candidatePosition)
		{
			highlight.DeactivateHighlight ();
			currentLauncher.ChangeTexture (launcherHighlightTexture);

			/*
			HighlightObject hl = currentLauncher.GetComponent<HighlightObject> ();

			if(hl != null)
				hl.ActivateHighlight();
				*/
		}

		private void totemReleased()
		{
			/*
			HighlightObject hl = currentLauncher.GetComponent<HighlightObject> ();

			if(hl != null)
				hl.DeactivateHighlight();
				*/

			currentLauncher.ResetTexture ();

			if (activeTotem.CurrentNode.Id == finalTotemPosition)
				StepCompleted ();

			else 
				highlight.ActivateHighlight ();
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

		private void FreeTotems()
		{
			foreach (Totem totem in totems) {
				SetToggleTotem (totem, true);
			}

		}

		private void StepCompleted()
		{
			highlight.DeactivateHighlight ();
			StopListeners (activeTotem);
			FreeTotems ();
			EndStep ();
		}

		private void SetToggleTotem(Totem totem, bool canBeDragged)
		{
			totem.GetComponent<DraggableObject>().CanBeDragged = canBeDragged;
		}

    }
}