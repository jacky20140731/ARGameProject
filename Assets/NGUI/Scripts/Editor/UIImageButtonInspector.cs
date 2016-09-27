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
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// Inspector class used to edit UISprites.
/// </summary>

[CustomEditor(typeof(UIImageButton))]
public class UIImageButtonInspector : Editor
{
	public override void OnInspectorGUI ()
	{
		EditorGUILayout.HelpBox("Image Button component's functionality is now a part of UIButton. You no longer need UIImageButton.", MessageType.Warning, true);

		if (GUILayout.Button("Auto-Upgrade"))
		{
			UIImageButton img = target as UIImageButton;

			UIButton btn = img.GetComponent<UIButton>();

			if (btn == null)
			{
				btn = img.gameObject.AddComponent<UIButton>();
				if (img.target != null) btn.tweenTarget = img.target.gameObject;
				else btn.tweenTarget = img.gameObject;

				UISprite sp = btn.tweenTarget.GetComponent<UISprite>();
				if (sp != null) sp.spriteName = img.normalSprite;
			}

			btn.hoverSprite = img.hoverSprite;
			btn.pressedSprite = img.pressedSprite;
			btn.disabledSprite = img.disabledSprite;
			btn.pixelSnap = img.pixelSnap;

			NGUITools.DestroyImmediate(img);
		}
	}
}
