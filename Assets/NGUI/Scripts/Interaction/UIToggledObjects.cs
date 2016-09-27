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
using System.Collections.Generic;

/// <summary>
/// Example script showing how to activate or deactivate a game object when a toggle's state changes.
/// OnActivate event is sent out by the UIToggle script.
/// </summary>

[AddComponentMenu("NGUI/Interaction/Toggled Objects")]
public class UIToggledObjects : MonoBehaviour
{
	public List<GameObject> activate;
	public List<GameObject> deactivate;

	[HideInInspector][SerializeField] GameObject target;
	[HideInInspector][SerializeField] bool inverse = false;

	void Awake ()
	{
		// Legacy functionality -- auto-upgrade
		if (target != null)
		{
			if (activate.Count == 0 && deactivate.Count == 0)
			{
				if (inverse) deactivate.Add(target);
				else activate.Add(target);
			}
			else target = null;

#if UNITY_EDITOR
			NGUITools.SetDirty(this);
#endif
		}

#if UNITY_EDITOR
		if (!Application.isPlaying) return;
#endif
		UIToggle toggle = GetComponent<UIToggle>();
		EventDelegate.Add(toggle.onChange, Toggle);
	}

	public void Toggle ()
	{
		bool val = UIToggle.current.value;

		if (enabled)
		{
			for (int i = 0; i < activate.Count; ++i)
				Set(activate[i], val);

			for (int i = 0; i < deactivate.Count; ++i)
				Set(deactivate[i], !val);
		}
	}

	void Set (GameObject go, bool state)
	{
		if (go != null)
		{
			NGUITools.SetActive(go, state);
			//UIPanel panel = NGUITools.FindInParents<UIPanel>(target);
			//if (panel != null) panel.Refresh();
		}
	}
}
