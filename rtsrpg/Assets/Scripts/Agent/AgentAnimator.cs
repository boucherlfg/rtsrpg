using System.Threading.Tasks;
using Core;
using States;
using UnityEngine;

namespace Agent
{
    public class AgentAnimator : MonoBehaviour
    {
        private const string Idle = nameof(Idle);
        private const string Walk = nameof(Walk);
        
        private Vector2 _lastPosition;
        private Animator _animator;
        
        private IInteracting _interacting;
       
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _lastPosition = transform.position;
        }

        private void Update()
        {
            var velocity = ((Vector2)transform.position - _lastPosition) / Time.deltaTime;
            _lastPosition = transform.position;
            _animator.Play(velocity.magnitude > 0.01f ? Walk : Idle);
        }
    }
}
