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

#if !UNITY_EDITOR && (UNITY_IPHONE || UNITY_ANDROID || UNITY_WP8 || UNITY_BLACKBERRY || UNITY_WINRT)
#define MOBILE
#endif

using UnityEngine;

/// <summary>
/// This class is added by UIInput when it gets selected in order to be able to receive input events properly.
/// The reason it's not a part of UIInput is because it allocates 336 bytes of GC every update because of OnGUI.
/// It's best to only keep it active when it's actually needed.
/// </summary>

[RequireComponent(typeof(UIInput))]
public class UIInputOnGUI : MonoBehaviour
{
#if !MOBILE
	[System.NonSerialized] UIInput mInput;

	void Awake () { mInput = GetComponent<UIInput>(); }

	/// <summary>
	/// Unfortunately Unity 4.3 and earlier doesn't offer a way to properly process events outside of OnGUI.
	/// </summary>

	void OnGUI ()
	{
		if (Event.current.rawType == EventType.KeyDown)
			mInput.ProcessEvent(Event.current);
	}
#endif
}
