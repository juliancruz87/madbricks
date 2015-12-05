using UnityEngine;
using System.Collections;

namespace UI
{
	public class UIButtonRestartLevel : MonoBehaviour 
	{
		public void OnClickEvent () {
			Application.LoadLevel (Application.loadedLevelName);
		}
	}
}