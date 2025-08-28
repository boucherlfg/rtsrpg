using System.Threading.Tasks;
using Core;
using States;
using UnityEngine;

namespace Agent
{
    public class AgentAnimator : MonoBehaviour
    {
        const string Idle = nameof(Idle);
        const string Walk = nameof(Walk);
        
        private Vector2 _lastPosition;
        private Animator _animator;
        
        private AgentState _currentState;
        private IAttacking _attacking;
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _lastPosition = transform.position;
        }

        public void Initialize(AgentState state)
        {
            if (state is not IAttacking attacking) return;
            
            _attacking = attacking;
            if (_attacking == null) return;
            
            _currentState = state;
            _attacking.AttackingStateChanged.Subscribe(HandleAttack);
        }

        public void Uninitialize(AgentState state)
        {
            _attacking.AttackingStateChanged.Unsubscribe(HandleAttack);
            _currentState = null;
            _attacking = null;
        }

        private void HandleAttack()
        {
            if (!_attacking.IsAttacking) return;
            _ = AttackAnimation(_attacking.AttackTarget);
        }
        
        private void Update()
        {
            var velocity = ((Vector2)transform.position - _lastPosition) / Time.deltaTime;
            _lastPosition = transform.position;
            _animator.Play(velocity.magnitude > 0.01f ? Walk : Idle);
        }

        private async Task AttackAnimation(Vector2 targetPoint)
        {
            var attackDuration = _attacking.AttackDuration;
            
            Debug.Log("attack starts");
            _animator.Play(Idle);
            Vector2 startPosition = transform.position;
            for (float i = 0; i < 1f; i += 2 * Time.deltaTime / attackDuration)
            {
                Debug.Log($"{i} seconds in");
                transform.position = Vector2.Lerp(startPosition, targetPoint, i);
                await Task.Yield();
            }

            transform.position = targetPoint;
            
            for (float i = 0; i < 1f; i += 2 * Time.deltaTime / attackDuration)
            {
                Debug.Log($"{i} seconds out");
                transform.position = Vector2.Lerp(targetPoint, startPosition, i);
                await Task.Yield();
            }
            transform.position = startPosition;

            _attacking.IsAttacking = false;
        }
    }
}
