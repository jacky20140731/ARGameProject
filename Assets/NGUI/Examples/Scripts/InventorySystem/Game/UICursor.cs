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
// Copyright © 2011-2014 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Selectable sprite that follows the mouse.
/// </summary>

[RequireComponent(typeof(UISprite))]
[AddComponentMenu("NGUI/Examples/UI Cursor")]
public class UICursor : MonoBehaviour
{
	static public UICursor instance;

	// Camera used to draw this cursor
	public Camera uiCamera;

	Transform mTrans;
	UISprite mSprite;

	UIAtlas mAtlas;
	string mSpriteName;

	/// <summary>
	/// Keep an instance reference so this class can be easily found.
	/// </summary>

	void Awake () { instance = this; }
	void OnDestroy () { instance = null; }

	/// <summary>
	/// Cache the expected components and starting values.
	/// </summary>

	void Start ()
	{
		mTrans = transform;
		mSprite = GetComponentInChildren<UISprite>();
		
		if (uiCamera == null)
			uiCamera = NGUITools.FindCameraForLayer(gameObject.layer);
		
		if (mSprite != null)
		{
			mAtlas = mSprite.atlas;
			mSpriteName = mSprite.spriteName;
			if (mSprite.depth < 100) mSprite.depth = 100;
		}
	}

	/// <summary>
	/// Reposition the widget.
	/// </summary>

	void Update ()
	{
		Vector3 pos = Input.mousePosition;

		if (uiCamera != null)
		{
			// Since the screen can be of different than expected size, we want to convert
			// mouse coordinates to view space, then convert that to world position.
			pos.x = Mathf.Clamp01(pos.x / Screen.width);
			pos.y = Mathf.Clamp01(pos.y / Screen.height);
			mTrans.position = uiCamera.ViewportToWorldPoint(pos);

			// For pixel-perfect results
#if UNITY_4_3 || UNITY_4_5 || UNITY_4_6
			if (uiCamera.isOrthoGraphic)
#else
			if (uiCamera.orthographic)
#endif
			{
				Vector3 lp = mTrans.localPosition;
				lp.x = Mathf.Round(lp.x);
				lp.y = Mathf.Round(lp.y);
				mTrans.localPosition = lp;
			}
		}
		else
		{
			// Simple calculation that assumes that the camera is of fixed size
			pos.x -= Screen.width * 0.5f;
			pos.y -= Screen.height * 0.5f;
			pos.x = Mathf.Round(pos.x);
			pos.y = Mathf.Round(pos.y);
			mTrans.localPosition = pos;
		}
	}

	/// <summary>
	/// Clear the cursor back to its original value.
	/// </summary>

	static public void Clear ()
	{
		if (instance != null && instance.mSprite != null)
			Set(instance.mAtlas, instance.mSpriteName);
	}

	/// <summary>
	/// Override the cursor with the specified sprite.
	/// </summary>

	static public void Set (UIAtlas atlas, string sprite)
	{
		if (instance != null && instance.mSprite)
		{
			instance.mSprite.atlas = atlas;
			instance.mSprite.spriteName = sprite;
			instance.mSprite.MakePixelPerfect();
			instance.Update();
		}
	}
}
