/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2015 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(UIColorPicker))]
public class UIColorPickerEditor : Editor
{
	public override void OnInspectorGUI ()
	{
		serializedObject.Update();

		NGUIEditorTools.SetLabelWidth(100f);
		UIColorPicker picker = target as UIColorPicker;

		GUILayout.Space(6f);
		GUI.changed = false;

		NGUIEditorTools.DrawProperty(serializedObject, "value");
		NGUIEditorTools.DrawProperty(serializedObject, "selectionWidget");

		GUILayout.Space(6f);
		GUI.changed = false;

		NGUIEditorTools.DrawEvents("On Value Change", picker, picker.onChange);
		serializedObject.ApplyModifiedProperties();
	}
}
