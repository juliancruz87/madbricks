using UnityEngine;
using System.Collections;

namespace CameraTools
{
	[System.Serializable]
	public class StaticCameraBehaviour : ICameraBehaviour
	{
		[SerializeField]
		private CameraParams param;

		private CameraParams savedParams;

		public bool IsEnabled 
		{ 
			get; 
			private set;
		}

		public void SetParams(CameraParams param)
		{
			this.param = param;
		}

		public void Enable(CameraParams cameraParams)
		{
			if (!IsEnabled)
			{
				savedParams = new CameraParams(cameraParams);
				cameraParams.SetParams (param);
				IsEnabled = true;
			}
		}
		
		public void Disable(CameraParams cameraParams)
		{
			if (IsEnabled)
			{
				cameraParams.SetParams (savedParams);
				IsEnabled = false;
			}
		}

		public void UpdateParamsWhenAdded (CameraParams cameraParams)
		{
		}

		public void UpdateParams(CameraParams cameraParams)
		{
		}
	}
}
