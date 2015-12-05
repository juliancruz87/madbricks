using System;
using InteractiveObjects.Detail;
using UnityEngine;
using Path;

// TODO: Extend to touch input


namespace Drag {
    public class DraggableObject : MonoBehaviour {
        
        private static DraggableObject objectBeingDragged;

        public OnObjectDragged OnObjectDragged;
        public Action OnSnap;

        [SerializeField]
        private float collideDistance;
        [SerializeField]
        private float maxJumpDistance;
        [SerializeField]
        private AudioSource dragSound;
        [SerializeField]
        private GameObject dragFloor;
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
            myTransform = transform;
            dragFloor = GameObject.FindWithTag("Floor");
			snapperObject = GetComponent<SnapItemToCloserPosition>();
		}

        private void Update() {
            UpdateNearestNode();

            CheckMouseInput();

            if (isBeingDragged) {
                UpdateDrag();

                if (InputIsOut() ||
                    Input.GetMouseButtonUp(0)) 
                    StopDrag();
            }
        }

        private bool InputIsOut() {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] raycastHits = Physics.RaycastAll(ray);
            return !RaycastHitsGameObject(raycastHits, gameObject);
        }

        private void UpdateNearestNode() {
            Node nearestNode = PathBuilder.Instance.GetNearsetNode(myTransform.position);
            if (nearestNode != null && 
                Math.Abs(Vector3.Distance(nearestNode.transform.position, myTransform.position)) < 0.01 &&
                currentNode != nearestNode)
                currentNode = nearestNode;
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
                float distance = Vector3.Distance(Camera.main.transform.position, hitinfo.collider.transform.position);
                if (distance < nearestDistance) {
                    firstGameObject = hitinfo.collider.gameObject;
                    nearestDistance = distance;
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

                    if (Vector3.Distance(myTransform.position, newDragPosition) > maxJumpDistance)
                        return;

                    if (ItWillHitAnotherTotem(newDragPosition))
                        return;

                    Vector3 rayStartPoint = (newDragPosition + new Vector3(0, 0.5f, 0));
                    
                    RaycastHit[] hits = Physics.RaycastAll(rayStartPoint, Vector3.down);
                    
                    bool hitFloor = RaycastHitsGameObject(hits, dragFloor);
                    DebugHitFloor(hitFloor, rayStartPoint);

                    if (!hitFloor)
                        return;

                    if (OnObjectDragged != null)
                        OnObjectDragged(myTransform.position, newDragPosition);

                    myTransform.position = newDragPosition;
                }
            }
            DebugDragDirection();
        }

        private bool ItWillHitAnotherTotem(Vector3 newDragPosition) { 
            GameObject[] totems = GameObject.FindGameObjectsWithTag("Totem");
            foreach (GameObject totem in totems) {
                if (totem != gameObject &&
                    Vector3.Distance(totem.transform.position, newDragPosition) < collideDistance) {
                    return true;
                }
            }
            return false;
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
}