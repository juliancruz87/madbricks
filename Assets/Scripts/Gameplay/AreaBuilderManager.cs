using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AreaBuilderManager : MonoBehaviour {

    public Text labelAreaNumber;

	// Use this for initialization
	void Start ()
    {
        SetAreaNumber();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadMenu()
    {
        StartCoroutine(LoadMenuCoroutine());
    }

    public IEnumerator LoadMenuCoroutine()
    {
        FadeManager.Instance.FadeOut();
        yield return new WaitForSeconds(1.88f);
		SceneManager.LoadScene(SceneProperties.SCENE_MAIN_MENU);
    }

    public void SetAreaNumber()
    {
        labelAreaNumber.text = SaveManager.Instance.GetSelectedArea();
    }
}
