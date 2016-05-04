using UnityEngine;
using DG.Tweening;
using CameraTools;

namespace Interactive.Detail
{
	public class ShakeCameraStep : BeginStepGameBase
	{
        [SerializeField]
        private Vector3 strenght = new Vector3(0.1f, 0.1f, 0.1f);

        [SerializeField]
        private int vibrato = 30;

        [SerializeField]
        private float duration = 0.5f;

        private CameraManager cameraManager;
        private Camera mainCamera;

        public override void StartStep()
        {
            mainCamera = Camera.main;
            cameraManager = mainCamera.gameObject.GetComponent<CameraManager>();
            cameraManager.enabled = false;
            mainCamera.DOShakePosition(duration, strenght, vibrato).OnComplete(OnShakeComplete);
		}

        private void OnShakeComplete()
        {
            cameraManager.enabled = true;
            EndStep();
        }
	}
}