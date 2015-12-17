﻿using System;
using Map;
using UnityEngine;
using Path;
using Interactive.Detail;

// TODO: Extend to touch input


namespace Drag {
    public class DraggableObject : MonoBehaviour {

        private const string TAG_OBSTACLE = "Obstacle";
        private const float NODE_TOLERANCE = 0.05f;

        private static DraggableObject objectBeingDragged;

        public OnObjectDragged OnObjectDragged;
        public Action OnObjectStopDrag;
        public Action OnSnap;
        public Action OnNodeUpdated;
        public OnLauncherTouched OnLauncherTouched;

        [SerializeField]
        private float collideDistance;
        [SerializeField]
        private float styckDistance;
        [SerializeField]
        private float maxJumpDistance;
        [SerializeField]
        private AudioSource dragSound;
        [SerializeField]
        private GameObject dragFloor;
        private Collider dragFloorCollider;
        [SerializeField]
        private bool allowMultipleDrags = false;
        [SerializeField]
        private bool isBeingDragged = false;
        [SerializeField]
        private Vector3 inputStartOffset = new Vector3();
        //TODO: Configure the plane as the gameobject thing
        [SerializeField]
        private Plane horizontalPlane = new Plane(Vector3.up, Vector3.zero);
        [SerializeField]
        private Node currentNode;

        private Vector3 startDragDirection;
        private Vector3 lastDragPosition;
        private MapObject currentLauncher;

        private AudioSource collisionAudioSource;
        [SerializeField] 
        private AudioClip collideAudioClip;

        public Node CurrentNode 
		{
			get { return currentNode; }
		}

		private SnapItemToCloserPosition snapperObject;
        private Transform myTransform;

		public Node SnapperObject 
		{
			get { return snapperObject.NodeSpnaped; }
		}
        
		private void Start () {
            collisionAudioSource = GameObject.Find("CollisionSoundPlayer").GetComponent<AudioSource>();
            myTransform = transform;
            dragFloor = GameObject.FindWithTag("Floor");
		    dragFloorCollider = dragFloor.GetComponent<Collider>();
			snapperObject = GetComponent<SnapItemToCloserPosition>();
		}

        private void Update() {
            UpdateNearestNode();

            CheckMouseInput();

            if (isBeingDragged) {
                UpdateDrag();

                if (Input.GetMouseButtonUp(0)) 
                    StopDrag();
            }
        }

        private void UpdateNearestNode() {
            Node lastNode = currentNode;
            Node nearestNode = PathBuilder.Instance.GetNearsetNode(myTransform.position);
            if (nearestNode != null &&
                Math.Abs(Vector3.Distance(nearestNode.transform.position, myTransform.position)) < NODE_TOLERANCE &&
                currentNode != nearestNode) {
                currentNode = nearestNode;
                if (OnNodeUpdated != null && 
                    lastNode != null)
                    OnNodeUpdated();
            }
                
        }

        private void CheckMouseInput() {
            if (Input.GetMouseButtonDown(0)) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit[] raycastHits = Physics.RaycastAll(ray);

                if (ThisGameObjectIsTheFirstHit(raycastHits) && 
                    IsAllowedToStartANewDrag()) {
                    StartDrag();
                }
                else 
                    StopDrag();
            }
        }

        private bool ThisGameObjectIsTheFirstHit(RaycastHit[] raycastHits) {
            GameObject firstGameObject = GetTheFirstGameObject(raycastHits);
            return firstGameObject == gameObject;
        }

        private GameObject GetTheFirstGameObject(RaycastHit[] raycastHits) {
            GameObject firstGameObject = null;
            float nearestDistance = float.MaxValue;
            foreach (RaycastHit hitinfo in raycastHits) {
                if (hitinfo.collider.gameObject.GetComponent<DraggableObject>() != null) {
                    float distance = Vector3.Distance(Camera.main.transform.position, hitinfo.collider.transform.position);
                    if (distance < nearestDistance) {
                        firstGameObject = hitinfo.collider.gameObject;
                        nearestDistance = distance;
                    }
                }
            }

            return firstGameObject;
        }

        private bool IsAllowedToStartANewDrag() {
            if (allowMultipleDrags)
                return true;
            else
                return objectBeingDragged == null;
        }

        private bool RaycastHitsGameObject(RaycastHit[] raycastHits, GameObject someGameObject) {
            foreach (RaycastHit raycast in raycastHits)
                if (raycast.transform.gameObject == someGameObject)
                    return true;
        
            return false;
        }

