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
/// Allows dragging of the camera object and restricts camera's movement to be within bounds of the area created by the rootForBounds colliders.
/// </summary>

[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Drag Camera")]
public class UIDragCamera : MonoBehaviour
{
	/// <summary>
	/// Target object that will be dragged.
	/// </summary>

	public UIDraggableCamera draggableCamera;

	/// <summary>
	/// Automatically find the draggable camera if possible.
	/// </summary>

	void Awake ()
	{
		if (draggableCamera == null)
		{
			draggableCamera = NGUITools.FindInParents<UIDraggableCamera>(gameObject);
		}
	}

	/// <summary>
	/// Forward the press event to the draggable camera.
	/// </summary>

	void OnPress (bool isPressed)
	{
		if (enabled && NGUITools.GetActive(gameObject) && draggableCamera != null)
		{
			draggableCamera.Press(isPressed);
		}
	}

	/// <summary>
	/// Forward the drag event to the draggable camera.
	/// </summary>

	void OnDrag (Vector2 delta)
	{
		if (enabled && NGUITools.GetActive(gameObject) && draggableCamera != null)
		{
			draggableCamera.Drag(delta);
		}
	}

	/// <summary>
	/// Forward the scroll event to the draggable camera.
	/// </summary>

	void OnScroll (float delta)
	{
		if (enabled && NGUITools.GetActive(gameObject) && draggableCamera != null)
		{
			draggableCamera.Scroll(delta);
		}
	}
}
