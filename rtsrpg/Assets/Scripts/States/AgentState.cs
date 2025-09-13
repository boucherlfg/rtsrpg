using System.Collections.Generic;
using Agent;
using Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace States
{
    [System.Serializable]
    public class AgentState
    {
        [HideInInspector] public int id;
        [HideInInspector] public bool initialized;
        [HideInInspector] public bool shouldUninitialize;
        [HideInInspector] public bool uninitialized;
    }
}