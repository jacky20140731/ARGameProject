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
using System.Collections;

/// <summary>
/// Allows dragging of the specified scroll view by mouse or touch.
/// </summary>

[AddComponentMenu("NGUI/Interaction/Drag Scroll View")]
public class UIDragScrollView : MonoBehaviour
{
	/// <summary>
	/// Reference to the scroll view that will be dragged by the script.
	/// </summary>

	public UIScrollView scrollView;

	// Legacy functionality, kept for backwards compatibility. Use 'scrollView' instead.
	[HideInInspector][SerializeField] UIScrollView draggablePanel;

	Transform mTrans;
	UIScrollView mScroll;
	bool mAutoFind = false;
	bool mStarted = false;

	/// <summary>
	/// Automatically find the scroll view if possible.
	/// </summary>

	void OnEnable ()
	{
		mTrans = transform;

		// Auto-upgrade
		if (scrollView == null && draggablePanel != null)
		{
			scrollView = draggablePanel;
			draggablePanel = null;
		}

		if (mStarted && (mAutoFind || mScroll == null))
			FindScrollView();
	}

	/// <summary>
	/// Find the scroll view.
	/// </summary>

	void Start ()
	{
		mStarted = true;
		FindScrollView();
	}

	/// <summary>
	/// Find the scroll view to work with.
	/// </summary>

	void FindScrollView ()
	{
		// If the scroll view is on a parent, don't try to remember it (as we want it to be dynamic in case of re-parenting)
		UIScrollView sv = NGUITools.FindInParents<UIScrollView>(mTrans);

		if (scrollView == null || (mAutoFind && sv != scrollView))
		{
			scrollView = sv;
			mAutoFind = true;
		}
		else if (scrollView == sv)
		{
			mAutoFind = true;
		}
		mScroll = scrollView;
	}

	/// <summary>
	/// Create a plane on which we will be performing the dragging.
	/// </summary>

	void OnPress (bool pressed)
	{
		// If the scroll view has been set manually, don't try to find it again
		if (mAutoFind && mScroll != scrollView)
		{
			mScroll = scrollView;
			mAutoFind = false;
		}

		if (scrollView && enabled && NGUITools.GetActive(gameObject))
		{
			scrollView.Press(pressed);
			
			if (!pressed && mAutoFind)
			{
				scrollView = NGUITools.FindInParents<UIScrollView>(mTrans);
				mScroll = scrollView;
			}
		}
	}

	/// <summary>
	/// Drag the object along the plane.
	/// </summary>

	void OnDrag (Vector2 delta)
	{
		if (scrollView && NGUITools.GetActive(this))
			scrollView.Drag();
	}

	/// <summary>
	/// If the object should support the scroll wheel, do it.
	/// </summary>

	void OnScroll (float delta)
	{
		if (scrollView && NGUITools.GetActive(this))
			scrollView.Scroll(delta);
	}

	/// <summary>
	/// Pan the scroll view.
	/// </summary>

	public void OnPan (Vector2 delta)
	{
		if (scrollView && NGUITools.GetActive(this))
			scrollView.OnPan(delta);
	}
}
