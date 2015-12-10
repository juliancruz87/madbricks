using Path;
using System.Collections.Generic;
using UnityEngine;

namespace Interactive.Detail
{
	public class TotemSingle : Totem
	{
		private Node nodeToArrives;
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
				Debug.LogWarning ("Wasn't found a path to follow");
			}
		}

		private List<Node> GetNodesToTravel ()
		{
			List<Node> nodes = PathBuilder.Instance.Finder.GetNodes (CurrentNode, totem.PositionToGo);
			if (nodes.Count <= 0)
			{
				Debug.LogWarning ("Try to find an alternative path");
				nodes = PathBuilder.Instance.Finder.GetNodesInLongDirection (CurrentNode, totem.PositionToGo);
			}

			return nodes;
		}

		protected override void GetReachedToPoint (Node node)
		{
			nodeToArrives = node;
			GoalReachedNode (node);
		}

#if UNITY_EDITOR
		private void Update ()
		{
			Vector3 position = myTransform.position;
			Debug.DrawLine(position + (Vector3.forward * 0.1F)  , position - (Vector3.forward * 0.1F), Color.blue);
			Debug.DrawLine(position + (Vector3.left * 0.1F) , position - (Vector3.left * 0.1F), Color.blue);
		}
#endif
	}
}