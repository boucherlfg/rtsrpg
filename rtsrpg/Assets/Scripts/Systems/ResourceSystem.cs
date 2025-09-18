using System.Collections.Generic;
using Core;
using Data;
using Events;
using Generic;
using States;

namespace Systems
{
    public class ResourceSystem : GameSystem<ResourceState>
    {
        protected override void InitializeState(ResourceState agent)
        {
            agent.Inventory.AddRange(agent.StartInventory);
        }
        
        protected override void UninitializeState(ResourceState agent)
        {
            // nothing
        }

        protected override void InitializeSystem()
        {
            var interactedEvent = ServiceManager.Instance.Get<OnInteracted>();
            interactedEvent.Subscribe(HandleInteract);
        }

        protected override void UninitializeSystem()
        {
            var interactedEvent = ServiceManager.Instance.Get<OnInteracted>();
            interactedEvent.Unsubscribe(HandleInteract);
        }

        protected override void UpdateOneState(ResourceState agent)
        {
            // nothing
        }
        
        private void HandleInteract((AgentState source, AgentState target) args)
        {
            if (args.source is not IInteracting interacting) return;
            
            _ = Extensions.Interpolate(interacting.AttackDuration/2.0f, null, onCompleted: () =>
            {
                if (args.target is not IDurable durable)
                {
                    TriggerResource(args);
                    return;
                }

                var oldHealth = durable.CurrentHealth;
                durable.CurrentHealth -= 1;
                ServiceManager.Instance.Get<OnHealthChanged>().Invoke((args.target, oldHealth, durable.CurrentHealth));
                if (durable.CurrentHealth <= 0)
                {
                    TriggerResource(args);
                }
            });
        }

        private void TriggerResource((AgentState source, AgentState target) args)
        {
            if (args.source is not IInventory destinationInventory) goto end;
            if (args.target is not IInventory originInventory) goto end;

            var inventoryChanged = ServiceManager.Instance.Get<OnInventoryChanged>();

            List<Item> copy = new (originInventory.Inventory);
            foreach (var item in copy)
            {
                destinationInventory.Inventory.Add(item);
                originInventory.Inventory.Remove(item);

                inventoryChanged.Invoke((originInventory, destinationInventory, item));
            }
            originInventory.Inventory.Clear();
            
            end: 
            ServiceManager.Instance.Get<Visuals>().SpawnDestroyParticle(((IPosition)args.target).Position);
            ServiceManager.Instance.Get<Pooling>().Despawn(args.target.id);
        }
    }

}