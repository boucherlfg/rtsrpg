using System;
using States;
using UnityEngine;

namespace Agent
{
    [Serializable]
    public abstract class AbstractUpdater : MonoBehaviour
    {
        protected AgentState _state;
        public abstract void Initialize(AgentState state);
        public abstract void Uninitialize();
    }
}