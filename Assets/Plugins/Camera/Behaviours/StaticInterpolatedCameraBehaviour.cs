using UnityEngine;
using System.Collections;

namespace CameraTools
{
	[System.Serializable]
	public class StaticInterpolatedCameraBehaviour : ICameraBehaviour 
	{
		[SerializeField]
		private float interpDuration = 0.5f;

		[SerializeField]
		private CameraParams targetParams;

		private CameraParams sourceParams;
		private CameraParamsInterp interpolator;
		private bool shouldDisableAfterInterpolation;

		public bool IsEnabled 
		{ 
			get; 
			private set; 
		}
		
		public void Enable(CameraParams cameraParams)
		{
			if (!IsEnabled)
			{
				sourceParams = new CameraParams(cameraParams);
				interpolator = new CameraParamsInterp(sourceParams, targetParams, interpDuration);
				IsEnabled = true;
			}
		}
		
		public void Disable(CameraParams cameraParams)
		{
			if (IsEnabled)
			{
				cameraParams.SetParams (sourceParams);
				IsEnabled = false;
			}
		}

		public bool IsInterpolating
		{
			get { return interpolator != null && !interpolator.HasFinished; }
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

				if (interpolator.HasFinished)
				{
					interpolator = null;
					if (shouldDisableAfterInterpolation)
					{
						Disable(cameraParams);
						shouldDisableAfterInterpolation = false;
					}
				}
			}
		}

		public void InterpolateBackAndDisable()
		{
			interpolator = new CameraParamsInterp(targetParams, sourceParams, interpDuration);
			shouldDisableAfterInterpolation = true;
		}
	}
}
