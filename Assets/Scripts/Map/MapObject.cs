using UnityEngine;

namespace Map {
    public class MapObject : MonoBehaviour {

        [SerializeField]
        private Transform startPosition;

        [SerializeField]
        private Vector3 startPositionOffset;

        // Use this for initialization
        private void Awake() {
            if (startPosition != null)
                transform.position = startPosition.position + 
                                        startPositionOffset;
        }
    }
}
