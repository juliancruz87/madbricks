using UnityEngine;
using System.Collections;

public class AreaBoxesManager : MonoBehaviour
{

    [SerializeField]
    private Transform mainCameraTransform;

    [SerializeField]
    private Material materialDefault;
    public Material MaterialDefault
    {
        get { return materialDefault; }
    }


    void Start()
    {
        EnableAreas();
    }

    void Update()
    {

    }

    public void EnableAreas()
    {
        int clearedArea = int.Parse(SaveManager.Instance.GetClearedArea());
        int clearedLevel = int.Parse(SaveManager.Instance.GetClearedLevel());

        for (int i = 0; i < clearedArea; i++)
        {
            Transform area = transform.GetChild(i + 1);

            AreaBox areaBox;
            if ((areaBox = area.GetComponent<AreaBox>()) != null)
            {
                Debug.Log("Enabling area " + (i + 1));

                if (i >= clearedArea - 1)
                    areaBox.EnableLevels(clearedLevel);
                else
                    areaBox.EnableLevels(3);
            }
        }
    }

    public void DisableAreas()
    {
        for (int i = 0; i < 6; i++)
        {
            Transform area = transform.GetChild(i + 1);

            AreaBox areaBox;
            if ((areaBox = area.GetComponent<AreaBox>()) != null)
            {
                areaBox.DisableLevels();
            }
        }
    }

    public void ResetAreaBoxes()
    {
        DisableAreas();
        EnableAreas();
    }
}
