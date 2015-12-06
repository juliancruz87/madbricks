using UnityEngine;
using System.Collections;

namespace CameraTools
{
	[System.Serializable]
	public class InterpolatedCameraBehaviour : ICameraBehaviour 
	{
		[SerializeField]
		private float interpDuration = 0.5f;

		private CameraParams sourceParams;
		private CameraParamsInterp interpolator;
				
		public bool IsEnabled 
		{ 
			get; 
			private set; 
		}

		public bool IsInterpolating
		{
			get { return IsEnabled && interpolator != null && !interpolator.HasFinished; }
		}

		public void Initialize(CameraParams cameraParams)
		{
			sourceParams = new CameraParams(cameraParams);
			interpolator = new CameraParamsInterp(sourceParams, new CameraParams(cameraParams), interpDuration);
		}

		public void Enable(CameraParams cameraParams)
		{
			sourceParams.SetParams(cameraParams);
			IsEnabled = true;
		}
		
		public void SetTarget(CameraParams targetParams)
		{
			interpolator.SetParams(sourceParams, targetParams, interpDuration);
		}
		
		public void Disable(CameraParams cameraParams)
		{
			if (IsEnabled)
				IsEnabled = false;
		}
		
		public void UpdateParamsWhenAdded (CameraParams cameraParams)
		{
		}
		
		public void UpdateParams(CameraParams cameraParams)
		{
			if (IsInterpolating)
			{
				interpolator.Update (Time.deltaTime);
				cameraParams.SetParams (interpolator.CurrentParams);
				sourceParams.SetParams (cameraParams);
			}
		}
	}
}
