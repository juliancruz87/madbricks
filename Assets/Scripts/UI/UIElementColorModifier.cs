using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIElementColorModifier : MonoBehaviour {

    public enum UIElementType
    {
        Image,
        Text
    }

    [SerializeField]
    private UIElementType elementType;

    [SerializeField]
    private Color colorToOverride;

    [SerializeField]

    void Start()
    {
        Color newColor = FindObjectOfType<GameUIManager>().GetLevelUIColor();

        if(elementType == UIElementType.Image)
        {
            GetComponent<Image>().color = newColor;
        }
        else if (elementType == UIElementType.Text)
        {
            GetComponent<Text>().color = newColor;
        }
    }
	
	void Update ()
    {
	
	}
}
