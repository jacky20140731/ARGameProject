/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;

/// <summary>
/// Attach to a game object to make its position always lag behind its parent as the parent moves.
/// </summary>

public class LagPosition : MonoBehaviour
{
	public Vector3 speed = new Vector3(10f, 10f, 10f);
	public bool ignoreTimeScale = false;

	Transform mTrans;
	Vector3 mRelative;
	Vector3 mAbsolute;
	bool mStarted = false;

	public void OnRepositionEnd ()
	{
		Interpolate(1000f);
	}

	void Interpolate (float delta)
	{
		Transform parent = mTrans.parent;

		if (parent != null)
		{
			Vector3 target = parent.position + parent.rotation * mRelative;
			mAbsolute.x = Mathf.Lerp(mAbsolute.x, target.x, Mathf.Clamp01(delta * speed.x));
			mAbsolute.y = Mathf.Lerp(mAbsolute.y, target.y, Mathf.Clamp01(delta * speed.y));
			mAbsolute.z = Mathf.Lerp(mAbsolute.z, target.z, Mathf.Clamp01(delta * speed.z));
			mTrans.position = mAbsolute;
		}
	}

	void Awake () { mTrans = transform; }
	void OnEnable () { if (mStarted) ResetPosition(); }
	void Start () { mStarted = true; ResetPosition(); }

	public void ResetPosition ()
	{
		mAbsolute = mTrans.position;
		mRelative = mTrans.localPosition;
	}

	void Update ()
	{
		Interpolate(ignoreTimeScale ? RealTime.deltaTime : Time.deltaTime);
	}
}
