using UnityEngine;
using System.Collections;
using Path;
using System.Collections.Generic;
using Interactive.Detail;

namespace Path
{
	public class PossiblePath
	{
		private List<Node> nodes = new List<Node> ();

		public void AddNode (Node node)
		{
			nodes.Add (node);
		}
	}
	
}