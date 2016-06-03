﻿using UnityEngine;
using UnityEditor;
using System.Collections;

public class PositionManager : MonoBehaviour {

	[MenuItem("Assets/Create/PositionManager")]
    public static void CreateAsset()
    {
        ScriptingObjects positionManager = ScriptableObject.CreateInstance<ScriptingObjects>();

        AssetDatabase.CreateAsset(positionManager, "Assets/newPositionManager.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = positionManager;
    }	
}
