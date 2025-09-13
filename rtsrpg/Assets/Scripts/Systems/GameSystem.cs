using Core;
using States;
using UnityEngine;

namespace Systems
{
    public abstract class GameSystem<T> : MonoBehaviour where T : AgentState
    {
        protected WorldState World;
        protected virtual void Awake()
        {
            World = ServiceManager.Instance.Get<WorldState>();
        }

        private void Update()
        {
            var agents = World.GetAll<T>();
            foreach (var agent in agents)
            {
                if (!agent.initialized)
                {
                    agent.initialized = true;
                    Initialize(agent);
                }
                if (agent.shouldUninitialize && !agent.uninitialized)
                {
                    agent.uninitialized = true;
                    Uninitialize(agent);
                    World.Remove(agent);
                }
                UpdateOneState(agent);
            }
        }

        protected abstract void UpdateOneState(T agent);
        protected abstract void Initialize(T agent);
        protected abstract void Uninitialize(T agent);
    }
}
