using UnityEngine;
using UnityEngine.UI;

namespace Interactive.Detail
{
    public class GuardianSelector : MonoBehaviour 
	{
        [SerializeField]
        private Sprite[] images;

        [SerializeField]
        private Image animatedGuardian;

        private void Start()
        {
            int area = GameManager.Instance.levelInfo.area;

            if (area == 0)
                animatedGuardian.enabled = false; 
            else
                animatedGuardian.sprite = images[area - 1];
        }

    }
}