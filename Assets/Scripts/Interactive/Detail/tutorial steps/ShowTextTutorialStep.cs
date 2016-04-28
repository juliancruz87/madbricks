using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UI;
using Graphics;

namespace Interactive.Detail
{
	public class ShowTextTutorialStep : BeginStepGameBase 
	{

		[SerializeField]
		private string text;

		[SerializeField]
		private GameObject objectToActivate;

		[SerializeField]
		private Text textField;


        private bool stepIsActive;

		private void Start()
		{
            stepIsActive = false;
			objectToActivate.SetActive (false);
		}

        public override void StartStep()
        {
			objectToActivate.SetActive (true);
			textField.text = text;
            stepIsActive = true;

        }

        private void Update()
        {
            if (stepIsActive && !objectToActivate.activeInHierarchy)
            {
                Debug.Log("Deactivated");
                stepIsActive = false;
                EndStep();
            }


        }
    }
}