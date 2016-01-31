using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class MainMenuManager : MonoBehaviour {

    public Text textLevel;
    public bool isMenuActive;
    public Camera mainCamera;
    public LayerMask selectLayer;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000, selectLayer))
            {
                Debug.Log(hit.collider.name);
            }
        }
    }

    public void StartGame()
    {
        int level = 1;
        if(int.TryParse(textLevel.text, out level))
            Application.LoadLevel("Level " + level);
        else
            Application.LoadLevel("Level " + 1);
    }

    public void StartLevel(int level)
    {
        Application.LoadLevel("Level " + level);
    }

    public void SwitchMenuState()
    {
        isMenuActive = !isMenuActive;
    }
}
