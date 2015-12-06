using UnityEngine;
using System.Collections;

namespace CameraTools.Detail
{
	[System.Serializable]
	public class CameraZoomParams
	{
		[SerializeField]
		private float distToLookAtPoint = 10;

		[SerializeField]
		private float pitchAngle = 60;

		[SerializeField]
		private float fov = 45;
		
		public void ApplyTo (CameraParams cameraParams)
		{
			cameraParams.rotation.x = pitchAngle;
			Vector3 lookDir = cameraParams.Forward;
			cameraParams.position = cameraParams.LookAtFloorPoint + (-lookDir * distToLookAtPoint);
			cameraParams.fov = fov;
		}
		
		public static CameraZoomParams Lerp (CameraZoomParams min, CameraZoomParams max, float t)
		{
			CameraZoomParams result = new CameraZoomParams ();

			t = Mathf.Clamp (t, 0.001f, 1.0f);
			result.distToLookAtPoint = Mathf.Lerp (min.distToLookAtPoint, max.distToLookAtPoint, t);
			result.pitchAngle = Mathf.Lerp (min.pitchAngle, max.pitchAngle, t);
			result.fov = Mathf.Lerp (min.fov, max.fov, t);

			return result;
		}
	}
}
