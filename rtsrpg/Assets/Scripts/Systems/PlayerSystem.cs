using System;
using Core;
using States;
using UnityEngine;

namespace Systems
{
    public class PlayerSystem : GameSystem
    {
        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        public void Update()
        {
            var world = ServiceManager.Instance.Get<WorldState>();
            var states = world.GetAll<PlayerState>();
            foreach (var (index, state) in states)
            {
                if(state.IsAttacking) continue;
                
                var move = Input.GetAxisRaw("Horizontal") * Vector2.right 
                           + Input.GetAxisRaw("Vertical") * Vector2.up;
                state.Position += move.normalized * (state.Speed * Time.deltaTime);
                state.PositionChanged.Invoke();
                
                if (!Input.GetMouseButtonUp(0)) continue;
                state.AttackTarget = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                state.IsAttacking = true;
                state.AttackingStateChanged.Invoke();
            }
        }
    }
}
