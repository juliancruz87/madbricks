using UnityEngine;
using System.Collections;

public class AreaSelectionManager : MonoBehaviour {

    [SerializeField]
    private int selectedArea;

    [SerializeField]
    private float growScale;

    [SerializeField]
    private float growTime;

    private Transform selectionTransform;
    private Transform exitTransform;

    void Start () {
	
	}
	
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.name.Contains("Area"))
        {
            selectionTransform = col.transform;
            Debug.Log("Selected: " + col.name);
            selectedArea = selectionTransform.GetComponent<AreaBox>().Area;
            selectionTransform.GetComponent<AreaBox>().IsSelected = true;
            //selectionTransform.GetComponent<AreaBox>().Grow(growScale, growTime);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.name.Contains("Area"))
        {
            selectionTransform = null;
            exitTransform = col.transform;
            Debug.Log("Unselected: " + col.name);
            exitTransform.GetComponent<AreaBox>().IsSelected = false;

            //exitTransform.GetComponent<AreaBox>().Shrink(growScale, growTime);
        }
    }

    public void LoadSelectedLevel()
    {
        if (!FindObjectOfType<MainMenuManager>().isMenuActive)
        {
            Debug.Log("Selected Area: " + selectedArea);
            SaveManager.Instance.SetSelectedArea(selectedArea + "");
            Application.LoadLevel(SceneProperties.SCENE_LOADER_AREA);
        }
    }

}
