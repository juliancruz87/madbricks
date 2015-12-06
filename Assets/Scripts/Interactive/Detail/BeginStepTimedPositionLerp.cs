using Assets.Scripts.Util;
using UnityEngine;

namespace Interactive.Detail {
    public class BeginStepTimedPositionLerp : BeginStepGameBase {
        [SerializeField]
        private Transform lerpObject;
        [SerializeField]
        private Transform startPosition;
        [SerializeField]
        private Transform targetPosition;
        [SerializeField]
        private float lerpTime;

        private bool stoped;
        private float currentTime;
        private AlphaLerp timeAlphaLerp;

        private void Awake() {
            stoped = true;
            timeAlphaLerp = new AlphaLerp(0, lerpTime);
        }

        private void Update() {
            if (!stoped)
                Lerp();
        }

        public void StartLerp() {
            stoped = false;
            currentTime = 0;
            lerpObject.position = startPosition.position;
        }

        private void Lerp() {
            currentTime += Time.deltaTime;
            float alpha = timeAlphaLerp.GetAlpha(currentTime);
            if (alpha >= 1) {
                alpha = 1;
                stoped = true;
            }

            lerpObject.position = Vector3.Lerp(startPosition.position, targetPosition.position, alpha);

            if (stoped &&
                EndStep != null)
                EndStep();
        }

        public override void StartStep() {
            StartLerp();
        }

		public override void EndStep_Debug ()
		{
			base.EndStep_Debug ();
			lerpObject.position = targetPosition.position;
		}
    }
}