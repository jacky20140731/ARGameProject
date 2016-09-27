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
/// Tween the object's rotation.
/// </summary>

[AddComponentMenu("NGUI/Tween/Tween Rotation")]
public class TweenRotation : UITweener
{
	public Vector3 from;
	public Vector3 to;
	public bool quaternionLerp = false;

	Transform mTrans;

	public Transform cachedTransform { get { if (mTrans == null) mTrans = transform; return mTrans; } }

	[System.Obsolete("Use 'value' instead")]
	public Quaternion rotation { get { return this.value; } set { this.value = value; } }

	/// <summary>
	/// Tween's current value.
	/// </summary>

	public Quaternion value { get { return cachedTransform.localRotation; } set { cachedTransform.localRotation = value; } }

	/// <summary>
	/// Tween the value.
	/// </summary>

	protected override void OnUpdate (float factor, bool isFinished)
	{
		value = quaternionLerp ? Quaternion.Slerp(Quaternion.Euler(from), Quaternion.Euler(to), factor) :
			Quaternion.Euler(new Vector3(
			Mathf.Lerp(from.x, to.x, factor),
			Mathf.Lerp(from.y, to.y, factor),
			Mathf.Lerp(from.z, to.z, factor)));
	}

	/// <summary>
	/// Start the tweening operation.
	/// </summary>

	static public TweenRotation Begin (GameObject go, float duration, Quaternion rot)
	{
		TweenRotation comp = UITweener.Begin<TweenRotation>(go, duration);
		comp.from = comp.value.eulerAngles;
		comp.to = rot.eulerAngles;

		if (duration <= 0f)
		{
			comp.Sample(1f, true);
			comp.enabled = false;
		}
		return comp;
	}

	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue () { from = value.eulerAngles; }

	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue () { to = value.eulerAngles; }

	[ContextMenu("Assume value of 'From'")]
	void SetCurrentValueToStart () { value = Quaternion.Euler(from); }

	[ContextMenu("Assume value of 'To'")]
	void SetCurrentValueToEnd () { value = Quaternion.Euler(to); }
}
