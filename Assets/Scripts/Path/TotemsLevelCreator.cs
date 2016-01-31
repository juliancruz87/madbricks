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

		private void Start ()
		{
			SetUp ();
		}

		public void SetUp ()
		{
			Grid gridConfig = grid.GetComponent<Grid> ();
			if(gridConfig != null)
				gridConfig.Create ();

			Node [] childs = GetComponentsInChildren<Node> ();
			System.Array.ForEach (childs, c => points.Add (c));
			instantiator.Instantiate (points, grid, grid);
			GameManagerForStates.Totems = instantiator.Totems;
		}
	}
}