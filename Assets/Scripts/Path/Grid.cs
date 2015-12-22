using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Path 
{
	public class Grid : MonoBehaviour 
	{
		private List<Node> nodes = new List<Node> ();

		public void Create ()
		{
			GetComponentsInChildren<Node> (nodes);

			for (int i = 0; i < nodes.Count; i++) 
				nodes [i].SetUp (i+1);
		}
	}
}