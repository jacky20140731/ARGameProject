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
/// Simple example script of how a button can be offset visibly when the mouse hovers over it or it gets pressed.
/// </summary>

[AddComponentMenu("NGUI/Interaction/Button Offset")]
public class UIButtonOffset : MonoBehaviour
{
	public Transform tweenTarget;
	public Vector3 hover = Vector3.zero;
	public Vector3 pressed = new Vector3(2f, -2f);
	public float duration = 0.2f;

	[System.NonSerialized] Vector3 mPos;
	[System.NonSerialized] bool mStarted = false;
	[System.NonSerialized] bool mPressed = false;

	void Start ()
	{
		if (!mStarted)
		{
			mStarted = true;
			if (tweenTarget == null) tweenTarget = transform;
			mPos = tweenTarget.localPosition;
		}
	}

	void OnEnable () { if (mStarted) OnHover(UICamera.IsHighlighted(gameObject)); }

	void OnDisable ()
	{
		if (mStarted && tweenTarget != null)
		{
			TweenPosition tc = tweenTarget.GetComponent<TweenPosition>();

			if (tc != null)
			{
				tc.value = mPos;
				tc.enabled = false;
			}
		}
	}

	void OnPress (bool isPressed)
	{
		mPressed = isPressed;

		if (enabled)
		{
			if (!mStarted) Start();
			TweenPosition.Begin(tweenTarget.gameObject, duration, isPressed ? mPos + pressed :
				(UICamera.IsHighlighted(gameObject) ? mPos + hover : mPos)).method = UITweener.Method.EaseInOut;
		}
	}

	void OnHover (bool isOver)
	{
		if (enabled)
		{
			if (!mStarted) Start();
			TweenPosition.Begin(tweenTarget.gameObject, duration, isOver ? mPos + hover : mPos).method = UITweener.Method.EaseInOut;
		}
	}

	void OnDragOver ()
	{
		if (mPressed) TweenPosition.Begin(tweenTarget.gameObject, duration, mPos + hover).method = UITweener.Method.EaseInOut;
	}

	void OnDragOut ()
	{
		if (mPressed) TweenPosition.Begin(tweenTarget.gameObject, duration, mPos).method = UITweener.Method.EaseInOut;
	}

	void OnSelect (bool isSelected)
	{
		if (enabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
			OnHover(isSelected);
	}
}
