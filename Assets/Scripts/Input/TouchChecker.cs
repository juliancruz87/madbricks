﻿using UnityEngine;
using System.Collections;

namespace ManagerInput
{
    public static class TouchChecker
    {
        private static ITouchInfo TouchInfo
        {
            get { return InputManager.Instance.InputDevice.PrimaryTouch; }
        }

        public static bool WasTappingFromCamera(Camera cam, Collider collider, bool hasToCheckUI = false)
        {
            if (!TouchInfo.ReleasedTapThisFrame)
                return false;

            return CheckTouchingFromCamera(cam, collider, true, hasToCheckUI);
        }

        public static bool IsTouchingFromCamera(Camera cam, Collider collider, bool usePreviousPosition = true, bool hasToCheckUI = false)
        {
            if (!TouchInfo.IsTouching)
                return false;

            return CheckTouchingFromCamera(cam, collider, usePreviousPosition, hasToCheckUI);
        }

        private static bool CheckTouchingFromCamera(Camera cam, Collider collider, bool usePreviousPosition, bool hasToCheckUI)
        {
			return InternalIsTouchingFromCamera (cam, collider, usePreviousPosition);
		}

        private static bool InternalIsTouchingFromCamera(Camera cam, Collider collider, bool usePreviousPosition)
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = cam.ScreenPointToRay(GetTouchPosition(usePreviousPosition));
            Physics.Raycast(ray, out hit);
            return hit.collider == collider;
        }

        public static bool WasTappingFromCollider(Camera cam, Collider collider, bool hasToCheckUI = false)
        {
            if (!TouchInfo.ReleasedTapThisFrame)
                return false;

            return CheckTouchingFromCollider(cam, collider, true, hasToCheckUI);
        }

        public static bool IsTouchingFromCollider(Camera cam, Collider collider, bool usePreviousPosition = true, bool hasToCheckUI = false)
        {
            if (!TouchInfo.IsTouching)
                return false;

            return CheckTouchingFromCollider(cam, collider, usePreviousPosition, hasToCheckUI);
        }

        private static bool CheckTouchingFromCollider(Camera cam, Collider collider, bool usePreviousPosition, bool hasToCheckUI)
        {
           	return InternalIsTouchingFromCollider(cam, collider, usePreviousPosition);
        }

        private static bool InternalIsTouchingFromCollider(Camera cam, Collider collider, bool usePreviousPosition)
        {
            Ray ray = cam.ScreenPointToRay(GetTouchPosition(usePreviousPosition));
            RaycastHit hit = new RaycastHit();
            return collider.Raycast(ray, out hit, 1000.0f);
        }

        private static Vector3 GetTouchPosition(bool usePreviousPosition)
        {
            if (usePreviousPosition)
                return TouchInfo.PreviousTouchPosition;
            else
                return TouchInfo.StartTouchPosition;
        }
    }
}