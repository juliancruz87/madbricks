using Path;
using System.Collections.Generic;
using UnityEngine;

namespace Interactive.Detail
{
	public class SphereTotem : Totem
	{
		private int currentNode = 0;
		private FinderShorterPath finder = new FinderShorterPath ();
		private List<Node> nodes = new List<Node>();

		public override TotemType Type 
		{
			get { return TotemType.Sphere; }
		}

		protected override void Move ()
		{
		    nodes = DijkstraPathFinder.FindShortestPath(CurrentNode, PathBuilder.Instance.GetNodeById(positionToGo));
			if (nodes.Count > 0) 
				ChoseNodeToGo ();
			else
				Debug.LogWarning (gameObject.name + " wasn't found a path to follow");
		}

		private void ChoseNodeToGo ()
		{
			Node node = nodes [currentNode];
			GoToNode (node, totem.SpeedPerTile);
		}
		
		protected override void GetReachedToPoint (Node node)
		{
			if (node.Id == positionToGo) 
			{
				currentNode = 0;
				GoalReachedNode (node);
			}
			else
				TryFindOtherPoint ();
		}

		private void TryFindOtherPoint ()
		{
			currentNode++;
			if (currentNode < nodes.Count)
				ChoseNodeToGo ();
			else
				EndGame ("Totem cannot reach the goal cant find " + positionToGo);
		}

		protected override void Update ()
		{
			base.Update ();
			if (nodes.Count > 0)
				finder.DrawShorterParthDebug ();
		}

		public override void GoToSecondaryPositionToGo ()
		{
			currentNode = 0;
			base.GoToSecondaryPositionToGo ();
		}
	}
}