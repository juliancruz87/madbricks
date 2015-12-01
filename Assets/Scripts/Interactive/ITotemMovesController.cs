using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace InteractiveObjects
{
	public interface ITotemMovesController
	{
		void CreatePositionsToSnap (Transform parentForPositionsToSnap);
	}
	
}