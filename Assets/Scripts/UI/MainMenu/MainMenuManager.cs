using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class MainMenuManager : MonoBehaviour {

    public Text textLevel;
    public bool isMenuActive;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
