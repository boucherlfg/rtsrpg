using System.Collections.Generic;
using System.Linq;
using ProcGen;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Agents
{
    public class Rabbit : Agent
    {
        private Vector2? _wanderDestination;
        private Agent _hungerTarget;
        public float Speed => _fleeing ? fleeingSpeed : restingSpeed;
        public float Range => _fleeing ? fleeingRange : restingRange;
    
        private bool _fleeing = false;
        private float _hunger = 0;
        private IEnumerable<Agent> _proximity;
    
        [SerializeField][EnumMask] private AgentType shouldFlee;
        [SerializeField][EnumMask] private AgentType shouldAttack;
    
        [SerializeField] private float hungerTreshold = 10;
        [SerializeField] private float hungerGainedByEating = 50;
    
        [SerializeField] private float restingSpeed = 2;
        [SerializeField] private  float restingRange = 10;
    
        [SerializeField] private  float fleeingSpeed = 5;
        [SerializeField] private  float fleeingRange = 15;

        [SerializeField] private  float visionRange = 5;
        [SerializeField] private float attackRange = 0.5f;
        [SerializeField] private float attackInterval = 1;
        [SerializeField] private float attackDamage = 1;
        private float _attackTimer = 0;
        private void Update()
        {
            _proximity = Physics2D.OverlapCircleAll(transform.position, visionRange)
                .Select(x => x.GetComponent<Agent>());
            HandleFleeing();
            HandleHunger();
            if(_fleeing) Wander();
            else if(_hunger > hungerTreshold) Eat();
        }

        void HandleFleeing()
        {
            _fleeing = false;
            foreach (var agent in _proximity)
            {
                if (agent == null) continue;
                if (!shouldFlee.HasFlag(agent.agentType)) continue;

                _fleeing = true; 
            }
        }

        void HandleHunger()
        {
            _hunger += Time.deltaTime;
        }
    
        void Wander()
        {
            _wanderDestination ??= GetWanderDestination();

            if (Vector2.Distance(_wanderDestination.Value, transform.position) <= attackRange)
            {
                _wanderDestination = GetWanderDestination();
            }
        
            var direction = _wanderDestination.Value - (Vector2)transform.position;
            transform.position += (Vector3)direction.normalized * (Speed * Time.deltaTime);
        }

        void Eat()
        {
            _hungerTarget ??= GetHungerTarget();

            if (!_hungerTarget)
            {
                Wander();
            }
            else if (Vector2.Distance(_hungerTarget.transform.position, transform.position) <= attackRange)
            {
                Attack();
            }
            else
            {
                var direction = _hungerTarget.transform.position - transform.position;
                transform.position += direction.normalized * (Speed * Time.deltaTime);
            }
        }

        void Attack()
        {
            if(!_hungerTarget) return;
            _attackTimer += Time.deltaTime;
            if (_attackTimer < attackInterval) return;
            
            var lifeAndDeath = _hungerTarget.GetComponent<LifeAndDeath>();
            lifeAndDeath.Health -= attackDamage;
            _attackTimer = 0;
            
            if (_hungerTarget) return;
            _hunger += hungerGainedByEating;
        }

        private Vector2 GetWanderDestination()
        {
            var dest = transform.position + (Vector3)Random.insideUnitCircle * Range;
            while (!MapGenerator.Instance.Bounds.Contains(dest))
            {
                dest = transform.position + (Vector3)Random.insideUnitCircle * Range;
            }

            return dest;
        }

        private Agent GetHungerTarget()
        {
            var grass = _proximity.Where(x => x && shouldAttack.HasFlag(x.agentType));
            if(!grass.Any()) return null;
            var target = grass.OrderBy(x => Vector2.Distance(transform.position, x.transform.position)).FirstOrDefault();
            return target;
        }
    }
}