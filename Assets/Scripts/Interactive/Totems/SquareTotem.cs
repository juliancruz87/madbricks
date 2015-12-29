using Path;
using System.Collections.Generic;
using UnityEngine;

namespace Interactive.Detail
{
	public class SquareTotem : Totem
	{
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
			GoalReachedNode (node);
		}

		protected override void Update ()
		{
			base.Update ();
			if (nodes.Count > 0)
				finder.DrawShorterParthDebug ();
		}
    }
}