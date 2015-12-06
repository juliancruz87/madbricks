using UnityEngine;
using System.Collections;
using CameraTools.Detail;

namespace CameraTools
{
	[System.Serializable]
	public class PanToFollowTargetCameraBehaviour : ICameraBehaviour
	{
		[SerializeField]
		private float interpSpeed = 10;

		[SerializeField]
		private Vector3 offsetFromTarget;

		private Transform transformToFollow;
		private Vector3 levelLimitMin;
		private Vector3 levelLimitMax;

		public bool IsEnabled 
		{ 
			get; 
			private set;
		}
		
		public void Enable(CameraParams cameraParams)
		{
			IsEnabled = true;
		}
		
		public void Disable(CameraParams cameraParams)
		{
			IsEnabled = false;
		}

		private Vector3 GetCurrentOffsetFromTarget(CameraParams cameraParams)
		{
			Vector3 lookAt = transformToFollow.position + offsetFromTarget;			
			return lookAt - cameraParams.GetLookAtPointInYPlane(lookAt.y);
		}

		public void SetTargetToFollow(Transform transform)
		{
			transformToFollow = transform;
		}

		public void Initialize(Vector3 levelLimitMin, Vector3 levelLimitMax)
		{
			this.levelLimitMin = levelLimitMin;
			this.levelLimitMax = levelLimitMax;
		}
		
		public void UpdateParamsWhenAdded(CameraParams cameraParams)
		{
			Vector3 deltaMove = GetCurrentOffsetFromTarget(cameraParams);
			Move(deltaMove, ref cameraParams.position);
		}

		public void UpdateParams(CameraParams cameraParams)
		{
			float movePct = Mathf.Min (interpSpeed * Time.deltaTime, 1.0f);
			Vector3 deltaMove = GetCurrentOffsetFromTarget(cameraParams) * movePct;
			Move(deltaMove, ref cameraParams.position);
		}

		private void Move(Vector3 deltaMove, ref Vector3 position)
		{
			AABRectForOnScreenArea rect = new AABRectForOnScreenArea();
			rect.AdjustPanningDeltaMoveToFollowLimits (ref deltaMove, levelLimitMin, levelLimitMax);
			position += deltaMove;
		}
	}
}
