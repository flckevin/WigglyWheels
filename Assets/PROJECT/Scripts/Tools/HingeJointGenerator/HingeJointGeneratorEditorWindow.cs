using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using QuocAnh.Tool;


#if UNITY_EDITOR
public class HingeJointGeneratorEditorWindow : EditorWindow
{
    SerializedObject serialObj;
    public GameObject[] target;

    [MenuItem("Tools/HingeJoint Generator")]
    static void ShowWindow()
    {
        GetWindow<HingeJointGeneratorEditorWindow>("Hinge Joint Generator");

    }


    private void OnEnable()
    {
        ScriptableObject scriptableObj = this;
        serialObj = new SerializedObject(scriptableObj);
    }

    private void OnGUI()
    {
        GUILayout.Space(30);
        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 20 };
        style.normal.textColor = UnityEngine.Color.yellow;
        style.fontStyle = FontStyle.Bold;
        EditorGUILayout.LabelField("HINGEJOINT ASSIGNER", style);

        GUILayout.Space(20);

        #region Arrray

        serialObj.Update();
        SerializedProperty serialProp = serialObj.FindProperty("target");

        EditorGUILayout.PropertyField(serialProp, true);
        serialObj.ApplyModifiedProperties();

        //REFERENCE 1: https://forum.unity.com/threads/editorwindow-gameobject-array.509218/ 
        //REFERENCE 2: https://stackoverflow.com/questions/47753367/how-to-display-modify-array-in-the-editor-window
        #endregion

        GUILayout.Space(20);

        if (GUILayout.Button("Generate HingeJoint"))
        {
            HIngeJointGenerator hingeGen = new HIngeJointGenerator();
            hingeGen.targets = target;
            hingeGen.CreateHingeJoint();
        }

    }
}
#endif