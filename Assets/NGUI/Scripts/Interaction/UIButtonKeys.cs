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
/// Deprecated component. Use UIKeyNavigation instead.
/// </summary>

[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Button Keys (Legacy)")]
public class UIButtonKeys : UIKeyNavigation
{
	public UIButtonKeys selectOnClick;
	public UIButtonKeys selectOnUp;
	public UIButtonKeys selectOnDown;
	public UIButtonKeys selectOnLeft;
	public UIButtonKeys selectOnRight;

	protected override void OnEnable ()
	{
		Upgrade();
		base.OnEnable();
	}

	public void Upgrade ()
	{
		if (onClick == null && selectOnClick != null)
		{
			onClick = selectOnClick.gameObject;
			selectOnClick = null;
			NGUITools.SetDirty(this);
		}

		if (onLeft == null && selectOnLeft != null)
		{
			onLeft = selectOnLeft.gameObject;
			selectOnLeft = null;
			NGUITools.SetDirty(this);
		}

		if (onRight == null && selectOnRight != null)
		{
			onRight = selectOnRight.gameObject;
			selectOnRight = null;
			NGUITools.SetDirty(this);
		}

		if (onUp == null && selectOnUp != null)
		{
			onUp = selectOnUp.gameObject;
			selectOnUp = null;
			NGUITools.SetDirty(this);
		}

		if (onDown == null && selectOnDown != null)
		{
			onDown = selectOnDown.gameObject;
			selectOnDown = null;
			NGUITools.SetDirty(this);
		}
	}
}
