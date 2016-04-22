using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UI;

namespace Interactive.Detail
{
	public class ShowPathTutorialStep : BeginStepGameBase 
	{
		[SerializeField]
		private GameObject pathPrefab;

		private List<GameObject> images = new List<GameObject> ();


        public override void StartStep()
        {

        }

		public void CloseText()
		{
			EndStep ();
		}

		private void ConfigImage(Image image, Sprite sprite)
		{
			image.sprite = sprite;
			image.preserveAspect = true;
			image.raycastTarget = false;
		}			

    }
}