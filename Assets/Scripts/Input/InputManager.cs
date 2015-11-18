using UnityEngine;
using System.Collections;
using ManagerInput.Detail;

namespace ManagerInput
{
	public class InputManager : MonoBehaviour 
	{
		private InputDeviceBase inputDevice;
		
		public static InputManager Instance 
		{
			get;
			private set;
		}
		
		public IInputDeviceTouchData InputDevice 
		{
			get { return inputDevice; }
		}

		private void Awake ()
		{
			if (Instance == null) 
			{
				Instance = this;
				inputDevice = InputDeviceBase.CreateInstance ();
			}
			else
			{
				Destroy (gameObject);
			}
		}
			
		private void OnDestroy ()
		{
			if (Instance == this)
				Instance = null;
		}
		
		protected virtual void Update ()
		{
			inputDevice.Read ();
		}
	}
}