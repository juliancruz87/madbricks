using UnityEngine;

namespace Map {
    public class MapObject : MonoBehaviour {

        [SerializeField]
        private Transform startPosition;

        [SerializeField]
        private Vector3 startPositionOffset;

        private void Awake() 
		{
			SetStartPosition (startPosition);
        }

		public void SetStartPosition (Transform position)
		{
			if (position != null) 
			{
				startPosition = position;
				transform.position = position.position + startPositionOffset;
			}
		}
    }
}
