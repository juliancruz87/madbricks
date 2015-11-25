using UnityEngine;
using System.Collections;

namespace ManagerInput.Detail
{
	[System.Serializable]
	public class TouchInfo : ITouchInfo
	{	
		private const float PRE_DRAG_DURATION = 0.3f;
		private bool wasHoldCancelled;
		
		public bool IsTouching
		{
			get;
			set;
		}

		public Vector3 TouchPosition
		{
			get;
			set;
		}
		
		public int TapCount
		{
			get;
			set;
		}

		public bool WasTouchingLastFrame
		{
			get;
			private set;
		}

		public Vector3 PreviousTouchPosition
		{
			get;
			private set;
		}

		public Vector3 StartTouchPosition
		{
			get;
			private set;
		}
		
		public float StartTouchTime
		{
			get;
			private set;
		}

		public bool IsDragging
		{
			get;
			private set;
		}
		
		public bool WasDraggingLastFrame
		{
			get;
			private set;
		}

		private bool IsInPreDragDelay
		{
			get
			{
				return ((Time.time - StartTouchTime) < PRE_DRAG_DURATION);
			}
		}
		
		private float MinDragPixels
		{
			get { return Screen.width * 0.02f; }
		}

		public bool ReleasedTapThisFrame
		{
			get
			{
				if (ReleasedTouchThisFrame)
				{
					return (IsInPreDragDelay && Vector3.Distance (StartTouchPosition, PreviousTouchPosition) < MinDragPixels);
				}
				else
				{
					return false;
				}
			}
		}

		public bool IsHolding
		{
			get
			{
				return IsTouching && !wasHoldCancelled && !IsInPreDragDelay && Vector3.Magnitude (DragVector) < MinDragPixels;
			}
		}

		public bool StartedTouchThisFrame
		{ 
			get { return (IsTouching && !WasTouchingLastFrame); } 
		}

		public bool ReleasedTouchThisFrame
		{ 
			get { return (!IsTouching && WasTouchingLastFrame); } 
		}

		public bool StartedDragThisFrame
		{
			get
			{
				return IsDragging && !WasDraggingLastFrame;
			}
		}

		public bool ReleasedDragThisFrame
		{
			get
			{
				return !IsDragging && WasDraggingLastFrame;
			}
		}

		public Vector3 DragVector
		{
			get
			{
				if (IsTouching)
					return (TouchPosition - StartTouchPosition);
				else if (WasTouchingLastFrame)
					return (PreviousTouchPosition - StartTouchPosition);
				else
					return Vector3.zero;
			}
		}

		public Vector3 NormalizedDragDeltaThisFrame
		{
			get
			{
				Vector3 delta = DragDeltaThisFrame;
				return new Vector3 (delta.x / Screen.width, delta.y / Screen.height, delta.z);
			}
		}

		public Vector3 DragDeltaThisFrame
		{
			get
			{
				return (TouchPosition - PreviousTouchPosition);
			}
		}
		
		public TouchInfo ()
		{
			Reset ();
		}
		
		public void Reset ()
		{
			IsTouching = false;
			WasTouchingLastFrame = false;
			TouchPosition = new Vector3 ();
			PreviousTouchPosition = new Vector3 ();
			TapCount = 0;	
			StartTouchPosition = new Vector3 ();
			StartTouchTime = -1;
			IsDragging = false;
			WasDraggingLastFrame = false;
			wasHoldCancelled = false;
		}
		
		public void PreRead ()
		{
			PreviousTouchPosition = TouchPosition;
			WasTouchingLastFrame = IsTouching;
			WasDraggingLastFrame = IsDragging;
		}
		
		public void PostRead ()
		{
			if (StartedTouchThisFrame)
			{
				StartTouchTime = Time.time;
				StartTouchPosition = TouchPosition;
				wasHoldCancelled = false;
			}
			
			if (IsTouching)
			{
				if (Vector3.Magnitude (DragVector) > MinDragPixels)
				{
					IsDragging = true;
					wasHoldCancelled = true;
				}
				else
				{
					IsDragging = !IsInPreDragDelay;
				}
			}
			else
			{
				IsDragging = false;
			}
		}
	}
}
