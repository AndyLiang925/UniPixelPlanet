using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(IPlanetOld), true)]
public class CustomEditorPlanetGen : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        IPlanetOld myScript = (IPlanetOld)target;
        if (GUILayout.Button("Update"))
        {
            myScript.UpdateViaEditor();
        }
    }
}


public abstract class IPlanetOld : MonoBehaviour
{

    [SerializeField] public int Pixel = 100;
    [SerializeField] public string Seed = "Seed";
    [SerializeField] public float CalcSeed;
    [SerializeField] public bool GenerateColors = false;

    public abstract void Initialize();

    public abstract void UpdateViaEditor();
}
