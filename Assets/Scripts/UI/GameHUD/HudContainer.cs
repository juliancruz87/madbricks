using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HudContainer : MonoBehaviour {

    [SerializeField]
    private Button buttonRestart;
    [SerializeField]
    private Button buttonMenu;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void HideButtons()
    {
        buttonRestart.gameObject.SetActive(false);
        buttonMenu.gameObject.SetActive(false);

    }
}
