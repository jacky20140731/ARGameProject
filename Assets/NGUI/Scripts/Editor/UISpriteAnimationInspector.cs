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

/// <summary>
/// Inspector class used to edit UISpriteAnimations.
/// </summary>

[CanEditMultipleObjects]
[CustomEditor(typeof(UISpriteAnimation))]
public class UISpriteAnimationInspector : Editor
{
	/// <summary>
	/// Draw the inspector widget.
	/// </summary>

	public override void OnInspectorGUI ()
	{
		GUILayout.Space(3f);
		NGUIEditorTools.SetLabelWidth(80f);
		serializedObject.Update();

		NGUIEditorTools.DrawProperty("Framerate", serializedObject, "mFPS");
		NGUIEditorTools.DrawProperty("Name Prefix", serializedObject, "mPrefix");
		NGUIEditorTools.DrawProperty("Loop", serializedObject, "mLoop");
		NGUIEditorTools.DrawProperty("Pixel Snap", serializedObject, "mSnap");

		serializedObject.ApplyModifiedProperties();
	}
}
