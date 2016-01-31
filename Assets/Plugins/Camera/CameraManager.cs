using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace CameraTools
{
	public interface ICameraBehaviour 
	{
		bool IsEnabled { get; }
		void Enable(CameraParams cameraParams);
		void Disable(CameraParams cameraParams);
		void UpdateParamsWhenAdded(CameraParams cameraParams);
		void UpdateParams(CameraParams cameraParams);
	}

	[RequireComponent (typeof(Camera))]
	public class CameraManager : MonoBehaviour 
	{
		private List<ICameraBehaviour> behaviours = new List<ICameraBehaviour>();
		private CameraParams cameraParams;

		private Camera unityCamera;
		private Transform myTransform;

		private void Awake()
		{
			myTransform = transform;
			unityCamera = GetComponent<Camera>();
			cameraParams = new CameraParams(unityCamera);
		}

		public void AddBehaviour(ICameraBehaviour behaviour)
		{
			behaviours.Add (behaviour);
			behaviour.UpdateParamsWhenAdded(cameraParams);
			cameraParams.ApplyParamsToCamera (unityCamera, myTransform);
		}

		public void EnableBehaviour(ICameraBehaviour behaviour)
		{
			behaviour.Enable (cameraParams);
		}
		
		public void DisableBehaviour(ICameraBehaviour behaviour)
		{
			behaviour.Disable (cameraParams);
		}

		private void LateUpdate()
		{
			foreach (ICameraBehaviour behaviour in behaviours)
			{
				if (behaviour.IsEnabled)
					behaviour.UpdateParams(cameraParams);
			}
			cameraParams.ApplyParamsToCamera (unityCamera, myTransform);
		}

		public static Vector3 GetPointInFloorFromScreenPoint(Vector3 point)
		{
			return GetPointInYPlaneFromScreenPoint(point, 0);
		}
		
		public static Vector3 GetPointInYPlaneFromScreenPoint(Vector3 point, float planeYPos)
		{
			Ray ray = Camera.main.ScreenPointToRay(point);
			return GetPointInYPlaneFromCameraRay(ray, planeYPos);
		}

		public static Vector3 GetPointInYPlaneFromCameraRay(Ray ray, float planeYPos)
		{
			float length = Mathf.Abs((ray.origin.y - planeYPos) / ray.direction.y);
			return ray.origin + ray.direction * length;
		}

		public static Vector3 ConvertScreenDeltaIntoFloorDelta(Vector3 oldScreenPoint, Vector3 newScreenPoint)
		{
			Vector3 pointInFloor = GetPointInFloorFromScreenPoint(newScreenPoint);
			Vector3 lastPointInFloor = GetPointInFloorFromScreenPoint(oldScreenPoint);
			return pointInFloor - lastPointInFloor;			
		}
		
		public static Vector2 WorldToNormalizedScreenPoint(Vector3 worldPoint)
		{
			Vector2 screenPointInPixels = Camera.main.WorldToScreenPoint (worldPoint);
			return new Vector2(screenPointInPixels.x / Screen.width, screenPointInPixels.y / Screen.height);
		}
			
		public static bool SphereCastFromScreenPoint(Vector3 point, float radius, out RaycastHit hit)
		{
			Ray ray = Camera.main.ScreenPointToRay(point);
	    	return Physics.SphereCast(ray, radius, out hit, Mathf.Infinity);
		}

		public static bool SphereCastFromScreenPoint(Vector3 point, float radius, out RaycastHit hit, int layerMask)
		{
			Ray ray = Camera.main.ScreenPointToRay(point);
	    	return Physics.SphereCast(ray, radius, out hit, Mathf.Infinity, layerMask);
		}
		
		public static RaycastHit[] SphereCastAllFromScreenPoint(Vector3 point, float radius)
		{
			Ray ray = Camera.main.ScreenPointToRay(point);
			return Physics.SphereCastAll(ray, radius, Mathf.Infinity);
		}

		public static Collider GetTappedCollider(Vector3 screenTouchPosition, float radius, int layerMask)
		{
			RaycastHit hit;
			if (SphereCastFromScreenPoint (screenTouchPosition, radius, out hit, layerMask))
				return hit.collider;
			else
				return null;
		}
		
		public static Collider[] GetOrderedTappedColliders(Vector3 screenTouchPosition, float radius)
		{
			RaycastHit[] hits = SphereCastAllFromScreenPoint (screenTouchPosition, radius);
			Array.Sort (hits, (a, b) => a.distance.CompareTo (b.distance));
			Collider[] colliders = new Collider[hits.Length];
			for (int i = 0; i < hits.Length; i++)
				colliders.Clone [i] = hits [i].collider;
			return colliders;
		}

		public static T GetTappedComponent<T>(Vector3 screenTouchPosition, float radius, int layerMask) where T : Component
		{
			Collider collider = GetTappedCollider(screenTouchPosition, radius, layerMask);
			if (collider != null)
				return collider.GetComponent<T>();
			else
				return null;
		}
	
		public static T GetFirstTappedComponent<T>(Vector3 screenTouchPosition, float radius) where T : Component
		{
			Collider[] colliders = GetOrderedTappedColliders(screenTouchPosition, radius);
			Collider collider = Array.Find (colliders, c => c.GetComponent<T>() != null);

			if (collider != null)
				return collider.GetComponent<T>();
			else
				return null;
		}
	}
}