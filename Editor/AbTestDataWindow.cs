using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AbTestDataWindow : EditorWindow
{
    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;

    [MenuItem("AB Test/Settings")]
    private static void Init()
    {
        AbTestDataWindow window = (AbTestDataWindow)EditorWindow.GetWindow(typeof(AbTestDataWindow));

        window.titleContent.text = "AB Test Settings";

        int windowWidth = Screen.currentResolution.width / 5;
        int windowHeight = Screen.currentResolution.height / 3;

        window.position = new Rect(Screen.currentResolution.width / 2 - windowWidth / 2, Screen.currentResolution.height / 2 - windowHeight / 2, windowWidth, windowHeight);

        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Text Field", myString);

        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();
    }
}
