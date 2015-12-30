using UnityEngine;
using ManagerInput.Detail;
using System.Collections.Generic;
using Interactive;

namespace ManagerInput.CameraControls
{
	public class ConditionalDrag
	{
		private List<ITotem> totems;
		public IGameManagerForStates GameManager 
		{
			set;
			private get;
		}

		public bool Check ()
		{
			if(GameManager == null)
				return false;
			else 
				return CheckIfTotemIsDragged ();
		}

		private bool CheckIfTotemIsDragged ()
		{
			if (totems == null && GameManager.Totems != null)
				totems = GameManager.Totems;
			
			if (totems != null)
				return !totems.Exists (c => c.IsDragged);
			
			return false;
		}
	}
}