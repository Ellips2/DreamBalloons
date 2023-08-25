#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ColorChanger))]
public class ColorChangerGUI : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ColorChanger cc = (ColorChanger)target;
        if (GUILayout.Button("Delete self in children", GUILayout.Width(150)))
        {
            cc.DeleteSelfInChildren();
        }
    }
}
#endif
