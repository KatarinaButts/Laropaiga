using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUITest : MonoBehaviour
{
    public GUIStyle customButton;
    bool pressed = true;
    bool pressed2 = true;
    bool pressed3 = true;
    bool pressed4 = true;
    bool pressed5 = true;
    bool pressed6 = true;

    string Toggle1 = "Toggle1";
    string Toggle2 = "Toggle2";
    string Toggle3 = "Toggle3";
    string Toggle4 = "Toggle4";
    string Toggle5 = "Toggle5";
    string Toggle6 = "Toggle6";



    int screenWidth = Screen.width;

    [SerializeField]
    RectTransform parent;

    //bool toggleButton1;
    //bool toggleButton2;
    //bool toggleButton3;
    //bool toggleButton4;
    //bool toggleButton5;

    //toggleButton;


    private void OnGUI()
    {
        GUILayout.BeginHorizontal("box", GUILayout.Width(screenWidth));
        //GUILayout.BeginVertical("box", GUILayout.Height(parent.rect.height));

        GUILayout.Space(10.0f);

        pressed = GUILayout.Toggle(pressed, Toggle1, customButton, GUILayout.Height(100));

        GUILayout.Space(10.0f);

        pressed2 = GUILayout.Toggle(pressed2, Toggle2, customButton, GUILayout.Height(100));

        GUILayout.Space(10.0f);

        pressed3 = GUILayout.Toggle(pressed3, Toggle3, customButton, GUILayout.Height(100));

        GUILayout.Space(10.0f);

        pressed4 = GUILayout.Toggle(pressed4, Toggle4, customButton, GUILayout.Height(100));

        GUILayout.Space(10.0f);

        pressed5 = GUILayout.Toggle(pressed5, Toggle5, customButton, GUILayout.Height(100));

        GUILayout.Space(10.0f);

        pressed6 = GUILayout.Toggle(pressed6, Toggle6, customButton, GUILayout.Height(100));

        // Make a button. We pass in the GUIStyle defined above as the style to use
        //toggleButton1 = GUI.Toggle(new Rect(parent.position.x, 0, 150, 150), pressed, "Toggle1", customButton);
        //toggleButton2 = GUI.Toggle(new Rect(parent.position.x, 150, 150, 150), pressed, "Toggle2", customButton);
        //toggleButton3 = GUI.Toggle(new Rect(parent.position.x, 300, 150, 150), pressed, "Toggle3", customButton);
        //toggleButton4 = GUI.Toggle(new Rect(parent.position.x, 450, 150, 150), pressed, "Toggle4", customButton);
        //toggleButton5 = GUI.Toggle(new Rect(parent.position.x, 600, 150, 150), pressed, "Toggle5", customButton);

        //GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

}
