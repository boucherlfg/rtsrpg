using Events;
using States;
using UnityEngine;

namespace Agent
{
    public class InteractUpdater : AbstractUpdater
    {
        public override void Initialize(AgentState state)
        {
            _state = state;
            ServiceManager.Instance.Get<OnInteracted>().Subscribe(HandleInteracted);
        }
        
        private void HandleInteracted((AgentState source, AgentState target) obj)
        {
            if (obj.source.id != _state.id) return;
            if(obj.source is not IPosition source) return;
            if(obj.target is not IPosition target) return;
            if(obj.source is not IInteracting interactable) return;
            
            _ = Extensions.Interpolate(interactable.AttackDuration / 2.0f, progress =>
            {
                transform.GetChild(0).position = Vector2.Lerp(source.Position, target.Position, progress);
            }, () =>
            {
                _ = Extensions.Interpolate(interactable.AttackDuration / 2.0f, progress =>
                {
                    transform.GetChild(0).position = Vector2.Lerp(source.Position, target.Position, 1.0f - progress);
                }, curve: progress => Mathf.SmoothStep(0f, 1f, progress));
            }, curve: progress => Mathf.SmoothStep(0f, 1f, progress));
        }

        public override void Uninitialize()
        {
        }
    }
}