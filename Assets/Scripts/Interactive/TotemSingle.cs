using UnityEngine;
using System.Collections;
using Path;
using System.Collections.Generic;
using Interactive.Detail;

namespace InteractiveObjects.Detail
{
	public class TotemSingle : MonoBehaviour 
	{
		private SnapItemToCloserPosition snaper;
		private TotemInstantiatorConfig totem;

		private void Awake ()
		{
			snaper = GetComponent<SnapItemToCloserPosition> ();
		}

		public void SetUp (TotemInstantiatorConfig totem)
		{
			this.totem = totem;
		}

		public void Play ()
		{
			//List<Connection> connections = PathBuilder.Instance.GetConnectionsByNode(snaper.NodeSpnaped);
			List<Connection> connections = PathBuilder.Instance.GetShortPath(snaper.NodeSpnaped, totem.PositionToGo);
		}
	}
}