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

[ExecuteInEditMode]
[AddComponentMenu("NGUI/Internal/Snapshot Point")]
public class UISnapshotPoint : MonoBehaviour
{
	public bool isOrthographic = true;
	public float nearClip = -100f;
	public float farClip = 100f;

	[Range(10, 80)]
	public int fieldOfView = 35;
	public float orthoSize = 30f;

	public Texture2D thumbnail;

	void Start () { if (tag != "EditorOnly") tag = "EditorOnly"; }
}
