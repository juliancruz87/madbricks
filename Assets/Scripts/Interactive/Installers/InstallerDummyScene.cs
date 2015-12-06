using UnityEngine;
using System.Collections;
using Zenject;
using Interactive;

public class InstallerDummyScene : MonoInstaller 
{
	[SerializeField]
	private GameObject gameManagerDummy;

	public override void InstallBindings ()
	{
		Container.Bind<IGameManagerForStates> ().ToInstance (gameManagerDummy.GetComponent<IGameManagerForStates>());
	}
}