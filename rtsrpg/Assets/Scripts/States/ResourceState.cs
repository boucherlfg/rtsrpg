using UnityEngine;

namespace States
{
    public class ResourceState : AgentState, IInteractable, IDurable
    {
        [SerializeField] private int maxHealth;
        private int _currentHealth;
        public Vector2 Position { get; set; }
        public int MaxHealth => maxHealth;
        public int CurrentHealth
        {
            get => _currentHealth;
            set => _currentHealth = Mathf.Clamp(value, 0, maxHealth);
        }
    }
}
