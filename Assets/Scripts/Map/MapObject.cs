using System.Collections;
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
			AnimatedTexture at;
			switch (type) {
                case MapObjectType.Totem_target:
                    InitAsTotemTarget();
                    break;
				case MapObjectType.LauncherNormal:
					at = gameObject.AddComponent<AnimatedTexture> ();
					at.FramesPerSecond = 3f;
                    break;
                case MapObjectType.BossJail:
                    at = gameObject.AddComponent<AnimatedTexture>();
					at.FramesPerSecond = 10f;
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

        public static ArrayList GetMapObjectsOfType(MapObjectType type) {
            ArrayList filteredMapObjects = new ArrayList();

            MapObject[] mapObjects = FindObjectsOfType<MapObject>();

            foreach (MapObject mapObject in mapObjects)
            {
                if (mapObject.Type == type)
                    filteredMapObjects.Add(mapObject);
            }

            return filteredMapObjects;
        }
    }
}
