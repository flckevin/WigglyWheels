using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using QuocAnh.Tool;
using System.ComponentModel;

#if UNITY_EDITOR
public class ColliderGenEditor : EditorWindow
{
  

    #region EDITOR WINDOW IN PROGRESS

    public GameObject[] target; // all target to generate
    public GameObject parentTo; // parent target
    public int layer = 7; // layer for collider
    public int size = 1; // size of path in collider
    public bool sameScale; //set same scale

    SerializedObject serialObj;

    [MenuItem("Tools/2D Polygon Generator")]
    public static void ShowWindow()
    {
        GetWindow<ColliderGenEditor>("2D Pollygon Colider Generator");
    }

    private void OnEnable()
    {
        ScriptableObject scriptableObj = this;
        serialObj = new SerializedObject(scriptableObj);
    }

    void OnGUI()
    {
        GUILayout.Space(30);
        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter , fontStyle = FontStyle.Bold, fontSize = 20 };
        style.normal.textColor = Color.green;
        style.fontStyle = FontStyle.Bold;
        EditorGUILayout.LabelField("2D POLYGON GENERATOR SETTING", style );

        GUILayout.Space(20);


        #region VARIBLES

        #region Arrray

        serialObj.Update();
        SerializedProperty serialProp = serialObj.FindProperty("target");

        EditorGUILayout.PropertyField(serialProp, true);
        serialObj.ApplyModifiedProperties();

        //REFERENCE 1: https://forum.unity.com/threads/editorwindow-gameobject-array.509218/ 
        //REFERENCE 2: https://stackoverflow.com/questions/47753367/how-to-display-modify-array-in-the-editor-window
        #endregion

        parentTo = (GameObject)EditorGUILayout.ObjectField("PARENT TO", parentTo, typeof(GameObject), true);

        GUILayout.Space(5);

        layer = EditorGUILayout.IntField("LAYER", layer);

        GUILayout.Space(5);

        size = EditorGUILayout.IntField("PATH SIZE", size);

        GUILayout.Space(5);

        sameScale = EditorGUILayout.Toggle("SAME SCALE", sameScale);

        GUILayout.Space(20);


        #endregion

        if (GUILayout.Button("Generate Collider"))
        {
            TwoDimenPolygonGen twoGen = new TwoDimenPolygonGen();

            twoGen.target = target;
            twoGen.parentToColGroup = parentTo;
            twoGen.layer = layer;
            twoGen.pathSize = size;
            twoGen.sameScale = sameScale;

            twoGen.GenerateInitiation();
        }

    }

    #endregion
}

#endif
