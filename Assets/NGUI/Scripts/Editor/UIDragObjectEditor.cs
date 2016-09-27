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
[CustomEditor(typeof(UIDragObject))]
public class UIDragObjectEditor : Editor
{
	public override void OnInspectorGUI ()
	{
		GUILayout.Space(6f);
		serializedObject.Update();

		NGUIEditorTools.SetLabelWidth(100f);

		SerializedProperty sp = NGUIEditorTools.DrawProperty("Target", serializedObject, "target");

		EditorGUI.BeginDisabledGroup(sp.objectReferenceValue == null);
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label("Movement", GUILayout.Width(78f));
			NGUIEditorTools.DrawPaddedProperty("", serializedObject, "scale");
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			GUILayout.Label("Scroll Wheel", GUILayout.Width(78f));
			NGUIEditorTools.DrawPaddedProperty("", serializedObject, "scrollMomentum");
			GUILayout.EndHorizontal();

			sp = NGUIEditorTools.DrawPaddedProperty("Drag Effect", serializedObject, "dragEffect");

			if (sp.hasMultipleDifferentValues || (UIDragObject.DragEffect)sp.intValue != UIDragObject.DragEffect.None)
			{
				NGUIEditorTools.DrawProperty("  Momentum", serializedObject, "momentumAmount", GUILayout.Width(140f));
			}

			sp = NGUIEditorTools.DrawProperty("Keep Visible", serializedObject, "restrictWithinPanel");

			if (sp.hasMultipleDifferentValues || sp.boolValue)
			{
				NGUIEditorTools.DrawProperty("  Content Rect", serializedObject, "contentRect");
				NGUIEditorTools.DrawProperty("  Panel Region", serializedObject, "panelRegion");
			}
		}
		EditorGUI.EndDisabledGroup();

		serializedObject.ApplyModifiedProperties();
	}
}
