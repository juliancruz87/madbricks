using UnityEngine;
using System.Collections;

namespace ManagerInput.Detail
{
	public class InputDeviceMouse : InputDeviceBase
	{
		private const float dobleClickTimeout = 0.2f;
		
		public override void Read ()
		{
			PreRead ();
			
			for (int i=0; i<2; i++)
			{
				if (Input.GetMouseButton (i))
				{
					touches [i].TouchPosition = Input.mousePosition;
					touches [i].IsTouching = true;
				}
				else
				{
					touches [i].TouchPosition = new Vector3 ();
					touches [i].IsTouching = false;
				}
				
				if (Input.GetMouseButtonDown (i))
				{
					touches [i].TapCount++;
				}
				else if (touches [i].StartTouchTime + dobleClickTimeout < Time.time)
				{
					touches [i].TapCount = 0;
				}
			}

#if UNITY_EDITOR
			if (Input.GetKey (KeyCode.Space))
				SimulateDoubleTouch();
#endif

			PostRead ();
		}

		private void SimulateDoubleTouch()
		{
			touches [1].IsTouching = true;
			touches [1].TouchPosition = new Vector3(Screen.width/2, Screen.height/2, 0);
		}
	}

	public class InputDeviceTouchScreen : InputDeviceBase
	{
		public override void Read ()
		{
			PreRead ();
			
			int inputTouchesCount = 0;
			Touch[] inputTouches = Input.touches;
			
			for (int i=0; i<2; i++)
			{			
				bool addedTouch = false;
				
				if (inputTouches.Length > inputTouchesCount)
				{
					Touch touch = inputTouches [inputTouchesCount];
					if (touch.fingerId == i)
					{
						if (touch.phase != TouchPhase.Ended)
						{
							touches [i].IsTouching = true;
							touches [i].TouchPosition = new Vector3 (touch.position.x, touch.position.y, 0.0f);
							touches [i].TapCount = touch.tapCount;
							addedTouch = true;
						}
						inputTouchesCount++;
					}
				}
				
				if (!addedTouch)
				{
					touches [i].IsTouching = false;
					touches [i].TouchPosition = new Vector3 (0, 0, 0);
					if (!touches [i].WasTouchingLastFrame)
						touches [i].TapCount = 0;
				}
			}

			PostRead ();
		}
	}
}
