using UnityEngine;
using DG.Tweening;
using CameraTools;

namespace Interactive.Detail
{
	public class MoveCameraStep : BeginStepGameBase
    { 
        [SerializeField]
        private float duration = 5;

        private CameraManager cameraManager;
        private Camera mainCamera;

        public override void StartStep()
        {
            mainCamera = Camera.main;
            cameraManager = mainCamera.gameObject.GetComponent<CameraManager>();
            cameraManager.enabled = false;
            mainCamera.gameObject.transform.DOMoveY(14, duration);
            EndStep();
		}
	}
}