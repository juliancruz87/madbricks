using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Interactive.Detail
{
	public enum TriangleTurnDirectionType
	{
		None = 0,
		Left = 1,
		Right = 2
	}
	public class TotemInstantiatorConfig : ScriptableObject
	{
		public TriangleTurnDirectionType preferedTriangleDirection;

		[SerializeField]
		private TotemType type;
		
		[SerializeField]
		private GameObject prefab;
		
		[SerializeField]
		private int positionToAdd;

		[SerializeField]
		private int positionToGo;

        [SerializeField]
        private int secondaryPositionToGo;

		[SerializeField]
		private int optimalPositionToStart;

		[SerializeField]
		private float speedPerTile = 0.5F;

		[SerializeField]
		private AudioClip soundToGetReach;

		[SerializeField]
		private bool isDebugTotemMove = false;

		[SerializeField]
		private List<TotemBehaviours> behavioursTypes;

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
			get 
			{ 
				if(isDebugTotemMove && Application.isEditor)
					return optimalPositionToStart;
				else
					return positionToAdd; 
			}
		}

		public int PositionToGo 
		{
			get { return positionToGo; }
		}

        public int SecondaryPositionToGo
        {
            get { return secondaryPositionToGo; }
        }

		public float SpeedPerTile 
		{
			get { return speedPerTile;}
		}

		public AudioClip SoundToGetReach 
		{
			get { return soundToGetReach; }
		}

		public List<TotemBehaviours> BehavioursTypes 
		{
			get { return behavioursTypes; }
		}
	}
}