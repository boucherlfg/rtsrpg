using System;
using System.Collections.Generic;
using Agent;
using UnityEngine;

namespace Data
{
    [Serializable]
    public struct SpawnCountPair
    {
        public int maxSpawnCount;
        public AgentScript prefab;
    }
    
    [CreateAssetMenu(menuName = "Felix/PoolingConfig")]
    public class PoolingConfig : ScriptableObject
    {
        public List<SpawnCountPair> spawnCounts;
    }
}