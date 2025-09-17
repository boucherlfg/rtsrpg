using UnityEngine;

namespace States
{
    public class PlayerState : AgentState, IInteracting, IMoving, IPosition
    {
        [SerializeField] private float interactRange = 1f;
        [SerializeField] private float attackDuration;
        [SerializeField] private float speed;
        [SerializeField] private float interactCooldown;
        private bool _isCoolingDown;
        public Vector2? MovementPosition { get; set; }
        public bool IsCoolingDown { get => _isCoolingDown; set => _isCoolingDown = value; }
        public IInteractable AttackTarget { get; set; }
        public Vector2 Position { get; set; }
        
        
        public float AttackDuration => attackDuration;
        public float InteractRange => interactRange;
        public float InteractCooldown => interactCooldown;
        public float Speed => speed;
        

    }
}