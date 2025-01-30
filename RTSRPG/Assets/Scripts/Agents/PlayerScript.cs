using System.Collections.Generic;
using System.Linq;
using ProcGen;
using UnityEngine;

namespace Agents
{
    public class PlayerScript : Agent
    {
        private Vector2? _destination;
        private Camera _mainCam;
        private Agent _attackTarget;
        private List<Item> _targetInventory = new();
        private float _attackTimer;
    
        private Inventory _inventory;
        [SerializeField] private float speed;
        [SerializeField] private float attackInterval = 1;
        [SerializeField] private float attackDamage = 1;
        [SerializeField] private float attackRange = 0.5f;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _mainCam = Camera.main;
            _inventory = GetComponent<Inventory>();
        }

        // Update is called once per frame
        void Update()
        {
            HandleInput();
            if(_destination.HasValue) HandleMove();
            else if (_attackTarget) HandleAttack();
            else if (_targetInventory != null) HandleHarvest(); 
        }

        void HandleHarvest()
        {
            _inventory.items.AddRange(_targetInventory);
            _targetInventory = null;
        }
        
        void HandleMove()
        {
            var direction = _destination.Value - (Vector2)transform.position;
            if (direction.magnitude < attackRange) return;
        
            transform.position += (Vector3)direction.normalized * (Time.deltaTime * speed);
        }

        void HandleAttack()
        {
            Vector2 direction = _attackTarget.transform.position - transform.position;
            if (direction.magnitude < attackRange)
            {
                Attack();
                return;
            }
        
            transform.position += (Vector3)direction.normalized * (Time.deltaTime * speed);
            return;
        
            void Attack()
            {
                _attackTimer += Time.deltaTime;
                if (_attackTimer < attackInterval) return;
            
                var lifeAndDeath = _attackTarget.GetComponent<LifeAndDeath>();
                lifeAndDeath.Health -= attackDamage;
                _attackTimer = 0;
            }
        }

        private void HandleInput()
        {
            var pos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButton(0))
            {
                if (!MapGenerator.Instance.Bounds.Contains(pos)) return;
                _attackTarget = null;
                _destination = pos;
            }
            else if (Input.GetMouseButton(1))
            {
                var hit = Physics2D.OverlapCircleAll(pos, 0.5f);
                if (!hit.Any()) return;

                var agents = hit.Where(x => x.GetComponent<Agent>() && x.gameObject != gameObject);
                if(!agents.Any()) return;
            
                var closest = agents.OrderBy(x => Vector2.Distance(x.transform.position, pos)).First();
                _destination = null;
                _attackTarget = closest.GetComponent<Agent>();
                var inventory = _attackTarget.GetComponent<Inventory>();
                _targetInventory = inventory ? inventory.items : new List<Item>();
            }
        }
    }
}
