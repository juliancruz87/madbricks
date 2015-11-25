using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ManagerInput;

namespace Demo.Detail
{

	public class DemoInputManager : MonoBehaviour 
	{
		[SerializeField]
		private Text text;
		[SerializeField]
		private Collider myCollider;
		
		private ITouchInfo TouchInfo
		{
			get{ return InputManager.Instance.InputDevice.PrimaryTouch; }
		}
			
		private void Update () 
		{
			if (TouchInfo.IsTouching)
				text.text = "touching screen now!";

			if (TouchInfo.IsDragging)
				text.text = "dragging screen now!";

			if(TouchChecker.WasTappingFromCollider (Camera.main, myCollider))
				text.text = "you touched the totem in screen!";

			/*
			if (InputManager.Instance.InputDevice.TouchCount > 0)
				text.text = "Touch numbers!";

			if (InputManager.Instance.InputDeviceSecondaryTouch > 0)
				text.text = "you have two fingers in the screen!";

			 */
		}
	}
}