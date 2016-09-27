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
/// Inspector class used to view UIDrawCalls.
/// </summary>

[CustomEditor(typeof(UIDrawCall))]
public class UIDrawCallInspector : Editor
{
	/// <summary>
	/// Draw the inspector widget.
	/// </summary>

	public override void OnInspectorGUI ()
	{
		if (Event.current.type == EventType.Repaint || Event.current.type == EventType.Layout)
		{
			UIDrawCall dc = target as UIDrawCall;

			if (dc.manager != null)
			{
				EditorGUILayout.LabelField("Render Queue", dc.renderQueue.ToString());
				EditorGUILayout.LabelField("Owner Panel", NGUITools.GetHierarchy(dc.manager.gameObject));
				EditorGUILayout.LabelField("Triangles", dc.triangles.ToString());
			}
			else if (Event.current.type == EventType.Repaint)
			{
				Debug.LogWarning("Orphaned UIDrawCall detected!\nUse [Selection -> Force Delete] to get rid of it.");
			}
		}
	}
}
