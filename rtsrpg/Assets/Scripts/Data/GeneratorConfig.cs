using Agent;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Felix/GeneratorConfig")]
    public class GeneratorConfig : ScriptableObject
    {
        public AgentScript toSpawn;
    }
}