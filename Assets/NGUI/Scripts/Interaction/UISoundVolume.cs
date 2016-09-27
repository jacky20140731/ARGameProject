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
/// Very simple script that can be attached to a slider and will control the volume of all sounds played via NGUITools.PlaySound,
/// which includes all of UI's sounds.
/// </summary>

[RequireComponent(typeof(UISlider))]
[AddComponentMenu("NGUI/Interaction/Sound Volume")]
public class UISoundVolume : MonoBehaviour
{
	void Awake ()
	{
		UISlider slider = GetComponent<UISlider>();
		slider.value = NGUITools.soundVolume;
		EventDelegate.Add(slider.onChange, OnChange);
	}

	void OnChange ()
	{
		NGUITools.soundVolume = UISlider.current.value;
	}
}
