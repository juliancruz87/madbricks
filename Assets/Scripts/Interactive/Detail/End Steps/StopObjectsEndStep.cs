using UnityEngine;
using DG.Tweening;
using CameraTools;

namespace Interactive.Detail
{
	public class StopObjectsEndStep : BeginStepGameBase
	{
        public override void StartStep()
        {
			FindObjectOfType<Handler> ().StopAnimation ();
			GameManager.Instance.Totems.ForEach (item => item.Stop ());

			EndStep ();
		}
			
	}
}