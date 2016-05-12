using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AreaSelectionManager : MonoBehaviour {

    [SerializeField]
    private int selectedArea;

    [SerializeField]
    private float growScale;

    [SerializeField]
    private float growTime;

    private Transform selectionTransform;
    private Transform exitTransform;

    public Camera mainCamera;
    public LayerMask selectionLayer;

    void Start () {
	
	}

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000, selectionLayer))
            {
                Debug.Log(hit.collider.name);
                LoadSelectedLevel();
            }
        }
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
            int clearedArea = int.Parse(SaveManager.Instance.GetClearedArea());
            if (clearedArea >= selectedArea)
            {
                SaveManager.Instance.SetSelectedArea(selectedArea + "");
                //Application.LoadLevel(SceneProperties.SCENE_LOADER_AREA);
                StartCoroutine(LoadAreaCoroutine());
            }
        }
    }

    public IEnumerator LoadAreaCoroutine()
    {
        FadeManager.Instance.FadeOut();
        yield return new WaitForSeconds(1.88f);
		SceneManager.LoadScene(SceneProperties.SCENE_LOADER_AREA);
    }


}
