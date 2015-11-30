using UnityEngine;

namespace Interactive.Detail {
    
    public class BeginStepPathBuilder : BeginStepGameBase {
        [SerializeField]
        private GameObject pathPrototype;

        [SerializeField]
        private Transform mapTransform;

        private Transform pathTransform;

        public override void StartStep() {
            CreatePath();
        }

        private void CreatePath() {
            pathTransform = ((GameObject) Instantiate(pathPrototype, mapTransform.position, mapTransform.rotation)).transform;
            if (EndStep != null)
                EndStep();
        }

        private void Update() {
            if (pathTransform != null &&
                mapTransform != null)
            pathTransform.position = mapTransform.position;
        }
    }
}