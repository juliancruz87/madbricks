using UnityEngine;
using DG.Tweening;
using CameraTools;

namespace Interactive.Detail
{
	public class MoveCameraStep : BeginStepGameBase
    {
        private const float toMove = 16;

        [SerializeField]
        private float duration = 5;
        [SerializeField]
        private Ease ease = Ease.Linear;

        private CameraManager cameraManager;
        private Camera mainCamera;

        public override void StartStep()
        {
            mainCamera = Camera.main;
            cameraManager = mainCamera.gameObject.GetComponent<CameraManager>();
            cameraManager.enabled = false;
            mainCamera.gameObject.transform.DOMoveY(toMove, duration).SetEase(ease);
            EndStep();
		}
	}
}