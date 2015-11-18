using UnityEngine;
using System.Collections;

namespace Graphics.Detail
{
	public class ColorAnimationForLight : MonoBehaviour, IGraphicsFX
	{
		[SerializeField]
		private bool isActive;
		[SerializeField]
		private float duration = 1.0F;
		[SerializeField]
		private Color color0 = Color.red;
		[SerializeField]
		private Color color1 = Color.blue;
		[SerializeField]
		private Light lt;

		private void Start()
		{
			lt = GetComponent<Light>();
		}
		private void Update() 
		{
			if (!isActive)
				return;
			float t = Mathf.PingPong(Time.time, duration) / duration;
			lt.color = Color.Lerp(color0, color1, t);
		}

		public void Play ()
		{
			isActive = true;
		}

		public void Stop ()
		{
			isActive = false;
		}
	}
}