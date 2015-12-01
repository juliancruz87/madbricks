using UnityEngine;
using System.Collections;

namespace Interactive.Detail
{
	public class TotemInstantiatorConfig : ScriptableObject
	{
		[SerializeField]
		private TotemType type;
		
		[SerializeField]
		private GameObject prefab;
		
		[SerializeField]
		private int positionToAdd;
		
		public TotemType Type 
		{
			get{ return type; }
		}
		
		public GameObject Prefab 
		{
			get { return prefab; }
		}
		
		public int PositionToAdd 
		{
			get { return positionToAdd; }
		}
	}
}