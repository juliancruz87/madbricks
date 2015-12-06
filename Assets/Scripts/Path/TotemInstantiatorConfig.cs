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

		[SerializeField]
		private int positionToGo;

		[SerializeField]
		private float speedPerTile = 0.5F;

		[SerializeField]
		private AudioClip soundToGetReach;
		
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

		public int PositionToGo 
		{
			get { return positionToGo; }
		}

		public float SpeedPerTile 
		{
			get { return speedPerTile;}
		}

		public AudioClip SoundToGetReach 
		{
			get { return soundToGetReach; }
		}
	}
}