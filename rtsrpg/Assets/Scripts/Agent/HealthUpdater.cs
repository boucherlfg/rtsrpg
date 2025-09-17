using Events;
using States;
using UnityEngine;

namespace Agent
{
    public class HealthUpdater : AbstractUpdater
    {
        public override void Initialize(AgentState state)
        {
            _state = state;
            ServiceManager.Instance.Get<OnHealthChanged>().Subscribe(HandleHealthChanged);
            if (state is IDurable durable)
            {
                durable.CurrentHealth = durable.MaxHealth;
            }
        }

        private void HandleHealthChanged((AgentState agent, int oldHealth, int newHealth) obj)
        {
            if (obj.agent.id != _state.id) return;
        }

        public override void Uninitialize()
        {
            ServiceManager.Instance.Get<OnHealthChanged>().Unsubscribe(HandleHealthChanged);
        }
    }
}