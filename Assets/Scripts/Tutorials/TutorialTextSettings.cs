using System.Collections;
using UnityEngine;

public class TutorialTextSettings : ScriptableObject
{
	[SerializeField]
	private string startText;

    [SerializeField]
    private string idleText;

    [SerializeField]
    private string actionText;


    public string StartText
    {
        get { return startText; }
    }

    public string ActionText
    {
        get { return actionText; }
    }

    public string IdleText
    {
        get
        { return idleText; }
    }
}



