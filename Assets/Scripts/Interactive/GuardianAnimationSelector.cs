using UnityEngine;
using UnityEngine.UI;

namespace Interactive.Detail
{
    public class GuardianAnimationSelector : MonoBehaviour 
	{
        [SerializeField]
        private GameObject[] guardians;

        private void Start()
        {
            int area = GameManager.Instance.levelInfo.area;

            if (area != 0)
            {
                GameObject guardian = Instantiate(guardians[area - 1]);
				guardian.transform.SetParent(gameObject.transform);
                guardian.transform.localPosition = Vector3.zero;
            }
        }

    }
}