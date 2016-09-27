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
/// Simple example script of how a button can be rotated visibly when the mouse hovers over it or it gets pressed.
/// </summary>

[AddComponentMenu("NGUI/Interaction/Button Rotation")]
public class UIButtonRotation : MonoBehaviour
{
	public Transform tweenTarget;
	public Vector3 hover = Vector3.zero;
	public Vector3 pressed = Vector3.zero;
	public float duration = 0.2f;

	Quaternion mRot;
	bool mStarted = false;

	void Start ()
	{
		if (!mStarted)
		{
			mStarted = true;
			if (tweenTarget == null) tweenTarget = transform;
			mRot = tweenTarget.localRotation;
		}
	}

	void OnEnable () { if (mStarted) OnHover(UICamera.IsHighlighted(gameObject)); }

	void OnDisable ()
	{
		if (mStarted && tweenTarget != null)
		{
			TweenRotation tc = tweenTarget.GetComponent<TweenRotation>();

			if (tc != null)
			{
				tc.value = mRot;
				tc.enabled = false;
			}
		}
	}

	void OnPress (bool isPressed)
	{
		if (enabled)
		{
			if (!mStarted) Start();
			TweenRotation.Begin(tweenTarget.gameObject, duration, isPressed ? mRot * Quaternion.Euler(pressed) :
				(UICamera.IsHighlighted(gameObject) ? mRot * Quaternion.Euler(hover) : mRot)).method = UITweener.Method.EaseInOut;
		}
	}

	void OnHover (bool isOver)
	{
		if (enabled)
		{
			if (!mStarted) Start();
			TweenRotation.Begin(tweenTarget.gameObject, duration, isOver ? mRot * Quaternion.Euler(hover) :
				mRot).method = UITweener.Method.EaseInOut;
		}
	}

	void OnSelect (bool isSelected)
	{
		if (enabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
			OnHover(isSelected);
	}
}
