using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Path 
{
	public class Grid : MonoBehaviour 
	{
		private List<Transform> nodes = new List<Transform> ();

		public void Create ()
		{
			GetComponentsInChildren<Transform> (nodes);

			nodes = nodes.FindAll (c => c != transform);

			for (int i = 0; i < nodes.Count; i++) 
			{
				nodes [i].gameObject.AddComponent<Node> ();
				nodes [i].GetComponent<Node> ().SetUp (i+1);
			}
		}
	}
}