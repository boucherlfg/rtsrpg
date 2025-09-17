using System;
using Core;
using Events;
using States;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Systems
{
    public class ResourceSystem : GameSystem<ResourceState>
    {
        protected override void Awake()
        {
            base.Awake();
            var interactedEvent = ServiceManager.Instance.Get<OnInteracted>();
            interactedEvent.Subscribe(HandleInteract);
        }

        private void OnDestroy()
        {
            var interactedEvent = ServiceManager.Instance.Get<OnInteracted>();
            interactedEvent.Unsubscribe(HandleInteract);
        }

        protected override void UpdateOneState(ResourceState agent)
        {
            // nothing
        }
        protected override void Initialize(ResourceState agent)
        {
            // nothing
        }
        protected override void Uninitialize(ResourceState agent)
        {
            // nothing
        }

        private void HandleInteract((AgentState source, AgentState target) args)
        {
            if (args.source is not IInteracting interacting) return;
            
            _ = Extensions.Interpolate(interacting.AttackDuration/2.0f, null, onCompleted: () =>
            {
                if (args.target is not IDurable durable) return;

                var oldHealth = durable.CurrentHealth;
                durable.CurrentHealth -= 1;
                ServiceManager.Instance.Get<OnHealthChanged>().Invoke((args.target, oldHealth, durable.CurrentHealth));
                if (durable.CurrentHealth <= 0)
                {
                    ServiceManager.Instance.Get<Pooling>().Despawn(args.target.id);
                }
            });
            
            
        }

    }
}