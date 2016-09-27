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

using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// UI Creation Wizard. This tool has been made obsolete with NGUI 3.0.6.
/// </summary>

public class UICreateNewUIWizard : EditorWindow
{
	public enum CameraType
	{
		None,
		Simple2D,
		Advanced3D,
	}

	static public CameraType camType = CameraType.Simple2D;

	/// <summary>
	/// Refresh the window on selection.
	/// </summary>

	void OnSelectionChange () { Repaint(); }

	/// <summary>
	/// Draw the custom wizard.
	/// </summary>

	void OnGUI ()
	{
		NGUIEditorTools.SetLabelWidth(80f);

		GUILayout.Label("Create a new UI with the following parameters:");
		NGUIEditorTools.DrawSeparator();

		GUILayout.BeginHorizontal();
		NGUISettings.layer = EditorGUILayout.LayerField("Layer", NGUISettings.layer, GUILayout.Width(200f));
		GUILayout.Space(20f);
		GUILayout.Label("This is the layer your UI will reside on");
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		camType = (CameraType)EditorGUILayout.EnumPopup("Camera", camType, GUILayout.Width(200f));
		GUILayout.Space(20f);
		GUILayout.Label("Should this UI have a camera?");
		GUILayout.EndHorizontal();

		NGUIEditorTools.DrawSeparator();
		GUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("When ready,");
		bool create = GUILayout.Button("Create Your UI", GUILayout.Width(120f));
		GUILayout.EndHorizontal();

		if (create) CreateNewUI(camType);

		EditorGUILayout.HelpBox("This tool has become obsolete with NGUI 3.0.6. You can create UIs from the NGUI -> Create menu.", MessageType.Warning);
	}

	/// <summary>
	/// Create a brand-new UI hierarchy.
	/// </summary>

	static public GameObject CreateNewUI (CameraType type)
	{
		UIPanel p = NGUITools.CreateUI(type == CameraType.Advanced3D, NGUISettings.layer);
		Selection.activeGameObject = p.gameObject;
		return Selection.activeGameObject;
	}
}
