using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;
using Path;

namespace Interactive.Detail
{
	public class TotemsLevelCreator : MonoBehaviour
	{
		[SerializeField]
		private Transform grid;

		[SerializeField]
		private TotemInstantiator instantiator;

		[Inject]
		private IGameManagerForStates gameStates;

		private List<Node> points = new List<Node>();

		[PostInject]
		private void PostInject ()
		{
			SetUp (gameStates);
		}

		public void SetUp (IGameManagerForStates gameStates)
		{
			Grid gridConfig = grid.GetComponent<Grid> ();
			if(gridConfig != null)
				gridConfig.Create ();
			instantiator.GameStates = gameStates;
			Node [] childs = GetComponentsInChildren<Node> ();
			System.Array.ForEach (childs, c => points.Add (c));
			instantiator.Instantiate (points, grid, grid);
			gameStates.Totems = instantiator.Totems;
		}
	}
}