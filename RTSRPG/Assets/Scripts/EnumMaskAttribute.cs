/*
 Written by: Lucas Antunes (aka ItsaMeTuni), lucasba8@gmail.com
 In: 2/15/2018
 The only thing that you cannot do with this script is sell it by itself without substantially modifying it.
 */

using UnityEditor;

#if UNITY_EDITOR
using UnityEngine;

[CustomPropertyDrawer(typeof(EnumMaskAttribute))]
public class EnumMaskPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginChangeCheck();
        uint a = (uint)(EditorGUI.MaskField(position, label, property.intValue, property.enumNames));
        if (EditorGUI.EndChangeCheck())
        {
            property.intValue = (int)a;
        }
    }
}
#endif

public class EnumMaskAttribute : PropertyAttribute
{
}
