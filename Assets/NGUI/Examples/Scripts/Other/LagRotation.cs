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
/// Attach to a game object to make its rotation always lag behind its parent as the parent rotates.
/// </summary>

[AddComponentMenu("NGUI/Examples/Lag Rotation")]
public class LagRotation : MonoBehaviour
{
	public float speed = 10f;
	public bool ignoreTimeScale = false;

	Transform mTrans;
	Quaternion mRelative;
	Quaternion mAbsolute;

	public void OnRepositionEnd ()
	{
		Interpolate(1000f);
	}

	void Interpolate (float delta)
	{
		if (mTrans != null)
		{
			Transform parent = mTrans.parent;

			if (parent != null)
			{
				mAbsolute = Quaternion.Slerp(mAbsolute, parent.rotation * mRelative, delta * speed);
				mTrans.rotation = mAbsolute;
			}
		}
	}

	void Start ()
	{
		mTrans = transform;
		mRelative = mTrans.localRotation;
		mAbsolute = mTrans.rotation;
	}

	void Update ()
	{
		Interpolate(ignoreTimeScale ? RealTime.deltaTime : Time.deltaTime);
	}
}
