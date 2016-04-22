using UI;
using UnityEditor;
using UnityEngine;
using TextEditor = UnityEditor.UI.TextEditor;

[CustomEditor(typeof(TextWithIcon))]
public class TextWithIconEditor : TextEditor
{

    private SerializedProperty ImageScalingFactorProp;

    protected override void OnEnable()
    {
        base.OnEnable();
        ImageScalingFactorProp = serializedObject.FindProperty("ImageScalingFactor");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.PropertyField(ImageScalingFactorProp, new GUIContent("Image Scaling Factor"));
        serializedObject.ApplyModifiedProperties();
    }
}
