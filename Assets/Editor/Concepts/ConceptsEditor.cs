using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Concepts))]
public class ConceptsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Concepts concept = (Concepts)target;
        if (GUILayout.Button("Check Concepts"))
        {
            concept.CheckStructure();
        }
    }
}
