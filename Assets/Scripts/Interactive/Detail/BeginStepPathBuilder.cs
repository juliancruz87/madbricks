using UnityEngine;
using Zenject;

namespace Interactive.Detail {
    
    public class BeginStepPathBuilder : BeginStepGameBase
	{
        [SerializeField]
        private GameObject pathPrototype;
       
		[SerializeField]
        private Transform mapTransform;

		[SerializeField]
		private Transform riel;

		[Inject]
		private IGameManagerForStates gameStates;

        private Transform pathTransform;

        public override void StartStep() 
		{
            CreatePath();
        }

        private void CreatePath() 
		{
            //pathTransform = ((GameObject)Instantiate(pathPrototype, mapTransform.position, mapTransform.rotation)).transform;
			Transform rielTransform = Instantiate<Transform> (riel);
			pathTransform = Instantiate <GameObject> (pathPrototype).transform;

			rielTransform.parent = mapTransform;
			pathTransform.parent = mapTransform;

			rielTransform.localPosition = Vector3.zero;
			pathTransform.localPosition = Vector3.zero;
			pathTransform.GetComponent<TotemsLevelCreator> ().SetUp (gameStates);
				
			if (EndStep != null)
                EndStep();
        }
    }
}