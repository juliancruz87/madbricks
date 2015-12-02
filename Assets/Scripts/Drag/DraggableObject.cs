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
        private AudioSource dragSound;

        //TODO: Make it private after test
        [SerializeField]
        private bool allowMultipleDrags = false;
        [SerializeField]
        private bool isBeingDragged = false;
        [SerializeField]
        private Vector3 startPosition;
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
			snapperObject = GetComponent<SnapItemToCloserPosition>();
		}

        private void Update() {
            UpdateNearestNode();

            if (!isBeingDragged)
                CheckMouseInput();
            else {
                UpdateDrag();
                if (Input.GetMouseButtonUp(0)) 
                    StopDrag();
            }
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

                if (RaycastHitsThisGameObject(raycastHits) &&
                    ThisGameObjectIsTheFirstHit(raycastHits) && 
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

        private bool RaycastHitsThisGameObject(RaycastHit[] raycastHits) {
            foreach (RaycastHit raycast in raycastHits)
                if (raycast.transform.gameObject == gameObject)
                    return true;
        
            return false;
        }

        private void UpdateDrag() {
            if (isBeingDragged) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                float distance1 = 0f;
                if (horizontalPlane.Raycast(ray, out distance1)) {
                    Vector3 newDragPosition = ray.GetPoint(distance1) + inputStartOffset;
                    if (newDragPosition != myTransform.position) {
                        if (startDragDirection == new Vector3())
                            SetStartDragDirection(newDragPosition);

                        GetTheCorrectedPosition(ref newDragPosition);
                    }
                    else 
                        startDragDirection = new Vector3();

                    if (OnObjectDragged != null)
                        OnObjectDragged(myTransform.position, newDragPosition);

                    myTransform.position = newDragPosition;
                }
            }
            //DebugDragDirection();
        }

        private void DebugDragDirection() {
            Vector3 debugOffset = new Vector3(0, 0.25f, 0);
            Debug.DrawLine(myTransform.position + debugOffset, (myTransform.position + (startDragDirection * 0.25f) + debugOffset), Color.green);
        }

        private void SetStartDragDirection(Vector3 newDragPosition) {
            Vector3 desiredDirection = newDragPosition - myTransform.position;
            startDragDirection = GetDominantDirection(desiredDirection);
        }

        private void GetTheCorrectedPosition(ref Vector3 newDragPosition) {
            Vector3 desiredDirection = new Vector3();
            if (Math.Abs(startDragDirection.x) > 0) {
                desiredDirection = new Vector3(1,0,0);
                newDragPosition.z = startPosition.z;
                if (newDragPosition.x > myTransform.position.x)
                    desiredDirection *= -1;
            }
            else if (Math.Abs(startDragDirection.z) > 0) {
                desiredDirection = new Vector3(0, 0, 1);
                newDragPosition.x = startPosition.x;
                if (newDragPosition.z > myTransform.position.z)
                    desiredDirection *= -1;
            }

            if (!EnabledToMove(desiredDirection))
                newDragPosition = myTransform.position;
        }

        private bool EnabledToMove(Vector3 desiredDirection) {
            Vector3 debugOffset = new Vector3(0, 0.3f, 0);
            Debug.DrawLine(myTransform.position + debugOffset,
                            (myTransform.position + debugOffset + (desiredDirection * PathBuilder.Instance.maxNodeDistance)), 
                            Color.blue);
            Node nextNode = GetNextNodeInDirection(desiredDirection);
            return nextNode;
        }

        private Node GetNextNodeInDirection(Vector3 desiredDirection) {
            Node nearestNode = PathBuilder.Instance.Finder.GetNearsetNodeInDirection(currentNode, desiredDirection);
            return nearestNode;
        }

        private Vector3 GetDominantDirection(Vector3 desiredDirection) {
            Vector3 dominantDirection = new Vector3();

            if (Math.Abs(desiredDirection.normalized.x) > Math.Abs(desiredDirection.normalized.z)) {
                dominantDirection = new Vector3(1, 0, 0);
                if (desiredDirection.x < 0)
                    dominantDirection *= -1;
                return dominantDirection;
            }
            else if (Math.Abs(desiredDirection.normalized.x) < Math.Abs(desiredDirection.normalized.z)) {
                dominantDirection = new Vector3(0, 0, 1);
                if (desiredDirection.z < 0)
                    dominantDirection *= -1;
                return dominantDirection;
            }

            return new Vector3(0, 0, 0);
        }

        private void StartDrag() {
            startDragDirection = new Vector3();
            isBeingDragged = true;
            objectBeingDragged = this;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance1 = 0f;
            if (horizontalPlane.Raycast(ray, out distance1)) {
                inputStartOffset = transform.position - ray.GetPoint(distance1);
                startPosition = this.transform.position;
            }

            if (dragSound != null)
                dragSound.Play();
        }

        public void StopDrag() 
		{
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