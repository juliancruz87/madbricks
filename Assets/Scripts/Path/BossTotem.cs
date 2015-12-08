using System;
using Drag;
using UnityEngine;
using Interactive.Detail;

namespace Path {
    public class BossTotem : MonoBehaviour {

        [SerializeField]
        private DraggableObject[] totems;

        [SerializeField]
        private Node currentNode;

        private Transform myTransform;
        private SnapItemToCloserPosition snapperObject;

        private void Awake() {
            myTransform = transform;
            snapperObject = GetComponent<SnapItemToCloserPosition>();
        }

        // Use this for initialization
        void Start () {
            InitTotems();
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
            Vector3 opositeStep = currentposition - newposition;
            
            Vector3 newDragPosition = opositeStep + myTransform.position;

            GetTheCorrectedPosition(ref newDragPosition, (opositeStep.normalized) * -1);

            myTransform.position = newDragPosition;
        }

        private void GetTheCorrectedPosition(ref Vector3 newDragPosition, Vector3 direction) {
            Debug.Log(direction);

            if (direction == new Vector3()) {
                newDragPosition = myTransform.position;
                return;
            }

            if (!EnabledToMove(direction)) 
                newDragPosition = myTransform.position;
        }

        private bool EnabledToMove(Vector3 direction) {
            Node nextNode = GetNextNodeInDirection(direction);
            return nextNode;
        }

        private Node GetNextNodeInDirection(Vector3 direction) {
            Node nearestNode = PathBuilder.Instance.Finder.GetNearsetNodeInDirection(currentNode, direction);
            return nearestNode;
        }

        // Update is called once per frame
        void Update () {
            UpdateNearestNode();
        }

        private void UpdateNearestNode() {
            Node nearestNode = PathBuilder.Instance.GetNearsetNode(myTransform.position);
            if (nearestNode != null &&
                Math.Abs(Vector3.Distance(nearestNode.transform.position, myTransform.position)) < 0.01 &&
                currentNode != nearestNode)
                currentNode = nearestNode;
        }

        void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.GetComponent<DraggableObject>())
                Destroy(collision.gameObject);
        }
    }
}
