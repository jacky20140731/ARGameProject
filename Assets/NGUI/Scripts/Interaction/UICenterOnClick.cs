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

/// <summary>
/// Attaching this script to an element of a scroll view will make it possible to center on it by clicking on it.
/// </summary>

[AddComponentMenu("NGUI/Interaction/Center Scroll View on Click")]
public class UICenterOnClick : MonoBehaviour
{
	void OnClick ()
	{
		UICenterOnChild center = NGUITools.FindInParents<UICenterOnChild>(gameObject);
		UIPanel panel = NGUITools.FindInParents<UIPanel>(gameObject);

		if (center != null)
		{
			if (center.enabled)
				center.CenterOn(transform);
		}
		else if (panel != null && panel.clipping != UIDrawCall.Clipping.None)
		{
			UIScrollView sv = panel.GetComponent<UIScrollView>();
			Vector3 offset = -panel.cachedTransform.InverseTransformPoint(transform.position);
			if (!sv.canMoveHorizontally) offset.x = panel.cachedTransform.localPosition.x;
			if (!sv.canMoveVertically) offset.y = panel.cachedTransform.localPosition.y;
			SpringPanel.Begin(panel.cachedGameObject, offset, 6f);
		}
	}
}
