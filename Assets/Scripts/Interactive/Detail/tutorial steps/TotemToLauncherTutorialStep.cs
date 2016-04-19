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
    public class TotemToLauncherTutorialStep : TutorialStepBase {

        [SerializeField]
		private int activeTotemPosition;
		[SerializeField]
		private int finalTotemPosition;
		[SerializeField]
		private Texture launcherHighlightTexture;
        [SerializeField]
        private LineHintPainter hintPainter;

        private Totem[] totems;

		private Totem activeTotem;
		private MapObject currentLauncher;
		private HighlightObject highlight;

        protected override void BeginTutorialStep()
        {
            totems = FindObjectsOfType<Totem>();
            ConfigTotems(activeTotemPosition);
            ConfigLauncher();
            SetListeners(activeTotem);
            highlight.ActivateHighlight();
            ShowStartText();
            
        }

        private void IdleStep()
        {
            highlight.ActivateHighlight();
            ShowIdleText();
        }

        private void ConfigLauncher()
		{
			List<MapObject> launchers = MapObject.GetMapObjectsOfType (MapObjectType.LauncherSticky, MapObjectType.LauncherNormal, MapObjectType.None);
			currentLauncher = launchers.Find (mapObject => mapObject.ParentNode.Id == finalTotemPosition);

		}

		private void SetListeners(Totem totem)
		{
			DraggableObject draggabble = totem.GetComponent<DraggableObject> ();
			draggabble.OnObjectDragged += TotemDragged;
			draggabble.OnSnap += TotemReleased;	
		}

		private void StopListeners(Totem totem)
		{
			DraggableObject draggabble = totem.GetComponent<DraggableObject> ();
			draggabble.OnObjectDragged -= TotemDragged;
			draggabble.OnSnap -= TotemReleased;
		}

		private void TotemDragged(Vector3 initialPosition, Vector3 candidatePosition)
		{
			highlight.DeactivateHighlight ();
			currentLauncher.ChangeTexture (launcherHighlightTexture);
            ShowActionText();
        }

		private void TotemReleased()
		{
			currentLauncher.ResetTexture ();

			if (activeTotem.CurrentNode.Id == finalTotemPosition)
				CompleteStep ();

			else 
				IdleStep();
		}

		private void ConfigTotems (int totemPosition) 
		{
			foreach(Totem totem in totems)
			{
				if (totem.InitialPosition == totemPosition) 
				{
					SetToggleTotem (totem, true);
					activeTotem = totem;
                    highlight = activeTotem.GetComponent<HighlightObject>();
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

        private void PaintHint ()
        {
            if(hintPainter != null)
                hintPainter.Paint(activeTotem.GetPathPositions());
        }

		protected override void CompleteStep()
        {
            PaintHint();
            highlight.DeactivateHighlight ();
			StopListeners (activeTotem);
			FreeTotems ();
            base.CompleteStep();
        }

		private void SetToggleTotem(Totem totem, bool canBeDragged)
		{
			totem.GetComponent<DraggableObject>().CanBeDragged = canBeDragged;
		}
    }
}