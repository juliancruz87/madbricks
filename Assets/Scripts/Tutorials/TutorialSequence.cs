using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialSequence: ScriptableObject 
{
    public List<Sprite> tutorialPages;
#if !UNITY_ANDROID
    public MovieTexture movie;
#endif
}
