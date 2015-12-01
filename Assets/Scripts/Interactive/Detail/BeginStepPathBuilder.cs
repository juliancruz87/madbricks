using UnityEngine;

namespace Interactive.Detail {
    
    public class BeginStepPathBuilder : BeginStepGameBase 
	{
        [SerializeField]
        private GameObject pathPrototype;
        [SerializeField]
        private Transform mapTransform;

        private Transform pathTransform;

        public override void StartStep() 
		{
            CreatePath();
        }

        private void CreatePath() 
		{
            pathTransform = ((GameObject)Instantiate(pathPrototype, mapTransform.position, mapTransform.rotation)).transform;
			pathTransform.parent = mapTransform;
			pathTransform.localPosition = Vector3.zero;
            if (EndStep != null)
                EndStep();
        }
    }
}