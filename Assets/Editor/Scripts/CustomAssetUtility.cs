using UnityEngine;
using UnityEditor;
using System.IO;

public static class CustomAssetUtility
{
    public static void CreateAsset<T> () where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T> ();


        string path = "Assets/Resources/Tutorials"; 
        //string path = AssetDatabase.GetAssetPath (Selection.activeObject);
        /*if (path == "") 
        {
           path = "Assets/Resources/Tutorials";
        } 
       else if (Path.GetExtension (path) != "") 
        {
           // path = path.Replace (Path.GetFileName (AssetDatabase.GetAssetPath (Selection.activeObject)), "");
        }*/
        
        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath (path + "/Tutorial_T.asset");
        
        AssetDatabase.CreateAsset (asset, assetPathAndName);
        
        AssetDatabase.SaveAssets ();
        EditorUtility.FocusProjectWindow ();
        Selection.activeObject = asset;
    }
}
