using System.Linq;
using Core;
using States;
using UnityEngine;

namespace Systems
{
    public abstract class GameSystem<T> : MonoBehaviour
    {
        protected WorldState World;
        protected virtual void Awake()
        {
            World = ServiceManager.Instance.Get<WorldState>();
        }

        private void Update()
        {
            var agents = World.GetAll<T>().ToArray();
            
            foreach (var agent in agents)
            {
                if (agent is not AgentState state) continue;
                if (state.initialized) continue;
                state.initialized = true;
                Initialize(agent);
            }

            foreach (var agent in agents)
            {
                if (agent is not AgentState state) continue;
                if (!state.shouldUninitialize || state.uninitialized) continue;
                state.uninitialized = true;
                Uninitialize(agent);
                World.Remove(state);
            }
            foreach (var agent in agents)
            {
                UpdateOneState(agent);
            }
        }

        protected abstract void UpdateOneState(T agent);
        protected abstract void Initialize(T agent);
        protected abstract void Uninitialize(T agent);
    }
}
