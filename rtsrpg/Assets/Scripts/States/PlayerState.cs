using System.Collections.Generic;
using Data;
using Generic;
using UnityEngine;

namespace States
{
    public class PlayerState : AgentState, IInteracting, IMoving, IPosition, IInventory
    {
        [SerializeField] private float interactRange;
        [SerializeField] private float attackDuration;
        [SerializeField] private float speed;
        [SerializeField] private float interactCooldown;
        [SerializeField] private List<Item> startInventory;

        public Vector2? MovementPosition { get; set; }
        public bool IsCoolingDown { get; set; }
        public IInteractable AttackTarget { get; set; }
        public Vector2 Position { get; set; }

        public List<Item> Inventory { get; } = new();
        public List<Item> StartInventory => startInventory;

        public float AttackDuration => attackDuration;
        public float InteractRange => interactRange;
        public float InteractCooldown => interactCooldown;
        public float Speed => speed;
        
        public override AgentState Copy()
        {
            return new PlayerState
            {
                speed = speed,
                interactRange = interactRange,
                interactCooldown = interactCooldown,
                attackDuration = attackDuration,
                startInventory = startInventory,
                MovementPosition = null,
                AttackTarget = null,
                IsCoolingDown = false,
                Position = Vector2.zero,
            };
        }
    }
}