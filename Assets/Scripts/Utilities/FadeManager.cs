using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class FadeManager : MonoBehaviour {

    public delegate void OnFadeOutEvent();
    public event OnFadeOutEvent OnFadeOut;

    private static FadeManager instance;
    public static FadeManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<FadeManager>();
            return instance;
        }
    }
 

    [SerializeField]
    private Image fadeOutImage;
    [SerializeField]
    private Image fadeInImage;
    [SerializeField]
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        Color fadeInColor = fadeInImage.color;
        fadeInImage.color = new Color(fadeInColor.r, fadeInColor.g, fadeInColor.b, 1) ;
        FadeIn();
    }

	void Start ()
    {
        
	}
	
	void Update () 
    {
        
	}

    public void FadeIn()
    {
        animator.SetTrigger("fadeIn");
    }

    public void FadeOut()
    {
        animator.SetTrigger("fadeOut");
    }

    public void OnFinishFadeOut()
    {
        OnFadeOut();
    }
}
