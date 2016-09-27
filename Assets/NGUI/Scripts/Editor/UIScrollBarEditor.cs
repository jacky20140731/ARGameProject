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
[CustomEditor(typeof(UIScrollBar))]
public class UIScrollBarEditor : UIProgressBarEditor
{
	protected override void DrawLegacyFields ()
	{
		UIScrollBar sb = target as UIScrollBar;

		float val = EditorGUILayout.Slider("Value", sb.value, 0f, 1f);
		float size = EditorGUILayout.Slider("Size", sb.barSize, 0f, 1f);
		float alpha = EditorGUILayout.Slider("Alpha", sb.alpha, 0f, 1f);

		if (sb.value != val ||
			sb.barSize != size ||
			sb.alpha != alpha)
		{
			NGUIEditorTools.RegisterUndo("Scroll Bar Change", sb);
			sb.value = val;
			sb.barSize = size;
			sb.alpha = alpha;
			NGUITools.SetDirty(sb);

			for (int i = 0; i < UIScrollView.list.size; ++i)
			{
				UIScrollView sv = UIScrollView.list[i];

				if (sv.horizontalScrollBar == sb || sv.verticalScrollBar == sb)
				{
					NGUIEditorTools.RegisterUndo("Scroll Bar Change", sv);
					sv.UpdatePosition();
				}
			}
		}
	}
}
