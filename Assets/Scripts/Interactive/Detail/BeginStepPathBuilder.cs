using UnityEngine;

namespace Interactive.Detail {
    
    public class BeginStepPathBuilder : BeginStepGameBase {
        [SerializeField]
        private GameObject pathPrototype;

        [SerializeField]
        private Transform mapTransform;

        public override void StartStep() {
            CreatePath();
        }

        private void CreatePath() {
            Instantiate(pathPrototype, mapTransform.position, mapTransform.rotation);
            if (EndStep != null)
                EndStep();
        }
    }
}