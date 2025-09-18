using Generic;
using UnityEngine;

namespace States
{
    [System.Serializable]
    public abstract class AgentState : ICopiable<AgentState>
    {
        [HideInInspector] public int id;
        [HideInInspector] public bool initialized;
        [HideInInspector] public bool shouldUninitialize;
        [HideInInspector] public bool uninitialized;
        public abstract AgentState Copy();
    }
}