using System;
using System.Linq;
using Core;
using Generic;
using States;
using UnityEngine;

namespace Systems
{
    public abstract class GameSystem<T> : MonoBehaviour
    {
        protected WorldState World;
        private void Awake()
        {
            World = ServiceManager.Instance.Get<WorldState>();
            InitializeSystem();
        }

        private void OnDestroy()
        {
            UninitializeSystem();
        }

        private void Update()
        {
            var agents = World.GetAll<T>().ToArray();
            
            foreach (var agent in agents)
            {
                if (agent is not AgentState state) continue;
                if (state.initialized) continue;
                state.initialized = true;
                InitializeState(agent);
            }

            foreach (var agent in agents)
            {
                if (agent is not AgentState state) continue;
                if (!state.shouldUninitialize || state.uninitialized) continue;
                state.uninitialized = true;
                UninitializeState(agent);
                World.Remove(state);
            }
            foreach (var agent in agents)
            {
                UpdateOneState(agent);
            }
        }

        protected abstract void UpdateOneState(T agent);
        
        protected abstract void InitializeState(T agent);
        
        protected abstract void UninitializeState(T agent);
        
        protected abstract void InitializeSystem();
        
        protected abstract void UninitializeSystem();
    }
}
