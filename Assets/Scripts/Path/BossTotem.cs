using System;
using System.Collections;
using Drag;
using UnityEngine;
using Interactive.Detail;
using Map;
using Interactive;
using Graphics;
using System.Collections.Generic;

namespace Path {
    public class BossTotem : MonoBehaviour, ITotem {

        private const string TAG_OBSTACLE = "Obstacle";

        [SerializeField]
        private MovementSettings settings;

        [SerializeField]
        private bool isJailed = false;

        [SerializeField]
        private DraggableObject[] totems;

        [SerializeField]
        private Node currentNode;

        private GameObject dragFloor;

        private Animator myAnimator;
        private Transform myTransform;
        private SnapItemToCloserPosition snapperObject;
		private HighlightObject highlightObject;
		
		public bool IsBoss 
		{
			get { return true; }
		}

		public Node CurrentNode 
		{
			get { return currentNode; }
		}

		public DraggableObject DragObject
		{
			get { return null; }
		}

		public bool IsJailed 
		{
			get { return isJailed; }
		}

		public bool IsDragged
		{
			get { return false; }
		}

        public bool IsInStartPoint
        {
            get { return isJailed; }
        }

        private IGameManagerForStates GameManagerForStates
		{
			get { return GameManager.Instance;}
		}

        private void Awake() {
            dragFloor = GameObject.FindWithTag("Floor");
            myTransform = transform;
            snapperObject = GetComponent<SnapItemToCloserPosition>();
			highlightObject = GetComponent<HighlightObject> ();
            myAnimator = GetComponentInChildren<Animator>();
        }

        // Use this for initialization
        void Start () {
            InitTotems();
        }


		public void Stop()
		{
			return;	
		}

		public void SetHighlight (bool IsActive)
		{
			if (highlightObject != null)
			{
				if (IsActive)
					highlightObject.ActivateHighlight ();
				else
					highlightObject.DeactivateHighlight ();
			}
		}

        private void InitTotems() {
            totems = FindObjectsOfType<DraggableObject>();
            foreach (DraggableObject totem in totems) {
                totem.OnObjectDragged = OnTotemDragged;
                totem.OnSnap = Snap;
            }
        }

        private void Snap() {
            if (snapperObject != null)
                snapperObject.SnapToCloserTransform();
        }

        private void OnTotemDragged(Vector3 currentposition, Vector3 newposition) {
            if (!isJailed) {
                Vector3 opositeStep = currentposition - newposition;
                Vector3 newDragPosition = opositeStep + myTransform.position;

                MoveIfPossible(newDragPosition);
            }
        }

        private void CheckMapObjectCondition() {
            MapObject nearestMapObject = GetNearestMapObject();
            if (nearestMapObject != null) {
                ProcessMapObjectCollision(nearestMapObject);
            }
        }

        private void ProcessMapObjectCollision(MapObject nearestMapObject) {
            switch (nearestMapObject.Type) {
                case MapObjectType.BossJail:
                    GetIntoJail();
                    break;
            }
        }

        private void GetIntoJail() 
		{
			if (isJailed)
				return;

            myAnimator.SetTrigger("Land");
            isJailed = true;
			GameManager.Instance.Goal ();
        }

        private MapObject GetNearestMapObject() {
			List<MapObject> mapObjects = MapObject.GetMapObjectsOfType(MapObjectType.BossJail);

            foreach (MapObject mapObject in mapObjects)
                if (Vector3.Distance(mapObject.transform.position, myTransform.position) < settings.StickyDistance)
                    return mapObject;

            return null;
        }

        private void MoveIfPossible(Vector3 newDragPosition) {
            Vector3 candidatePosition = (newDragPosition);
            Vector3 rayStartPoint = (candidatePosition + new Vector3(0, 0.5f, 0));

            RaycastHit[] hits = Physics.RaycastAll(rayStartPoint, Vector3.down);

            bool hitFloor = RaycastHitsGameObject(hits, dragFloor);
            DebugHitFloor(hitFloor, rayStartPoint);

            if (!hitFloor)
                return;

            if (Vector3.Distance(myTransform.position, candidatePosition) > settings.MaxJumpDistance)
                return;

            if (WillHitAnObstacle(candidatePosition)) 
                return;

            myTransform.position = candidatePosition;
        }

        private bool WillHitAnObstacle(Vector3 newDragPosition) {
            GameObject[] obstacles = GameObject.FindGameObjectsWithTag(TAG_OBSTACLE);
            foreach (GameObject obstacle in obstacles) {
                if (obstacle != gameObject &&
                    Vector3.Distance(obstacle.transform.position, newDragPosition) < settings.CollideDistance) {
                    if (obstacle.GetComponent<DraggableObject>()) {
                        myAnimator.SetTrigger("Explode");
                        Destroy(obstacle);
                        GameManagerForStates.Lose();
                    }
                    return true;

                }
            }
            return false;
        }

        private bool RaycastHitsGameObject(RaycastHit[] raycastHits, GameObject someGameObject) {
            foreach (RaycastHit raycast in raycastHits)
                if (raycast.transform.gameObject == someGameObject)
                    return true;

            return false;
        }

        private void DebugHitFloor(bool hitFloor, Vector3 rayStartPoint) {
            Color rayColor = Color.red;
            if (hitFloor)
                rayColor = Color.green;

            Debug.DrawLine(rayStartPoint, rayStartPoint + (5 * Vector3.down), rayColor);
        }

        // Update is called once per frame
        void Update () {
            UpdateNearestNode();
            CheckMapObjectCondition();
            if (!isJailed)
                CheckTotemCollision();
        }

        private void UpdateNearestNode() {
            Node nearestNode = PathBuilder.Instance.GetNearsetNode(myTransform.position);
            if (nearestNode != null &&
                Math.Abs(Vector3.Distance(nearestNode.transform.position, myTransform.position)) < 0.01 &&
                currentNode != nearestNode)
                currentNode = nearestNode;
        }

        private void CheckTotemCollision() {
            DraggableObject[] totems = FindObjectsOfType<DraggableObject>();
            foreach (DraggableObject totem in totems) {
                if (Vector3.Distance(totem.transform.position, transform.position) < settings.CollideDistance) {
                    Destroy(totem.gameObject);
                    GameManagerForStates.Lose();
                }
            }
        }

        public Vector3[] GetPathPositions()
        {
            return new Vector3 [] { };
        }
    }
}
