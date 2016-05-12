using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Path;

namespace Map {
    public class MapObject : MonoBehaviour {

        private const string TAG_OBSTACLE = "Obstacle";

        [SerializeField]
        private Transform startPosition;

        [SerializeField]
        private Vector3 startPositionOffset;

        [SerializeField]
        protected MapObjectType type;

		private Renderer myRenderer;
		private Texture originalTexture;

        public MapObjectType Type {
            get { return type; }
        } 

		public Node ParentNode
		{
			get { return PathBuilder.Instance.GetNearsetNode (startPosition.position); }

		}

        private void Awake() {
			myRenderer = GetComponentInChildren<Renderer> ();
			originalTexture = myRenderer.material.mainTexture;
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
				case MapObjectType.LauncherSticky:
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

		public void ChangeTexture(Texture newtexture) 
		{
			myRenderer.material.mainTexture = newtexture;	
		}

		public void ResetTexture() 
		{
			ChangeTexture (originalTexture);
		}

		public static List<MapObject>GetMapObjectsOfType(params MapObjectType[] types)
		{
			List<MapObject> filteredMapObjects = new List<MapObject> ();

			MapObject[] mapObjects = FindObjectsOfType<MapObject> ();

			foreach (MapObject mapObject in mapObjects) {
				foreach (MapObjectType type in types) {
					if (mapObject.Type == type)
						filteredMapObjects.Add (mapObject);
				}
			}

			return filteredMapObjects;
		}

        /*public static ListGetMapObjectsOfType(MapObjectType type) {
            ArrayList filteredMapObjects = new ArrayList();

            MapObject[] mapObjects = FindObjectsOfType<MapObject>();

            foreach (MapObject mapObject in mapObjects)
            {
                if (mapObject.Type == type)
                    filteredMapObjects.Add(mapObject);
            }

            return filteredMapObjects;
        }*/
    }
}
