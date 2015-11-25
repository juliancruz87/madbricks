using UnityEngine;

namespace ManagerInput
{
	public interface ITouchInfo
	{
		bool IsTouching { get; }
		Vector3 TouchPosition { get; }
		int TapCount { get; }
		bool WasTouchingLastFrame { get; }
		Vector3 PreviousTouchPosition { get; }
		Vector3 StartTouchPosition { get; }
		float StartTouchTime { get; }
		bool IsDragging { get; }
		bool WasDraggingLastFrame { get; }
		bool ReleasedTapThisFrame { get; }
		bool IsHolding { get; }
		bool StartedTouchThisFrame { get; }
		bool ReleasedTouchThisFrame { get; }
		bool StartedDragThisFrame { get; }
		bool ReleasedDragThisFrame { get; }
		Vector3 DragVector { get; }
		Vector3 NormalizedDragDeltaThisFrame { get; }
		Vector3 DragDeltaThisFrame { get; }
	}	
}
