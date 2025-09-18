using System;
using Core;
using Data;
using Events;
using Generic;
using States;

namespace Agent
{
    public class InventoryUpdater : AbstractUpdater
    {
        public override void Initialize(AgentState state)
        {
            _state = state;
            ServiceManager.Instance.Get<OnInventoryChanged>().Subscribe(HandleInventoryChanged);
        }

        private void HandleInventoryChanged((IInventory source, IInventory target, Item item) obj)
        {
            if (_state != obj.target) return;
            ServiceManager.Instance.Get<Visuals>().SpawnFloatingImage(obj.item.image, obj.source as AgentState);
        }

        public override void Uninitialize()
        {
            ServiceManager.Instance.Get<OnInventoryChanged>().Unsubscribe(HandleInventoryChanged);
        }
    }
}