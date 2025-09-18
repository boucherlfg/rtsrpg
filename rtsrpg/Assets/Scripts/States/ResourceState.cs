using System.Collections.Generic;
using Agent;
using Data;
using Generic;
using UnityEngine;

namespace States
{
    public class ResourceState : AgentState, IInteractable, IDurable, IInventory
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private List<Item> startInventory;
        
        public Vector2 Position { get; set; }
        public int MaxHealth => maxHealth;
        public int CurrentHealth { get; set; } = 0;

        public List<Item> Inventory { get; } = new();
        public List<Item> StartInventory => startInventory;
        
        public override AgentState Copy()
        {
            return new ResourceState
            {
                maxHealth = maxHealth,
                startInventory = new List<Item>(startInventory),
                CurrentHealth = 0
            };
        }
    }
}
