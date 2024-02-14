#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using Unity.VisualScripting;

[CustomEditor(typeof(EditorButton))]
public class ExecuteButton : Editor
{
    public EditorButton editorButton;
    private PointerEventData eventData;
    // Start is called before the first frame update
    void Start()
    {   
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Execute Button")) {
            DoIt();
        }
    }
    private void DoIt() {

        if (editorButton == null)
        {
            editorButton = (EditorButton)target;
        }
        editorButton.button.onClick.Invoke();
    }
}
#endif
