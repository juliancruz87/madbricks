using UnityEngine;

namespace Interactive.Detail {
    
    public class BeginStepPathBuilder : BeginStepGameBase
	{
        [SerializeField]
        private GameObject pathPrototype;
       
		[SerializeField]
        private Transform mapTransform;

		[SerializeField]
		private Transform riel;

        private Transform pathTransform;

        public override void StartStep() 
		{
            CreatePath();
        }

        private void CreatePath() 
		{
			Transform rielTransform = Instantiate<Transform> (riel);
			pathTransform = Instantiate <GameObject> (pathPrototype).transform;

			rielTransform.SetParent(mapTransform);
			pathTransform.SetParent(mapTransform);

			rielTransform.localPosition = Vector3.zero;
			pathTransform.localPosition = Vector3.zero;
			pathTransform.GetComponent<TotemsLevelCreator> ().SetUp ();
				
			if (EndStep != null)
                EndStep();
        }
    }
}