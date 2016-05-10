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
                ActivateAreaGuardian(area);

            }
        }

        private void ActivateAreaGuardian(int area)
        {
            for (int i = 0; i < guardians.Length; i++)
            {
                if (area == i + 1)
                    guardians[i].SetActive(true);
                else
                    guardians[i].SetActive(false);
            }
        }

    }
}