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
            ParticleSystem particles = FindObjectOfType<ParticleSystem>();
            FindObjectOfType<GameUIManager>().gameObject.SetActive(false);

            if (particles != null)
                particles.gameObject.SetActive(false);

            GameManager.Instance.Totems.ForEach (item => item.Stop ());

			EndStep ();
		}
			
	}
}