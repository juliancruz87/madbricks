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

    void Awake ()
    {
        PlayerPrefs.SetString(PrefsProperties.CLEARED_AREA, "3");
        PlayerPrefs.SetString(PrefsProperties.CLEARED_LEVEL, "2");

        DisableLevels();
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

    public void EnableLevels(int levelAmount)
    {
        Debug.Log("Enabling levels " + levelAmount);
        for (int i = 0; i < levelAmount; i++)
        {
            Transform level = transform.GetChild(i+1);
           // if (level.GetComponent<TextMesh>() == null)
            {
                Debug.Log("Enabling level " + (i+1)); 
                level.GetComponent<Renderer>().material = materialArea;
            }
        }
    }

    public void DisableLevels()
    {
        Material defaultMaterial = transform.parent.GetComponent<AreaBoxesManager>().MaterialDefault;
        Debug.Log("Disable levels ");

        for (int i = 1; i < 4; i++)
        {
            Transform level = transform.GetChild(i);
            if (level.GetComponent<TextMesh>() == null)
            {
                level.GetComponent<Renderer>().material = defaultMaterial;
            }
        }
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

   
}
