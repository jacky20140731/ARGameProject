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
/// This class is meant to be used only internally. It's like Debug.Log, but prints using OnGUI to screen instead.
/// </summary>

[AddComponentMenu("NGUI/Internal/Debug")]
public class NGUIDebug : MonoBehaviour
{
	static bool mRayDebug = false;
	static List<string> mLines = new List<string>();
	static NGUIDebug mInstance = null;

	/// <summary>
	/// Set by UICamera. Can be used to show/hide raycast information.
	/// </summary>

	static public bool debugRaycast
	{
		get
		{
			return mRayDebug;
		}
		set
		{
			mRayDebug = value;
			if (value && Application.isPlaying)
				CreateInstance();
		}
	}

	/// <summary>
	/// Ensure we have an instance present.
	/// </summary>

	static public void CreateInstance ()
	{
		if (mInstance == null)
		{
			GameObject go = new GameObject("_NGUI Debug");
			mInstance = go.AddComponent<NGUIDebug>();
			DontDestroyOnLoad(go);
		}
	}

	/// <summary>
	/// Add a new on-screen log entry.
	/// </summary>

	static void LogString (string text)
	{
#if UNITY_EDITOR
		Debug.Log(text);
#else
		if (Application.isPlaying)
		{
			if (mLines.Count > 20) mLines.RemoveAt(0);
			mLines.Add(text);
			CreateInstance();
		}
		else Debug.Log(text);
#endif
	}

	/// <summary>
	/// Add a new log entry, printing all of the specified parameters.
	/// </summary>

	static public void Log (params object[] objs)
	{
		string text = "";

		for (int i = 0; i < objs.Length; ++i)
		{
			if (i == 0)
			{
				text += objs[i].ToString();
			}
			else
			{
				text += ", " + objs[i].ToString();
			}
		}
		LogString(text);
	}

	/// <summary>
	/// Clear the logged text.
	/// </summary>

	static public void Clear () { mLines.Clear(); }

	/// <summary>
	/// Draw bounds immediately. Won't be remembered for the next frame.
	/// </summary>

	static public void DrawBounds (Bounds b)
	{
		Vector3 c = b.center;
		Vector3 v0 = b.center - b.extents;
		Vector3 v1 = b.center + b.extents;
		Debug.DrawLine(new Vector3(v0.x, v0.y, c.z), new Vector3(v1.x, v0.y, c.z), Color.red);
		Debug.DrawLine(new Vector3(v0.x, v0.y, c.z), new Vector3(v0.x, v1.y, c.z), Color.red);
		Debug.DrawLine(new Vector3(v1.x, v0.y, c.z), new Vector3(v1.x, v1.y, c.z), Color.red);
		Debug.DrawLine(new Vector3(v0.x, v1.y, c.z), new Vector3(v1.x, v1.y, c.z), Color.red);
	}
	
	void OnGUI()
	{
		Rect rect = new Rect(5f, 5f, 1000f, 18f);

		if (mRayDebug)
		{
			UICamera.ControlScheme scheme = UICamera.currentScheme;
			string text = "Scheme: " + scheme;
			GUI.color = Color.black;
			GUI.Label(rect, text);
			rect.y -= 1f;
			rect.x -= 1f;
			GUI.color = Color.white;
			GUI.Label(rect, text);
			rect.y += 18f;
			rect.x += 1f;

			text = "Hover: " + NGUITools.GetHierarchy(UICamera.hoveredObject).Replace("\"", "");
			GUI.color = Color.black;
			GUI.Label(rect, text);
			rect.y -= 1f;
			rect.x -= 1f;
			GUI.color = Color.white;
			GUI.Label(rect, text);
			rect.y += 18f;
			rect.x += 1f;

			text = "Selection: " + NGUITools.GetHierarchy(UICamera.selectedObject).Replace("\"", "");
			GUI.color = Color.black;
			GUI.Label(rect, text);
			rect.y -= 1f;
			rect.x -= 1f;
			GUI.color = Color.white;
			GUI.Label(rect, text);
			rect.y += 18f;
			rect.x += 1f;

			text = "Controller: " + NGUITools.GetHierarchy(UICamera.controllerNavigationObject).Replace("\"", "");
			GUI.color = Color.black;
			GUI.Label(rect, text);
			rect.y -= 1f;
			rect.x -= 1f;
			GUI.color = Color.white;
			GUI.Label(rect, text);
			rect.y += 18f;
			rect.x += 1f;

			text = "Active events: " + UICamera.CountInputSources();
			if (UICamera.disableController) text += ", disabled controller";
			if (UICamera.inputHasFocus) text += ", input focus";
			GUI.color = Color.black;
			GUI.Label(rect, text);
			rect.y -= 1f;
			rect.x -= 1f;
			GUI.color = Color.white;
			GUI.Label(rect, text);
			rect.y += 18f;
			rect.x += 1f;
		}

		for (int i = 0, imax = mLines.Count; i < imax; ++i)
		{
			GUI.color = Color.black;
			GUI.Label(rect, mLines[i]);
			rect.y -= 1f;
			rect.x -= 1f;
			GUI.color = Color.white;
			GUI.Label(rect, mLines[i]);
			rect.y += 18f;
			rect.x += 1f;
		}
	}
}
