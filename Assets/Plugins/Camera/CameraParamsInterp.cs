using UnityEngine;
using System.Collections;
using CameraTools.Detail;
using ManagerInput.Detail;

namespace CameraTools
{
	public class CameraParamsInterp
	{
		private CameraParams startParams;
		private CameraParams endParams;
		private float duration;
		private float timeLeft;
		
		public bool HasFinished
		{
			get { return timeLeft < 0; }
		}
		
		private float InterpPct
		{
			get { return MathHelpers.GetEasedOutPct((duration - timeLeft) / duration, 2); }
		}
		
		public CameraParams CurrentParams
		{
			get 
			{
				if (HasFinished)
					return endParams;
				else
					return CameraParams.Lerp (startParams, endParams, InterpPct); 
			}
		}

		public CameraParamsInterp(CameraParams startParams, CameraParams endParams, float duration)
		{
			SetParams(startParams, endParams, duration);
		}

		public void SetParams(CameraParams startParams, CameraParams endParams, float duration)
		{
			this.startParams = startParams;
			this.endParams = endParams;
			this.duration = duration;
			timeLeft = duration;
		}

		public void Update(float deltaTime)
		{
			timeLeft -= deltaTime;
		}
	}
}