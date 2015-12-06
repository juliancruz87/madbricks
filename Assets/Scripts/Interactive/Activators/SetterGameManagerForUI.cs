using UnityEngine;
using System.Collections;
using Zenject;

namespace Interactive
{
	public interface SetterGameManagerForStates
	{
		IGameManagerForStates GameManager { set; }
	}
	
}