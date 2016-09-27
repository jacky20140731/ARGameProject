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
/// Inspector class used to edit UITextures.
/// </summary>

[CanEditMultipleObjects]
[CustomEditor(typeof(UIBasicSprite), true)]
public class UIBasicSpriteEditor : UIWidgetInspector
{
	/// <summary>
	/// Draw all the custom properties such as sprite type, flip setting, fill direction, etc.
	/// </summary>

	protected override void DrawCustomProperties ()
	{
		GUILayout.Space(6f);

		SerializedProperty sp = NGUIEditorTools.DrawProperty("Type", serializedObject, "mType", GUILayout.MinWidth(20f));

		UISprite.Type type = (UISprite.Type)sp.intValue;

		if (type == UISprite.Type.Simple)
		{
			NGUIEditorTools.DrawProperty("Flip", serializedObject, "mFlip");
		}
		else if (type == UISprite.Type.Tiled)
		{
			NGUIEditorTools.DrawBorderProperty("Trim", serializedObject, "mBorder");
			NGUIEditorTools.DrawProperty("Flip", serializedObject, "mFlip");
		}
		else if (type == UISprite.Type.Sliced)
		{
			NGUIEditorTools.DrawBorderProperty("Border", serializedObject, "mBorder");
			NGUIEditorTools.DrawProperty("Flip", serializedObject, "mFlip");

			EditorGUI.BeginDisabledGroup(sp.hasMultipleDifferentValues);
			{
				sp = serializedObject.FindProperty("centerType");
				bool val = (sp.intValue != (int)UISprite.AdvancedType.Invisible);

				if (val != EditorGUILayout.Toggle("Fill Center", val))
				{
					sp.intValue = val ? (int)UISprite.AdvancedType.Invisible : (int)UISprite.AdvancedType.Sliced;
				}
			}
			EditorGUI.EndDisabledGroup();
		}
		else if (type == UISprite.Type.Filled)
		{
			NGUIEditorTools.DrawProperty("Flip", serializedObject, "mFlip");
			NGUIEditorTools.DrawProperty("Fill Dir", serializedObject, "mFillDirection", GUILayout.MinWidth(20f));
			GUILayout.BeginHorizontal();
			GUILayout.Space(4f);
			NGUIEditorTools.DrawProperty("Fill Amount", serializedObject, "mFillAmount", GUILayout.MinWidth(20f));
			GUILayout.Space(4f);
			GUILayout.EndHorizontal();
			NGUIEditorTools.DrawProperty("Invert Fill", serializedObject, "mInvert", GUILayout.MinWidth(20f));
		}
		else if (type == UISprite.Type.Advanced)
		{
			NGUIEditorTools.DrawBorderProperty("Border", serializedObject, "mBorder");
			NGUIEditorTools.DrawProperty("  Left", serializedObject, "leftType");
			NGUIEditorTools.DrawProperty("  Right", serializedObject, "rightType");
			NGUIEditorTools.DrawProperty("  Top", serializedObject, "topType");
			NGUIEditorTools.DrawProperty("  Bottom", serializedObject, "bottomType");
			NGUIEditorTools.DrawProperty("  Center", serializedObject, "centerType");
			NGUIEditorTools.DrawProperty("Flip", serializedObject, "mFlip");
		}

		//GUI.changed = false;
		//Vector4 draw = EditorGUILayout.Vector4Field("Draw Region", mWidget.drawRegion);

		//if (GUI.changed)
		//{
		//    NGUIEditorTools.RegisterUndo("Draw Region", mWidget);
		//    mWidget.drawRegion = draw;
		//}
		base.DrawCustomProperties();
	}
}
