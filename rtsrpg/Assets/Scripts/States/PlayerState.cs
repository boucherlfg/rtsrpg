using UnityEngine;

namespace States
{
    public class PlayerState : AgentState, IInteracting, IMoving, IPosition
    {
        [SerializeField] private float interactRange = 1f;
        [SerializeField] private float attackDuration;
        [SerializeField] private float speed;
        
        public Vector2? MovementPosition { get; set; }
        public bool IsAttacking { get; set; }
        public IInteractable AttackTarget { get; set; }
        public float AttackDuration => attackDuration;
        public float InteractRange => interactRange;
        public float Speed => speed;
        public Vector2 Position { get; set; }

        public GenericEvent PositionChanged { get; } = new();
        public GenericEvent AttackingStateChanged { get; } = new();
    }
}