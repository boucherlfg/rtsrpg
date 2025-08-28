using Generic;
using UnityEngine;

namespace States
{
    public interface IAttacking
    {
        public GenericEvent AttackingStateChanged { get; }
        public bool IsAttacking { get; set; }
        public Vector2 AttackTarget { get; set; }
        public float AttackDuration { get; }
    }
}