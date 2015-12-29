using Path;
using System.Collections.Generic;
using UnityEngine;

namespace Interactive.Detail
{
	public class SphereTotem : Totem
	{
		private FinderShorterPath finder = new FinderShorterPath ();
		private int currentNode = 0;
		private List<Node> nodes = new List<Node>();

		public override TotemType Type 
		{
			get { return TotemType.Sphere; }
		}

		protected override void Move ()
		{
			//nodes = finder.FindShorterPathFromTo (CurrentNode.Id, totem.PositionToGo, Finder);
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
			if (node.Id == totem.PositionToGo) 
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
				EndGame ("Totem can reach the goal");
		}

		protected override void Update ()
		{
			base.Update ();
			if (nodes.Count > 0)
				finder.DrawShorterParthDebug ();
		}
	}
}