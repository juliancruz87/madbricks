using UnityEngine;
using System.Collections;
using Zenject;

namespace Interactive.Installers
{
	public class InstallerLevel1_1 : MonoInstaller 
	{
		[SerializeField]
		private GameManager gameManager;

		public override void InstallBindings ()
		{
			Container.Bind<IGameManagerForStates> ().ToInstance (gameManager);
			Container.Bind<IGameManagerForUI> ().ToInstance (gameManager);
		}
	}
}