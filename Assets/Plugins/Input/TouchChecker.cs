using UnityEngine;
using System.Collections;

namespace ManagerInput
{
    public static class TouchChecker
    {
		private static Collider collider;

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

		public static void SetLastColliderTouched (Collider myCollider)
		{
			if(TouchInfo.IsTouching && collider == null)
				collider = myCollider;
		}

		public static void ReleaseLastColliderTouched ()
		{
			collider = null;
		}

		public static bool NoHasColliderTouched ()
		{
			return collider == null;
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

        public static bool InputIsOverThisCollider(Camera cam, Collider someCollider) {
            Ray ray = cam.ScreenPointToRay(GetTouchPosition(true));
            RaycastHit[] hits = Physics.RaycastAll(ray);
            
            Collider firstCollider = null;
            float nearestDistance = float.MaxValue;
            foreach (RaycastHit hitinfo in hits) {
                float distance = Vector3.Distance(Camera.main.transform.position, hitinfo.collider.transform.position); 
                if (distance < nearestDistance) {
                    firstCollider = hitinfo.collider;
                    nearestDistance = distance;
                }
            }

            return firstCollider == someCollider;
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