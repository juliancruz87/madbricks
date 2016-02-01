using UnityEngine;
using System.Collections;

public class AreaBox : MonoBehaviour
{

    [SerializeField]
    private int area;
    public int Area
    {
        get
        {
            return area;
        }

    }

    [SerializeField]
    private Material materialArea;
    private Transform numberTransform;

    [SerializeField]
    private bool isSelected;
    public bool IsSelected
    {
        get { return isSelected; }
        set { isSelected = value; }
    }


    void Start()
    {
        numberTransform = transform.GetChild(0);
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 360 - transform.parent.rotation.y, 0);
        numberTransform.rotation = Quaternion.Euler(43, 45, 0);
    }

    public void Grow(float growScale, float growTime)
    {
        StopCoroutine("ShrinkCoroutine");
        StartCoroutine(GrowCoroutine(growScale, growTime));
    }

    public void Shrink(float growScale, float growTime)
    {
        StopCoroutine("GrowCoroutine");
        StartCoroutine(ShrinkCoroutine(growScale, growTime));
    }

    IEnumerator GrowCoroutine(float growScale, float growTime)
    {
        Vector3 newScale = transform.localScale * growScale;
        float originalTime = growTime;

        while (growTime > 0.0f)
        {
            growTime -= Time.deltaTime;
            if (growTime < 0.0f)
            {
                growTime = 0f;
            }
            transform.localScale = Vector3.Lerp(transform.localScale, newScale, growTime / originalTime);
            yield return null;
        }
        transform.localScale = newScale;

    }

    IEnumerator ShrinkCoroutine(float growScale, float growTime)
    {
        Vector3 newScale = Vector3.one;
        float originalTime = growTime;

        while (growTime > 0.0f)
        {
            growTime -= Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, newScale, growTime / originalTime);
            yield return null;

        }


    }

    /*void OnMouseDown()
    {
        if (isSelected)
        {
            if (!FindObjectOfType<MainMenuManager>().isMenuActive)
            {
                Debug.Log("Selected Area: " + area);
                SaveManager.Instance.SetSelectedArea(area+"");
                Application.LoadLevel(SceneProperties.SCENE_LOADER_AREA);
            }
        }

    }*/
}
