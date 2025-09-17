
using Core;
using Events;
using States;
using UnityEngine;

namespace Systems
{
    public class TestSystem : GameSystem<TestState>
    {
        protected override void UpdateOneState(TestState agent)
        {
            // empty
        }

        protected override void Initialize(TestState agent)
        {
            var interactedEvent = ServiceManager.Instance.Get<OnInteracted>();
            if (agent is IInteractable interactable)
            {
                interactedEvent.Subscribe(HandleInteract);
            }
        }

        private void HandleInteract((AgentState source, AgentState target) args)
        {
            Debug.Log("interacted with " + args.target.GetType().Name);
            ServiceManager.Instance.Get<Pooling>().Despawn(args.target.id);
        }

        protected override void Uninitialize(TestState agent)
        {
            var interactedEvent = ServiceManager.Instance.Get<OnInteracted>();
            if (agent is IInteractable interactable)
            {
                interactedEvent.Unsubscribe(HandleInteract);
            }
        }
    }
}
