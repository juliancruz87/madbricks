using UnityEngine;
using System.Collections;

public class AreaSelectionObject : MonoBehaviour {

    private string areaID;

    void Start()
    {
        int nameLength = transform.parent.name.Length;
        areaID = transform.parent.name.Substring(nameLength - 1, 1);
    }

    void OnMouseDown()
    {
        Debug.Log("Selected Area: " + areaID);
        SaveManager.Instance.SetSelectedArea(areaID);
        Application.LoadLevel(SceneProperties.SCENE_LOADER_AREA);
    }
}
