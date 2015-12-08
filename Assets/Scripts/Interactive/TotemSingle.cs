using Path;
using System.Collections.Generic;

namespace Interactive.Detail
{
	public class TotemSingle : Totem
	{
		protected override void Move ()
		{
			List<Node> nodes = GetNodesToTravel ();

			if (nodes.Count > 0) 
			{
				Node node = nodes [nodes.Count-1];
				float speed = totem.SpeedPerTile * nodes.Count;
				GoToNode (node, speed);
			}
		}

		private List<Node> GetNodesToTravel ()
		{
			List<Node> nodes = PathBuilder.Instance.Finder.GetNodes (CurrentNode, totem.PositionToGo);
			if (nodes.Count <= 0) 
				nodes = PathBuilder.Instance.Finder.GetNodesInLongDirection (CurrentNode, totem.PositionToGo);

			return nodes;
		}

		protected override void GetReachedToPoint (Node node)
		{
			GoalReachedNode (node);
		}
	}
}