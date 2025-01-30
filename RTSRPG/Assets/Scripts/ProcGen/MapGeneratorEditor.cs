using UnityEditor;
using UnityEngine;

namespace ProcGen
{
    [CustomEditor(typeof(MapGenerator))]
    public class MapGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var mapGen = target as MapGenerator;

            DrawDefaultInspector();
            if (GUILayout.Button("Generate"))
            {
                mapGen.Generate();
            }
        }
    }
}