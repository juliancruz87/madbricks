using UnityEngine;
using ManagerInput;
using CameraTools.Detail;

namespace CameraTools
{
	[System.Serializable]
	public class PanneableCameraBehaviour : ICameraBehaviour
	{
		[SerializeField]
		private CameraSpringEffect springEffect;
		
		[SerializeField]
		private CameraPanningVelocity panningVelocity;
		
		private Transform levelLimitMin;
		private Transform levelLimitMax;
		private IInputDeviceTouchData inputDevice;
		
		public bool IsEnabled
		{ 
			get; 
			private set; 
		}
		
		public void Initialize(IInputDeviceTouchData inputDevice, Transform levelLimitMin, Transform levelLimitMax)
		{
			this.inputDevice = inputDevice;
			this.levelLimitMin = levelLimitMin;
			this.levelLimitMax = levelLimitMax;
		}
		
		public void Enable(CameraParams cameraParams)
		{
			IsEnabled = true;
		}
		
		public void Disable(CameraParams cameraParams)
		{
			IsEnabled = false;
		}
		
		public void UpdateParamsWhenAdded(CameraParams cameraParams)
		{
		}
		
		public void UpdateParams(CameraParams cameraParams)
		{
			Vector3 panDelta;
			
			if (IsDragging(out panDelta))
				UpdatePanningWhileDragging(panDelta, ref cameraParams.position);
			else
				UpdatePanningWhileNotDragging(ref cameraParams.position);
		}
		
		private void UpdatePanningWhileDragging(Vector3 panDelta, ref Vector3 position)
		{
			AABRectForOnScreenArea rect = new AABRectForOnScreenArea();
			rect.AdjustPanningDeltaMoveToFollowLimits(ref panDelta, levelLimitMin.position, levelLimitMax.position);
			panningVelocity.UpdateWhileDragging (panDelta);
			position += panDelta;
		}
		
		private void UpdatePanningWhileNotDragging(ref Vector3 position)
		{
			panningVelocity.UpdateWhileNotDragging ();
			
			Vector3 springEffectDelta;
			if (springEffect.Calculate(levelLimitMin.position, levelLimitMax.position, out springEffectDelta))
				position += springEffectDelta;
			else
				position -= panningVelocity.CurrentVelocity * Time.deltaTime;
		}
		
		private bool IsDragging(out Vector3 panDelta)
		{
			ITouchInfo touchInfo = inputDevice.PrimaryTouch; 
			if (touchInfo.IsDragging && !inputDevice.IsDoubleTouching)
			{
				panDelta = -CameraManager.ConvertScreenDeltaIntoFloorDelta(touchInfo.PreviousTouchPosition, touchInfo.TouchPosition);
				return true;
			}
			else
			{
				panDelta = Vector3.zero;
				return false;
			}
		}
	}
}