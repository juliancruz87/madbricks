using UnityEngine;
using System.Collections;
using ManagerInput.Detail;

namespace ManagerInput.Detail
{
	public abstract class InputDeviceBase : IInputDeviceTouchData
	{
		protected TouchInfo[] touches = { new TouchInfo (), new TouchInfo () };
		
		public ITouchInfo PrimaryTouch
		{
			get { return touches [0]; }
		}
		
		public ITouchInfo SecondaryTouch
		{
			get { return touches [1]; }
		}
		
		public ITouchInfo[] Touches
		{
			get { return touches; }
		}

		public int TouchCount
		{
			get
			{
				int count = 0;
				foreach (TouchInfo touch in touches)
				{
					if (touch.IsTouching)
						count++;
				}
				return count;
			}
		}
		
		public bool IsDoubleTouching
		{
			get
			{
				return (PrimaryTouch.IsTouching && PrimaryTouch.WasTouchingLastFrame && 
					SecondaryTouch.IsTouching && SecondaryTouch.WasTouchingLastFrame);
			}
		}
		
		public float PinchDeltaThisFrame
		{
			get
			{
				float prevDist = Vector3.Distance (PrimaryTouch.PreviousTouchPosition, SecondaryTouch.PreviousTouchPosition);
				float currentDist = Vector3.Distance (PrimaryTouch.TouchPosition, SecondaryTouch.TouchPosition);
				return currentDist - prevDist;
			}
		}
		
		public float NormalizedPinchDeltaThisFrame
		{
			get { return PinchDeltaThisFrame / Screen.width; }
		}

		public float DoubleTouchRotationDeltaThisFrame
		{
			get
			{
				Vector3 prevDir = PrimaryTouch.PreviousTouchPosition - SecondaryTouch.PreviousTouchPosition;
				Vector3 dir = PrimaryTouch.TouchPosition - SecondaryTouch.TouchPosition;
				return MathHelpers.GetAngleBetweenVectorsXYPlane (prevDir, dir);
			}
		}
		
		public static InputDeviceBase CreateInstance ()
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
				return new InputDeviceTouchScreen ();
			else
				return new InputDeviceMouse ();		
		}

		public abstract void Read ();

		protected void PreRead ()
		{
			foreach (TouchInfo touch in touches)
				touch.PreRead ();
		}
			
		protected void PostRead ()
		{
			foreach (TouchInfo touch in touches)
				touch.PostRead ();
		}
	}
}
