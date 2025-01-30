using UnityEngine;
using UnityEngine.Serialization;

namespace ProcGen
{
    [System.Serializable]
    public class Props
    {
        [EnumMask] public TerrainType whereItGrows;
        public int quantity = 1000;
        public int epicenters = 10;
        public float minDistance = 1;
        public float maxDistance = 3;

        public float respawnRate = 30;
        
        public GameObject prefab;
    }
}