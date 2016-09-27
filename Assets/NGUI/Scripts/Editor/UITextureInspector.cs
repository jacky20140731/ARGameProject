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
[CustomEditor(typeof(UITexture), true)]
public class UITextureInspector : UIBasicSpriteEditor
{
	UITexture mTex;

	protected override void OnEnable ()
	{
		base.OnEnable();
		mTex = target as UITexture;
	}

	protected override bool ShouldDrawProperties ()
	{
		if (target == null) return false;
		SerializedProperty sp = NGUIEditorTools.DrawProperty("Texture", serializedObject, "mTexture");
		NGUIEditorTools.DrawProperty("Material", serializedObject, "mMat");

		if (sp != null) NGUISettings.texture = sp.objectReferenceValue as Texture;

		if (mTex != null && (mTex.material == null || serializedObject.isEditingMultipleObjects))
		{
			NGUIEditorTools.DrawProperty("Shader", serializedObject, "mShader");
		}

		EditorGUI.BeginDisabledGroup(mTex == null || mTex.mainTexture == null || serializedObject.isEditingMultipleObjects);

		NGUIEditorTools.DrawRectProperty("UV Rect", serializedObject, "mRect");

		sp = serializedObject.FindProperty("mFixedAspect");
		bool before = sp.boolValue;
		NGUIEditorTools.DrawProperty("Fixed Aspect", sp);
		if (sp.boolValue != before) (target as UIWidget).drawRegion = new Vector4(0f, 0f, 1f, 1f);

		if (sp.boolValue)
		{
			EditorGUILayout.HelpBox("Note that Fixed Aspect mode is not compatible with Draw Region modifications done by sliders and progress bars.", MessageType.Info);
		}

		EditorGUI.EndDisabledGroup();
		return true;
	}

	/// <summary>
	/// Allow the texture to be previewed.
	/// </summary>

	public override bool HasPreviewGUI ()
	{
		return (Selection.activeGameObject == null || Selection.gameObjects.Length == 1) &&
			(mTex != null) && (mTex.mainTexture as Texture2D != null);
	}

	/// <summary>
	/// Draw the sprite preview.
	/// </summary>

	public override void OnPreviewGUI (Rect rect, GUIStyle background)
	{
		Texture2D tex = mTex.mainTexture as Texture2D;

		if (tex != null)
		{
			Rect tc = mTex.uvRect;
			tc.xMin *= tex.width;
			tc.xMax *= tex.width;
			tc.yMin *= tex.height;
			tc.yMax *= tex.height;
			NGUIEditorTools.DrawSprite(tex, rect, mTex.color, tc, mTex.border);
		}
	}
}
