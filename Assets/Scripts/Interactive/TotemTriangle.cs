using Path;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Interactive.Detail
{
	public class TotemTriangle : Totem
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
				Debug.LogWarning ("Wasn't found a path to follow");
			}
		}

		private List<Node> GetNodesToTravel ()
		{
			List<Node> nodes = new List<Node> ();
			nodes = PathBuilder.Instance.Finder.GetNodesInDirection (CurrentNode, totem.PositionToGo, Vector3.forward, nodes);
			return nodes;
		}

		protected override void GetReachedToPoint (Node node)
		{
			if (node.Id == totem.PositionToGo) 
				GoalReachedNode (node);
			else 
				GoToOtherPoint ();
		}

		private void GoToOtherPoint ()
		{
			Vector3 currentEulers = myTransform.rotation.eulerAngles;
			Vector3 turn90Dregrees = Vector3.up * 90;

			if (Random.Range (0, 100) < 50)
				myTransform.DORotate (currentEulers - turn90Dregrees, 0.3F);
			else
				myTransform.DORotate (currentEulers - turn90Dregrees, 0.3F);

			Move ();
		}
	}
}