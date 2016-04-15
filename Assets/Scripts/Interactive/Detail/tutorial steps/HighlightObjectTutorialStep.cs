using System;
using Assets.Scripts.Util;
using UnityEngine;
using Drag;
using Graphics;
using Map;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using ManagerInput;

namespace Interactive.Detail
{
    public class HighlightObjectTutorialStep : TutorialStepBase {

		[SerializeField]
		private MapObjectType mapObjectType;

		private static ITouchInfo TouchInfo
		{
			get { return InputManager.Instance.InputDevice.PrimaryTouch; }
		}

		private ArrayList mapObjects;

        protected override void BeginTutorialStep()
		{
			mapObjects = MapObject.GetMapObjectsOfType (mapObjectType);
			HightlightObjects ();
			ShowStartText();
			StartCoroutine (WaitForInput());
        }

		private IEnumerator WaitForInput()
		{
			bool flag = true;

			while (flag) 
			{
				if (TouchInfo.ReleasedTapThisFrame)
					flag = false;
				
				yield return null;
			}

			CompleteStep ();
		}

		private void HightlightObjects()
		{
			HighlightObject hl;

			foreach (MapObject mapObject in mapObjects) 
			{
				hl = mapObject.GetComponent<HighlightObject> ();
				if (hl != null) {
					hl.ActivateHighlight ();
				}
			}
		}

		private void DeactiveHighlights()
		{
			HighlightObject hl;

			foreach (MapObject mapObject in mapObjects) 
			{
				hl = mapObject.GetComponent<HighlightObject> ();
				if (hl != null)
					hl.DeactivateHighlight ();
			}
		}

		protected override void CompleteStep ()
		{
			DeactiveHighlights ();
			base.CompleteStep ();
		}

    }
}