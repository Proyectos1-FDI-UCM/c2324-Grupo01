using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TempoManager))]
public class TempoManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TempoManager tempoManager = (TempoManager)target;

        if (GUILayout.Button("Update Player speed in inspector"))
        {
            tempoManager.UpdatePlayerSpeedInInspector();
        }

        if (GUILayout.Button("Save and set level Tempo/Player speed"))
        {
            tempoManager.SetLevelTempo();
        }
    }

}