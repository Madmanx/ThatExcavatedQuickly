
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CharactersDatabase))]
public class DatabaseEditor : Editor
{

    CharactersDatabase comp;

    public void OnEnable()
    {
        comp = (CharactersDatabase)target;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Reset Database"))
            comp.ResetAll();
        
        base.OnInspectorGUI();



        EditorUtility.SetDirty(comp);
    }

}