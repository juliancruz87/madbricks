using Path;
using System.Collections.Generic;
using UnityEngine;

namespace Interactive.Detail
{
	public class SquareTotem : Totem
	{
		private int currentNode = 0;
		private FinderShorterPath finder = new FinderShorterPath ();
		private List<Node> nodes = new List<Node> ();
		public override TotemType Type 
		{
			get { return TotemType.Square; }
		}

		protected override void Move ()
		{
			nodes = GetNodesToTravel ();

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

		private List<Node> GetNodesToTravel ()
		{
			List<Node> nodes = Finder.GetNodes (CurrentNode, positionToGo, myTransform);
			if (nodes.Count <= 0)
			{
				Debug.LogWarning (gameObject.name +" try to find an alternative path");
                nodes = Finder.GetNodesInLongDirection(CurrentNode, positionToGo);
			}

			return nodes;
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
    }
}