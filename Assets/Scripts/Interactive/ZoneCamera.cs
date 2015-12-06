using UnityEngine;
using Zenject;
using CameraTools;
using ManagerInput;

namespace World.Detail
{
	[RequireComponent (typeof(CameraManager))]
	public class ZoneCamera : MonoBehaviour
	{
		[SerializeField]
		private ZoomableCameraBehaviour zoomBehaviour;
		
		private CameraManager cameraManager;
		private bool couldPanAndZoomLastFrame;
		
		private void Start ()
		{
			cameraManager = GetComponent<CameraManager> ();
			InitializeBehaviours ();
		}
		
		private void InitializeBehaviours ()
		{
			IInputDeviceTouchData inputDevice = InputManager.Instance.InputDevice;
			InitializeZooming (inputDevice);
		}

		private void InitializeZooming (IInputDeviceTouchData inputDevice)
		{
			zoomBehaviour.Initialize (inputDevice);
			cameraManager.AddBehaviour (zoomBehaviour);
		}
		
		private void Update ()
		{
			ToggleBehaviours ();
		}
		
		private void ToggleBehaviours ()
		{
			bool canPanAndZoom = InputManager.Instance.InputDevice.TouchCount == 2;
			if (couldPanAndZoomLastFrame != canPanAndZoom)
			{
				ToggleZooming (canPanAndZoom);
				couldPanAndZoomLastFrame = canPanAndZoom;
			}
		}
		
		private void ToggleZooming (bool shouldEnable)
		{
			if (shouldEnable)
				cameraManager.EnableBehaviour (zoomBehaviour);
			else
				cameraManager.DisableBehaviour (zoomBehaviour);
		}
	}
}