using Path;
using System.Collections.Generic;

namespace InteractiveObjects.Detail
{
	public class TotemSingle : Totem
	{
		protected override void Move ()
		{
			List<Node> nodes =  PathBuilder.Instance.Finder.GetNodes (snaper.NodeSpnaped, totem.PositionToGo);
			if (nodes.Count > 0)
				GoToNode (0, nodes);
			else 
			{
				nodes =  PathBuilder.Instance.Finder.GetNodesInLongDirection (snaper.NodeSpnaped, totem.PositionToGo);
				if(nodes.Count > 0)
					GoToNode (0, nodes);
			}
		}
	}
}