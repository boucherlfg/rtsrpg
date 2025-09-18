using Events;
using Generic;
using States;
using UnityEngine;

namespace Agent
{
    public class PositionUpdater : AbstractUpdater
    {
        private OnPositionChanged _onPositionChanged;
        public override void Initialize(AgentState state)
        {
            _state = state;
            _onPositionChanged = ServiceManager.Instance.Get<OnPositionChanged>();
            _onPositionChanged.Subscribe(HandlePositionChanged);

            if (state is not IPosition position) return;
            position.Position = transform.position;
        }

        private void HandlePositionChanged((AgentState agent, Vector2 newPosition) obj)
        {
            if (_state.id != obj.agent.id) return;
            transform.position = obj.newPosition;
        }

        public override void Uninitialize()
        {
            ServiceManager.Instance.Get<OnPositionChanged>().Unsubscribe(HandlePositionChanged);
        }
    }
}