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

[CustomEditor(typeof(UIWrapContent), true)]
public class UIWrapContentEditor : Editor
{
	public override void OnInspectorGUI ()
	{
		GUILayout.Space(6f);
		NGUIEditorTools.SetLabelWidth(90f);

		string fieldName = "Item Size";
		string error = null;
		UIScrollView sv = null;

		if (!serializedObject.isEditingMultipleObjects)
		{
			UIWrapContent list = target as UIWrapContent;
			sv = NGUITools.FindInParents<UIScrollView>(list.gameObject);

			if (sv == null)
			{
				error = "UIWrappedList needs a Scroll View on its parent in order to work properly";
			}
			else if (sv.movement == UIScrollView.Movement.Horizontal) fieldName = "Item Width";
			else if (sv.movement == UIScrollView.Movement.Vertical) fieldName = "Item Height";
			else
			{
				error = "Scroll View needs to be using Horizontal or Vertical movement";
			}
		}

		serializedObject.Update();
		GUILayout.BeginHorizontal();
		NGUIEditorTools.DrawProperty(fieldName, serializedObject, "itemSize", GUILayout.Width(130f));
		GUILayout.Label("pixels");
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		SerializedProperty sp1 = NGUIEditorTools.DrawProperty("Range Limit", serializedObject, "minIndex", GUILayout.Width(130f));
		NGUIEditorTools.SetLabelWidth(20f);
		SerializedProperty sp2 = NGUIEditorTools.DrawProperty("to", serializedObject, "maxIndex", GUILayout.Width(60f));
		NGUIEditorTools.SetLabelWidth(90f);
		if (sp1.intValue == sp2.intValue) GUILayout.Label("unlimited");
		GUILayout.EndHorizontal();

		NGUIEditorTools.DrawProperty("Cull Content", serializedObject, "cullContent");

		if (!string.IsNullOrEmpty(error))
		{
			EditorGUILayout.HelpBox(error, MessageType.Error);
			if (sv != null && GUILayout.Button("Select the Scroll View"))
				Selection.activeGameObject = sv.gameObject;
		}

		serializedObject.ApplyModifiedProperties();

		if (sp1.intValue != sp2.intValue)
		{
			if ((target as UIWrapContent).GetComponent<UICenterOnChild>() != null)
			{
				EditorGUILayout.HelpBox("Limiting indices doesn't play well with UICenterOnChild. You should either not limit the indices, or not use UICenterOnChild.", MessageType.Warning);
			}
		}
	}
}
