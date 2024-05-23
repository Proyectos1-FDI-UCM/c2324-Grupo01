using UnityEngine;
using UnityEditor;

/// <summary>
/// User interface for development to create timestamps. A design tool to sync objects in space to music tempos and custom beats.
/// </summary>
[CustomEditor(typeof(BeatPositionCalculator))]
public class BeatPositionCalculatorEditor : Editor
{
    private static bool readyToInstantiateBaseBPM = false;
    private static bool readyToInstantiateCustomBeats = false;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        BeatPositionCalculator calculator = (BeatPositionCalculator)target;

        if (GUILayout.Button("Calculate Base Beat (BPM)"))
        {
            calculator.CalculateBaseBeats();
            readyToInstantiateBaseBPM = true;
        }

        if (GUILayout.Button("Instantiate BPM Timestamps") && readyToInstantiateBaseBPM)
        {
            InstatantiateBPMTimestamps(calculator);            
        }

        if (GUILayout.Button("Calculate Custom Beats"))
        {
            calculator.CalculateCustomBeats();
            readyToInstantiateCustomBeats = true;
        }

        if (GUILayout.Button("Instantiate Custom Beats Timestamps") && readyToInstantiateCustomBeats)
        {
            InstatantiateCustomBeatsTimestamps(calculator);
        }
    }

    /// <summary>
    /// Creates a game object BPM Timestamps that has child timestamp prefabs.
    /// They are positioned on the xs recorded in the scriptable object set as data.
    /// </summary>
    /// <param name="calculator"></param>
    private void InstatantiateBPMTimestamps(BeatPositionCalculator calculator)
    {
        GameObject newObj = new("BPM Timestamps");
        int i = 0;

        foreach (var timestamp in calculator.baseBPMData.timestamps)
        {
            GameObject newPrefab = Instantiate(calculator.BPMTimestampPrefab);
            newPrefab.name = $"BPM Stamp: {timestamp.time}s / x={timestamp.positionX}";
            newPrefab.transform.parent = newObj.transform;
            newPrefab.transform.position = new(timestamp.positionX, -3.0f, 0f);
            if (i % 4 == 0)
            {
                newPrefab.GetComponent<SpriteRenderer>().color = Color.blue;
            }
            i++;      
        }

        Debug.Log("BPM timestamps created!");
    }

    /// <summary>
    /// Creates a game object Custom Timestamps that has child timestamp prefabs.
    /// They are positioned on the xs recorded in the scriptable object set as data.
    /// </summary>
    /// <param name="calculator"></param>
    private void InstatantiateCustomBeatsTimestamps(BeatPositionCalculator calculator)
    {
        GameObject newObj = new("Custom Timestamps");

        foreach (var timestamp in calculator.customBeatsData.timestamps)
        {
            GameObject newPrefab = Instantiate(calculator.CustomBeatsTimestampPrefab);
            newPrefab.name = $"Custom Stamp: {timestamp.time}s / x={timestamp.positionX}";
            newPrefab.transform.parent = newObj.transform;
            newPrefab.transform.position = new(timestamp.positionX, -3.0f, 0f);
        }

        Debug.Log("Custom timestamps created!");
    }
}