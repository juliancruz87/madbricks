using UnityEngine;

namespace CameraTools
{
	[System.Serializable]
	public class PlayAnimCameraBehaviour : ICameraBehaviour
	{
		[SerializeField]
		private Animator proxyAnimator;

		[SerializeField]
		private float fov = 45;

		[SerializeField]
		private string idleStateName = "Idle";

		private Transform proxyAnimatorTransform;

		public bool IsEnabled 
		{
			get;
			private set;
		}

		public void Enable (CameraParams cameraParams)
		{
			IsEnabled = true;
			proxyAnimatorTransform = proxyAnimator.transform;
		}

		public void PlayAnimation(string stateName)
		{
			proxyAnimator.Play (stateName);
		}

		public void Disable (CameraParams cameraParams)
		{
			IsEnabled = false;
			proxyAnimator.Play (idleStateName);
		}

		public void UpdateParamsWhenAdded (CameraParams cameraParams)
		{
		}

		public void UpdateParams (CameraParams cameraParams)
		{
			cameraParams.fov = fov;
			cameraParams.position = proxyAnimatorTransform.position;
			cameraParams.rotation = proxyAnimatorTransform.rotation.eulerAngles;
		}
	}
}