using UnityEngine;
using ManagerInput.Detail;

namespace ManagerInput
{
	public interface IInputDeviceTouchData
	{
		ITouchInfo PrimaryTouch { get; }
		ITouchInfo SecondaryTouch { get; }
		ITouchInfo[] Touches { get; }
		int TouchCount { get; }
		bool IsDoubleTouching { get; }
		float PinchDeltaThisFrame { get; }
		float NormalizedPinchDeltaThisFrame { get; }
		float DoubleTouchRotationDeltaThisFrame { get; }
	}
}
