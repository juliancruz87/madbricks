using UnityEngine;
using DG.Tweening;
using CameraTools;
using ManagerInput.CameraControls;

namespace Interactive.Detail
{
	public class StopObjectsEndStep : BeginStepGameBase
	{
        public override void StartStep()
        {
			FindObjectOfType<Handler> ().StopAnimation ();
            ParticleSystem particles = FindObjectOfType<ParticleSystem>();
            FindObjectOfType<GameUIManager>().gameObject.SetActive(false);
            FindObjectOfType<RotatorByAxes>().Collider.enabled = true;

            if (particles != null)
                particles.gameObject.SetActive(false);

            GameManager.Instance.Totems.ForEach (totem => StopTotems (totem));

			EndStep ();
		}

        private void StopTotems(ITotem totem)
        {
            totem.Stop();

            if (totem.DragObject != null)
                totem.DragObject.CanBeDragged = false;

        }
			
	}
}