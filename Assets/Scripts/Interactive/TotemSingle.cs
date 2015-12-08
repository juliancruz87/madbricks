using Path;
using System.Collections.Generic;

namespace InteractiveObjects.Detail
{
	public class TotemSingle : Totem
	{
		protected override void Move ()
		{
			List<Node> nodes = GetNodesToTravel ();
			if (nodes.Count > 0)
				GoToNode (0, nodes);
		}

		private List<Node> GetNodesToTravel ()
		{
			List<Node> nodes = PathBuilder.Instance.Finder.GetNodes (CurrentNode, totem.PositionToGo);
			if( nodes.Count <= 0)
				nodes = PathBuilder.Instance.Finder.GetNodesInLongDirection (CurrentNode, totem.PositionToGo);
			return nodes;
		}
	}
}