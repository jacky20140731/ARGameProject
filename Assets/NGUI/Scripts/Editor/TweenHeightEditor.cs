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

[CustomEditor(typeof(TweenHeight))]
public class TweenHeightEditor : UITweenerEditor
{
	public override void OnInspectorGUI ()
	{
		GUILayout.Space(6f);
		NGUIEditorTools.SetLabelWidth(120f);

		TweenHeight tw = target as TweenHeight;
		GUI.changed = false;

		int from = EditorGUILayout.IntField("From", tw.from);
		int to = EditorGUILayout.IntField("To", tw.to);
		bool table = EditorGUILayout.Toggle("Update Table", tw.updateTable);

		if (from < 0) from = 0;
		if (to < 0) to = 0;

		if (GUI.changed)
		{
			NGUIEditorTools.RegisterUndo("Tween Change", tw);
			tw.from = from;
			tw.to = to;
			tw.updateTable = table;
			NGUITools.SetDirty(tw);
		}

		DrawCommonProperties();
	}
}
