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

[CustomEditor(typeof(UIScrollView))]
public class UIScrollViewEditor : Editor
{
	public override void OnInspectorGUI ()
	{
		NGUIEditorTools.SetLabelWidth(130f);

		GUILayout.Space(3f);
		serializedObject.Update();

		SerializedProperty sppv = serializedObject.FindProperty("contentPivot");
		UIWidget.Pivot before = (UIWidget.Pivot)sppv.intValue;

		NGUIEditorTools.DrawProperty("Content Origin", sppv, false);

		SerializedProperty sp = NGUIEditorTools.DrawProperty("Movement", serializedObject, "movement");

		if (((UIScrollView.Movement)sp.intValue) == UIScrollView.Movement.Custom)
		{
			NGUIEditorTools.SetLabelWidth(20f);

			GUILayout.BeginHorizontal();
			GUILayout.Space(114f);
			NGUIEditorTools.DrawProperty("X", serializedObject, "customMovement.x", GUILayout.MinWidth(20f));
			NGUIEditorTools.DrawProperty("Y", serializedObject, "customMovement.y", GUILayout.MinWidth(20f));
			GUILayout.EndHorizontal();
		}

		NGUIEditorTools.SetLabelWidth(130f);

		NGUIEditorTools.DrawProperty("Drag Effect", serializedObject, "dragEffect");
		NGUIEditorTools.DrawProperty("Scroll Wheel Factor", serializedObject, "scrollWheelFactor");
		NGUIEditorTools.DrawProperty("Momentum Amount", serializedObject, "momentumAmount");

		NGUIEditorTools.DrawProperty("Restrict Within Panel", serializedObject, "restrictWithinPanel");
		NGUIEditorTools.DrawProperty("Cancel Drag If Fits", serializedObject, "disableDragIfFits");
		NGUIEditorTools.DrawProperty("Smooth Drag Start", serializedObject, "smoothDragStart");
		NGUIEditorTools.DrawProperty("IOS Drag Emulation", serializedObject, "iOSDragEmulation");

		NGUIEditorTools.SetLabelWidth(100f);

		if (NGUIEditorTools.DrawHeader("Scroll Bars"))
		{
			NGUIEditorTools.BeginContents();
			NGUIEditorTools.DrawProperty("Horizontal", serializedObject, "horizontalScrollBar");
			NGUIEditorTools.DrawProperty("Vertical", serializedObject, "verticalScrollBar");
			NGUIEditorTools.DrawProperty("Show Condition", serializedObject, "showScrollBars");
			NGUIEditorTools.EndContents();
		}
		serializedObject.ApplyModifiedProperties();

		if (before != (UIWidget.Pivot)sppv.intValue)
		{
			(target as UIScrollView).ResetPosition();
		}
	}
}
