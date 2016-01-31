using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Path;

namespace Interactive.Detail
{
	public class TotemsLevelCreator : MonoBehaviour
	{
		[SerializeField]
		private Transform grid;

		[SerializeField]
		private TotemInstantiator instantiator;

		private IGameManagerForStates GameManagerForStates
		{
			get { return GameManager.Instance;}
		}

		private List<Node> points = new List<Node>();

		public void SetUp ()
		{
			Grid gridConfig = grid.GetComponent<Grid> ();
			if(gridConfig != null)
				gridConfig.Create ();

			Node [] childs = GetComponentsInChildren<Node> ();

			foreach (Node child in childs) 
				points.Add (child);

			instantiator.Instantiate (points, grid, grid);
			GameManagerForStates.Totems = instantiator.Totems;
		}
	}
}