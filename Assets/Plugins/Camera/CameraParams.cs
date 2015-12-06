using UnityEngine;
using System.Collections;

namespace CameraTools
{
	[System.Serializable]
	public class CameraParams
	{
		public Vector3 position;
		public Vector3 rotation;
		public float fov;

		public Vector3 Forward
		{
			get { return Quaternion.Euler (rotation) * Vector3.forward; }
		}
		
		public Vector3 LookAtFloorPoint
		{
			get
			{
				return GetLookAtPointInYPlane(0);
			}
		}

		public CameraParams()
		{
		}
		
		public CameraParams(CameraParams param)
		{
			SetParams (param);
		}
		
		public CameraParams(Vector3 position, Vector3 rotation, float fov)
		{
			SetParams(position, rotation, fov);
		}
		
		public CameraParams(Camera cam)
		{
			position = cam.transform.position;
			rotation = cam.transform.rotation.eulerAngles;
			fov = cam.fieldOfView;
		}
		
		public void SetParams(CameraParams param)
		{
			SetParams (param.position, param.rotation, param.fov);
		}

		public void SetParams(Vector3 position, Vector3 rotation, float fov)
		{
			this.position = position;
			this.rotation = rotation;
			this.fov = fov;
		}
		
		public void ApplyParamsToCamera(Camera camera, Transform transform)
		{
			transform.position = position;
			transform.rotation = Quaternion.Euler (rotation);
			camera.fieldOfView = fov;
		}

		public Vector3 GetLookAtPointInYPlane(float yPlanePos)
		{
			Ray ray = new Ray(position, Forward);
			return CameraManager.GetPointInYPlaneFromCameraRay (ray, yPlanePos);
		}

		public static CameraParams Lerp(CameraParams a, CameraParams b, float interpPct)
		{
			CameraParams result = new CameraParams();
			
			result.position = Vector3.Lerp(a.position, b.position, interpPct);
			result.rotation = Quaternion.Lerp(Quaternion.Euler (a.rotation),  Quaternion.Euler (b.rotation), interpPct).eulerAngles;	
			result.fov = Mathf.Lerp (a.fov, b.fov, interpPct);
			
			return result;
		}
		
		public static CameraParams Slerp(CameraParams a, CameraParams b, float interpPct)
		{
			CameraParams result = new CameraParams();
			
			result.position = Vector3.Slerp(a.position, b.position, interpPct);
			result.rotation = Quaternion.Lerp(Quaternion.Euler (a.rotation),  Quaternion.Euler (b.rotation), interpPct).eulerAngles;	
			result.fov = Mathf.Lerp (a.fov, b.fov, interpPct);
			
			return result;
		}
	}
}
