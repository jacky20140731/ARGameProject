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
/// Tween the object's color.
/// </summary>

[AddComponentMenu("NGUI/Tween/Tween Color")]
public class TweenColor : UITweener
{
	public Color from = Color.white;
	public Color to = Color.white;

	bool mCached = false;
	UIWidget mWidget;
	Material mMat;
	Light mLight;
	SpriteRenderer mSr;

	void Cache ()
	{
		mCached = true;
		mWidget = GetComponent<UIWidget>();
		if (mWidget != null) return;

		mSr = GetComponent<SpriteRenderer>();
		if (mSr != null) return;

#if UNITY_4_3 || UNITY_4_5 || UNITY_4_6
		Renderer ren = renderer;
#else
		Renderer ren = GetComponent<Renderer>();
#endif
		if (ren != null)
		{
			mMat = ren.material;
			return;
		}

#if UNITY_4_3 || UNITY_4_5 || UNITY_4_6
		mLight = light;
#else
		mLight = GetComponent<Light>();
#endif
		if (mLight == null) mWidget = GetComponentInChildren<UIWidget>();
	}

	[System.Obsolete("Use 'value' instead")]
	public Color color { get { return this.value; } set { this.value = value; } }

	/// <summary>
	/// Tween's current value.
	/// </summary>

	public Color value
	{
		get
		{
			if (!mCached) Cache();
			if (mWidget != null) return mWidget.color;
			if (mMat != null) return mMat.color;
			if (mSr != null) return mSr.color;
			if (mLight != null) return mLight.color;
			return Color.black;
		}
		set
		{
			if (!mCached) Cache();
			if (mWidget != null) mWidget.color = value;
			else if (mMat != null) mMat.color = value;
			else if (mSr != null) mSr.color = value;
			else if (mLight != null)
			{
				mLight.color = value;
				mLight.enabled = (value.r + value.g + value.b) > 0.01f;
			}
		}
	}

	/// <summary>
	/// Tween the value.
	/// </summary>

	protected override void OnUpdate (float factor, bool isFinished) { value = Color.Lerp(from, to, factor); }

	/// <summary>
	/// Start the tweening operation.
	/// </summary>

	static public TweenColor Begin (GameObject go, float duration, Color color)
	{
#if UNITY_EDITOR
		if (!Application.isPlaying) return null;
#endif
		TweenColor comp = UITweener.Begin<TweenColor>(go, duration);
		comp.from = comp.value;
		comp.to = color;

		if (duration <= 0f)
		{
			comp.Sample(1f, true);
			comp.enabled = false;
		}
		return comp;
	}

	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue () { from = value; }

	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue () { to = value; }

	[ContextMenu("Assume value of 'From'")]
	void SetCurrentValueToStart () { value = from; }

	[ContextMenu("Assume value of 'To'")]
	void SetCurrentValueToEnd () { value = to; }
}
