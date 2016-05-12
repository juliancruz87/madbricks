using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace UI
{
	public class UIButtonRestartLevel : MonoBehaviour 
	{
		public void OnClickEvent () {
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		}
	}
}