using UnityEngine;
using System.Collections;
using ManagerInput;

namespace CameraTools
{
	[System.Serializable]
	public class RotableCameraBehaviour : ICameraBehaviour
	{
		private IInputDeviceTouchData inputDevice;
		
		public bool IsEnabled
		{ 
			get; 
			private set; 
		}

		public void Initialize(IInputDeviceTouchData inputDevice)
		{
			this.inputDevice = inputDevice;
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
			float delta = CalculateDelta();
			
			if (delta > 0.0001f || delta < -0.0001f)
			{
				Vector3 lookAtFloorPoint = cameraParams.LookAtFloorPoint;
				float distToLookAtPoint = Vector3.Distance(cameraParams.position, lookAtFloorPoint);

				cameraParams.rotation.y += delta;
				cameraParams.position = lookAtFloorPoint + (-cameraParams.Forward * distToLookAtPoint);
			}
		}
		
		private float CalculateDelta()
		{
			float delta = 0;
			
			delta += CalculateDeltaByPinch();
			delta += CalculateDeltaByKeyboard();
			
			return delta;
		}
		
		private float CalculateDeltaByPinch()
		{
			if (inputDevice.IsDoubleTouching)
				return -inputDevice.DoubleTouchRotationDeltaThisFrame;
			else
				return 0;
		}
		
		private float CalculateDeltaByKeyboard()
		{
			if (Input.GetKey (KeyCode.K))
				return -30.0f * Time.deltaTime;
			else if (Input.GetKey (KeyCode.L))
				return 30.0f * Time.deltaTime;
			else
				return 0;
		}
	}
}