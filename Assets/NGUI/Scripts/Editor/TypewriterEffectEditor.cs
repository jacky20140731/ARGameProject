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
[CustomEditor(typeof(TypewriterEffect))]
public class TypewriterEffectEditor : Editor
{
	public override void OnInspectorGUI ()
	{
		GUILayout.Space(6f);
		NGUIEditorTools.SetLabelWidth(120f);

		serializedObject.Update();

		NGUIEditorTools.DrawProperty(serializedObject, "charsPerSecond");
		NGUIEditorTools.DrawProperty(serializedObject, "fadeInTime");
		NGUIEditorTools.DrawProperty(serializedObject, "delayOnPeriod");
		NGUIEditorTools.DrawProperty(serializedObject, "delayOnNewLine");
		NGUIEditorTools.DrawProperty(serializedObject, "scrollView");
		NGUIEditorTools.DrawProperty(serializedObject, "keepFullDimensions");

		TypewriterEffect tw = target as TypewriterEffect;
		NGUIEditorTools.DrawEvents("On Finished", tw, tw.onFinished);

		serializedObject.ApplyModifiedProperties();
	}
}
