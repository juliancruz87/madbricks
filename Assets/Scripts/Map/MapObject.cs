using UnityEngine;

namespace Map {
    public class MapObject : MonoBehaviour {

        private const string TAG_OBSTACLE = "Obstacle";

        [SerializeField]
        private Transform startPosition;

        [SerializeField]
        private Vector3 startPositionOffset;

        [SerializeField]
        protected MapObjectType type;

        public MapObjectType Type {
            get { return type; }
        } 

        private void Awake() {
			SetStartPosition (startPosition);
            InitMapObjectType();
		}

        private void InitMapObjectType() {
            switch (type) {
                case MapObjectType.Totem_target:
                    InitAsTotemTarget();
                    break;
                default:
                    break;
            }
                
        }

        private void InitAsTotemTarget() {
            gameObject.tag = TAG_OBSTACLE;
        }

        public void SetStartPosition (Transform position) {
			if (position != null) {
				startPosition = position;
				transform.position = position.position + startPositionOffset;
			}
		}
    }
}
