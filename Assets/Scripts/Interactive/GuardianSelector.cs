using UnityEngine;
using UnityEngine.UI;

namespace Interactive.Detail
{
    public class GuardianSelector : MonoBehaviour 
	{
        [SerializeField]
        private Sprite[] guardians;

        [SerializeField]
        private Image[] images;

        private void Start()
        {
            int area = GameManager.Instance.levelInfo.area;

            for (int i = 0; i < images.Length; i++)
            {
                if (area == 0)
                    images[i].enabled = false;
                else
                    images[i].sprite = guardians[area - 1];
            }
        }

    }
}