        private void UpdateDrag() {
            if (isBeingDragged) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                float distance1 = 0f;
                if (horizontalPlane.Raycast(ray, out distance1)) {
                    Vector3 newDragPosition = ray.GetPoint(distance1) + inputStartOffset;
                    Vector3 dragStep = newDragPosition - lastDragPosition;
                    Vector3 candidatePosition = myTransform.position + dragStep;
                    
                    bool hitFloor = HitFloorAtPosition(candidatePosition);
                    //bool hitFloor = RaycastHitsGameObject(hits, dragFloor);
                    //DebugHitFloor(hitFloor, rayStartPoint);

                    lastDragPosition = newDragPosition;

                    if (!hitFloor) 
                        if(!CandidatePositionCanBeFixed(ref candidatePosition, dragStep))
                            return;

                    if (Vector3.Distance(myTransform.position, candidatePosition) > maxJumpDistance)
                        return;

                    if (ItWillHitAnotherTotem(candidatePosition))
                        return;

                    if (OnObjectDragged != null)
                        OnObjectDragged(myTransform.position, candidatePosition);

                    myTransform.position = candidatePosition;
                }
            }
            DebugDragDirection();
            CheckMapObjectCondition();
        }

        private bool CandidatePositionCanBeFixed(ref Vector3 candidatePosition, Vector3 dragStep) {
            Vector3 newCandidatePosition = candidatePosition;
            float absX = Math.Abs(dragStep.x);
            float absZ = Math.Abs(dragStep.z);
            if (absX > absZ) {
                newCandidatePosition.z = myTransform.position.z;
                if (HitFloorAtPosition(newCandidatePosition)) {
                    candidatePosition = newCandidatePosition;
                    return true;
                }
                
                newCandidatePosition = candidatePosition;
                newCandidatePosition.x = myTransform.position.x;
                if (HitFloorAtPosition(newCandidatePosition)) {
                    candidatePosition = newCandidatePosition;
                    return true;
                }

                return false;
            }
            
            newCandidatePosition.x = myTransform.position.x;
            if (HitFloorAtPosition(newCandidatePosition)) {
                candidatePosition = newCandidatePosition;
                return true;
            }
            
            newCandidatePosition = candidatePosition;
            newCandidatePosition.z = myTransform.position.z;
            if (HitFloorAtPosition(newCandidatePosition)) {
                candidatePosition = newCandidatePosition;
                return true;
            }

            return false;
        }

        private bool HitFloorAtPosition(Vector3 position) {
            Vector3 rayStartPoint = (position + new Vector3(0, 0.5f, 0));
            Ray ray = new Ray(rayStartPoint, Vector3.down);
            RaycastHit hitInfo = new RaycastHit();
            dragFloorCollider.Raycast(ray, out hitInfo, 10f);
            return hitInfo.collider == dragFloorCollider;
        }

        private void CheckMapObjectCondition() {
            MapObject nearestMapObject = GetNearestMapObject();
            if (nearestMapObject != null) {
                ProcessMapObjectCollision(nearestMapObject);
            }
        }

        private void ProcessMapObjectCollision(MapObject nearestMapObject) {
            switch (nearestMapObject.Type) {
                case MapObjectType.LauncherNormal:
                    SetNewLauncher(nearestMapObject);
                    break;
                case MapObjectType.LauncherSticky:
                    SetNewLauncher(nearestMapObject);
                    StickToLauncher(nearestMapObject);
                    break;
            }
        }

        private void SetNewLauncher(MapObject newLauncher) {
            if (newLauncher != null &&
                newLauncher != currentLauncher &&
                OnLauncherTouched != null) {
                currentLauncher = newLauncher;
                OnLauncherTouched(newLauncher);
            }
                
        }

        private MapObject GetNearestMapObject() {
            MapObject[] mapObjects = FindObjectsOfType<MapObject>();

            foreach (MapObject mapObject in mapObjects)
                if (Vector3.Distance(mapObject.transform.position, myTransform.position) < styckDistance &&
                    (mapObject.Type == MapObjectType.LauncherSticky ||
                     mapObject.Type == MapObjectType.LauncherNormal))
                    return mapObject;

            return null;
        }

        private void StickToLauncher(MapObject stickyLauncher) {
            gameObject.GetComponent<Collider>().enabled = false;
            StopDrag();
        }


        private void OnCollisionEnter(Collision collision) {
            if (isBeingDragged && 
                collision.gameObject.tag == TAG_OBSTACLE &&
                collision.gameObject.GetComponent<DraggableObject>() != null
                ) {
                PlayCollideSound();
            }
        }

        private bool ItWillHitAnotherTotem(Vector3 newDragPosition) { 
            GameObject[] totems = GameObject.FindGameObjectsWithTag(TAG_OBSTACLE);
            foreach (GameObject totem in totems) {
                if (totem != gameObject &&
                    Vector3.Distance(totem.transform.position, newDragPosition) < collideDistance) {
                    return true;
                }
            }
            return false;
        }

        private void PlayCollideSound() {
            if (!collisionAudioSource.isPlaying) {
                collisionAudioSource.Play();
            }
        }

        private void DebugHitFloor(bool hitFloor, Vector3 rayStartPoint) {
            Color rayColor = Color.red;
            if (hitFloor)
                rayColor = Color.green;
            
            Debug.DrawLine(rayStartPoint, rayStartPoint + (5 * Vector3.down), rayColor);
        }

        private void DebugDragDirection() {
            Vector3 debugOffset = new Vector3(0, 0.25f, 0);
            Debug.DrawLine(myTransform.position + debugOffset, (myTransform.position + (startDragDirection * 0.25f) + debugOffset), Color.green);
        }

        private void StartDrag() {
            lastDragPosition = myTransform.position;
            startDragDirection = new Vector3();
            isBeingDragged = true;
            objectBeingDragged = this;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance1 = 0f;
            if (horizontalPlane.Raycast(ray, out distance1)) 
                inputStartOffset = transform.position - ray.GetPoint(distance1);
            
            if (dragSound != null)
                dragSound.Play();
        }

        public void StopDrag() {
            if (OnObjectStopDrag != null)
                OnObjectStopDrag();

            if (isBeingDragged)
                Snap();

            isBeingDragged = false;

            if (objectBeingDragged == this)
                objectBeingDragged = null;
            
            if (dragSound != null)
                dragSound.Stop();
        }

        private void Snap() {
			if (snapperObject != null)
				snapperObject.SnapToCloserTransform();

            if (OnSnap != null)
                OnSnap();
        }

        private void OnDestroy() {
            if (this == objectBeingDragged && OnSnap != null)
                OnSnap();
        }
    }

    public delegate void OnObjectDragged(Vector3 currentPosition, Vector3 newPosition);
    public delegate void OnLauncherTouched(MapObject launcher);
}