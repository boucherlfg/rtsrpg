using UnityEngine;

namespace States
{
    public class PlayerState : AgentState, IAttacking, IMoving, IPosition
    {
        [SerializeField] private float attackDuration;
        [SerializeField] private float speed;
        
        public bool IsAttacking { get; set; }
        public Vector2 AttackTarget { get; set; }
        public float AttackDuration => attackDuration;
        public float Speed => speed;
        public Vector2 Position { get; set; }

        public GenericEvent PositionChanged { get; } = new();
        public GenericEvent AttackingStateChanged { get; } = new();
    }
}