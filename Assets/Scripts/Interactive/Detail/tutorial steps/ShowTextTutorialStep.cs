using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UI;

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

		private void Start()
		{
			objectToActivate.SetActive (false);
		}

        public override void StartStep()
        {
			objectToActivate.SetActive (true);
			textField.text = text;

        }

		public void CloseText()
		{
			objectToActivate.SetActive (false);
			textField.text = "";
			EndStep ();
		}
    }
}