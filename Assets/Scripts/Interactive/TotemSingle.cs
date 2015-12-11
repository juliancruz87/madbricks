using Path;
using System.Collections.Generic;
using UnityEngine;

namespace Interactive.Detail
{
	public class TotemSingle : Totem
	{
		protected override void Move ()
		{
			List<Node> nodes = GetNodesToTravel ();

			if (nodes.Count > 0) 
			{
				Node node = nodes [nodes.Count - 1];
				float speed = totem.SpeedPerTile * nodes.Count;
				GoToNode (node, speed);
			} 
			else 
			{
				Debug.LogWarning (gameObject.name + " wasn't found a path to follow");
			}
		}

		private List<Node> GetNodesToTravel ()
		{
			List<Node> nodes = PathBuilder.Instance.Finder.GetNodes (CurrentNode, totem.PositionToGo);
			if (nodes.Count <= 0)
			{
				Debug.LogWarning (gameObject.name +" try to find an alternative path");
				nodes = PathBuilder.Instance.Finder.GetNodesInLongDirection (CurrentNode, totem.PositionToGo);
			}

			return nodes;
		}

		protected override void GetReachedToPoint (Node node)
		{
			GoalReachedNode (node);
		}
	}
}