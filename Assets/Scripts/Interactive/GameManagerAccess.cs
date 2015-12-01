using UnityEngine;
using System.Collections;
using Interactive.Detail;

namespace Interactive
{
	public static class GameManagerAccess
	{
		public static IGameManagerForStates GameManagerState
		{
			get{ return GameManager.Instance;}
		}
	} 
	
}