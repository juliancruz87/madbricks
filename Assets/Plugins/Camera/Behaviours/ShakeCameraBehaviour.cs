using UnityEngine;

namespace CameraTools
{
	[System.Serializable]
	public class ShakeCameraBehaviour : ICameraBehaviour
	{
		[SerializeField]
		private float amplitude = 1.0f;
		
		[SerializeField]
		private float speed = 10.0f;
		
		private Vector3 initialPosition;

		public float DesiredStopTime
		{
			get;
			set;
		}

		public bool IsEnabled
		{ 
			get; 
			private set; 
		}
		
		public void Enable(CameraParams cameraParams)
		{
			if (!IsEnabled)
			{
				SetInitialPosition(cameraParams.position);
				IsEnabled = true;
			}
		}
		
		public void Disable(CameraParams cameraParams)
		{
			if (IsEnabled)
			{
				cameraParams.position = initialPosition;
				IsEnabled = false;
			}
		}

		public void UpdateParamsWhenAdded(CameraParams cameraParams)
		{
		}

		public void UpdateParams (CameraParams cameraParams)
		{
			Vector3 shakeDisplacement = CalculateDisplacementForThisFrame();
			cameraParams.position += shakeDisplacement;
		}

		private Vector3 CalculateDisplacementForThisFrame()
		{
			Vector3 displacement = new Vector3 ();
			displacement.x = GetNoise (0F, Time.time * speed);
			displacement.y = GetNoise (Time.time * speed, 0F);
			displacement.z = GetNoise (Time.time * speed, Time.time * speed);
			return displacement;
		}
		
		private float GetNoise (float a, float b)
		{
			const float BIAS_CORRECTION = 0.05f;
			float perlinValue = Mathf.PerlinNoise (a, b) * 2.0f - 1.0f + BIAS_CORRECTION;
			return amplitude * perlinValue;
		}

		public void SetInitialPosition(Vector3 position)
		{
			initialPosition = position;
		}
	}
}
