using UnityEngine;
using System.Collections;
using ManagerInput;
using Sound;
using Graphics;

namespace InteractiveObjects.Detail
{
	public class TappeableObject : ToucherForWorld
	{
		[SerializeField]
		private ConfigurationForPlaySound soundType;
		private IGraphicsFX [] graphicsFX;
		private bool wasTouched = false;

		protected override void Start ()
		{
			base.Start ();
			graphicsFX = GetComponentsInChildren<IGraphicsFX> (true);
		}

		protected override void OnClickPressed ()
		{
			SoundManager.Instance.Play (soundType);
			ManageGraphicsFX ();
			wasTouched = !wasTouched;
		}

		private void ManageGraphicsFX ()
		{
			if (wasTouched) 
			{
				foreach (IGraphicsFX graphic in graphicsFX)
					graphic.Stop ();
			} 
			else 
			{
				foreach (IGraphicsFX graphic in graphicsFX)
					graphic.Play ();
			}
		}
	}	
}