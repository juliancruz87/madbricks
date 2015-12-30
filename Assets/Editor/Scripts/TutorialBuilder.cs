using UnityEngine;
using UnityEditor;
using System;

public class TutorialBuilder
{
    [MenuItem("Madbricks/Tutorials/Create Sequence")]
    public static void CreateTutorial()
    {
        CustomAssetUtility.CreateAsset<TutorialSequence>();
    }
}