using UnityEngine;
using System.Collections;
using CameraTools.Detail;
using ManagerInput;

namespace CameraTools
{
	[System.Serializable]
	public class ZoomableCameraBehaviour : ICameraBehaviour
	{
		[SerializeField]
		private CameraZoomParams minZoomParams;

		[SerializeField]
		private CameraZoomParams maxZoomParams;

		[SerializeField, Range (0, 1)]
		private float initialZoomValue = 0.5f;   // a value between 0 and 1

		[SerializeField]
		private float pinchSensitivity = 6.0f;

		private float currentZoomValue;
		private IInputDeviceTouchData inputDevice;

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

		private CameraZoomParams CurrentZoomParams
		{
			get { return CameraZoomParams.Lerp (minZoomParams, maxZoomParams, currentZoomValue); }
		}
		
		public void Initialize(IInputDeviceTouchData inputDevice)
		{
			this.inputDevice = inputDevice;
			currentZoomValue = initialZoomValue;
		}

		public void UpdateParamsWhenAdded (CameraParams cameraParams)
		{
			ApplyCurrentZoom(cameraParams);
		}

		public void UpdateParams(CameraParams cameraParams)
		{
			float delta = CalculateDelta();

			if (delta > 0.0001f || delta < -0.0001f)
			{
				currentZoomValue = Mathf.Clamp01 (currentZoomValue + delta);
				ApplyCurrentZoom(cameraParams);
			}
		}

		private float CalculateDelta()
		{
			float delta = 0;

			delta += CalculateDeltaByPitch();
			delta += CalculateDeltaByKeyboard();
			delta += CalculateDeltaByMouse();

			return delta;
		}

		private float CalculateDeltaByPitch()
		{
			if (inputDevice.IsDoubleTouching)
				return -inputDevice.NormalizedPinchDeltaThisFrame * (0.1f + currentZoomValue) * pinchSensitivity;
			else
				return 0;
		}

		private float CalculateDeltaByKeyboard()
		{
			if (Input.GetKey (KeyCode.I))
				return -1.0f * Time.deltaTime;
			else if (Input.GetKey (KeyCode.O))
				return 1.0f * Time.deltaTime;
			else
				return 0;
		}

		private float CalculateDeltaByMouse()
		{
			return Input.GetAxis("Mouse ScrollWheel") * -0.2f;
		}

		private void ApplyCurrentZoom(CameraParams cameraParams)
		{
			CurrentZoomParams.ApplyTo(cameraParams);
		}
	}
}
