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
/// Turns the popup list it's attached to into a language selection list.
/// </summary>

[RequireComponent(typeof(UIPopupList))]
[AddComponentMenu("NGUI/Interaction/Language Selection")]
public class LanguageSelection : MonoBehaviour
{
	UIPopupList mList;

	void Awake ()
	{
		mList = GetComponent<UIPopupList>();
		Refresh();
	}

	void Start () { EventDelegate.Add(mList.onChange, delegate() { Localization.language = UIPopupList.current.value; }); }

	/// <summary>
	/// Immediately refresh the list of known languages.
	/// </summary>

	public void Refresh ()
	{
		if (mList != null && Localization.knownLanguages != null)
		{
			mList.Clear();

			for (int i = 0, imax = Localization.knownLanguages.Length; i < imax; ++i)
				mList.items.Add(Localization.knownLanguages[i]);

			mList.value = Localization.language;
		}
	}
}
