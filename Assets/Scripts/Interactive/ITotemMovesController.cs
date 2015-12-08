using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Interactive
{
	public interface ITotemMovesController
	{
		void CreatePositionsToSnap (Transform parentForPositionsToSnap);
	}
	
}