﻿using UnityEngine;

// TODO: Extend to touch input
namespace Drag {
    public class DraggableObject : MonoBehaviour {

        [SerializeField]
        private AudioSource dragSound;

        //TODO: Make it private after test
        [SerializeField]
        private bool isBeingDragged = false;
        [SerializeField]
        private Vector3 startPosition;
        [SerializeField]
        private Vector3 inputStartOffset = new Vector3();
        [SerializeField]
        private Plane horizontalPlane = new Plane(Vector3.up, Vector3.zero);

        private void Update() {
            if (!isBeingDragged)
                CheckMouseInput();
            else {
                UpdateDrag();
                if (Input.GetMouseButtonUp(0)) 
                    StopDrag();
            }
        }

        private void CheckMouseInput() {
            if (Input.GetMouseButtonDown(0)) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit[] raycastHits = Physics.RaycastAll(ray);

                if (RaycastHitsThisGameObject(raycastHits)) {
                    startPosition = Input.mousePosition;
                    StartDrag();
                }
                else 
                    StopDrag();
            }
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
                    transform.position = ray.GetPoint(distance1) + inputStartOffset;
                }
            }
        }

        private void StartDrag() {
            isBeingDragged = true;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance1 = 0f;
            if (horizontalPlane.Raycast(ray, out distance1)) {
                inputStartOffset = transform.position - ray.GetPoint(distance1);
            }

            if (dragSound != null)
                dragSound.Play();
        }

        public void StopDrag() {
            isBeingDragged = false;
            if (dragSound != null)
                dragSound.Stop();
        }
    }
}