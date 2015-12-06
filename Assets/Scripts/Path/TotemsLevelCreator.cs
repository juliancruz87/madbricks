using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;

namespace Interactive.Detail
{
	public class TotemsLevelCreator : MonoBehaviour
	{
		[SerializeField]
		private Transform grid;

		[SerializeField]
		private TotemInstantiator instantiator;

		private List<Transform> points = new List<Transform>();

		public void SetUp (IGameManagerForStates gameStates)
		{
			instantiator.GameStates = gameStates;
			Transform [] childs = GetComponentsInChildren<Transform> ();
			System.Array.ForEach (childs, c => points.Add (c));
			instantiator.Instantiate (points, grid, grid);
		}
	}
}