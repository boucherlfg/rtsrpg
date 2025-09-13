using System.Linq;
using States;
using UnityEngine;

namespace Systems
{
    public class PlayerSystem : GameSystem<PlayerState>
    {
        private Camera _mainCamera;
        
        protected override void Awake()
        {
            base.Awake();
            _mainCamera = Camera.main;
        }
        
        private void HandleMove(PlayerState state)
        {
            if (Input.GetMouseButtonUp(0))
            {
                state.AttackTarget = null;
                state.MovementPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            }

            if (state.MovementPosition == null) return;
                
            var direction = state.MovementPosition.Value - state.Position;
            var movement = state.Speed * Time.deltaTime;
            if (direction.magnitude < movement)
            {
                state.Position = state.MovementPosition.Value;
                state.MovementPosition = null;
                return;
            }
                
            state.Position += direction.normalized * movement;
            state.PositionChanged.Invoke();
        }

        private void HandleInteract(PlayerState state)
        {
            if (Input.GetMouseButtonUp(1))
            {
                state.MovementPosition = null;
                var clickTarget = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                
                var choice = World.GetAll<IInteractable>(x =>
                    Vector2.Distance(clickTarget, x.Position) < state.InteractRange).ToArray();
                
                if (!choice.Any()) return;
                state.AttackTarget = choice.OrderBy(x => Vector2.Distance(clickTarget, x.Position)).First();
            }

            if (state.AttackTarget == null) return;
            
            var direction = state.AttackTarget.Position - state.Position;
            var movement = state.Speed * Time.deltaTime;
            if (direction.magnitude + movement < state.InteractRange)
            {
                state.AttackTarget.Interact.Invoke(state);
                state.AttackTarget = null;
                return;
            }
            
            state.Position += direction.normalized * movement;
            state.PositionChanged.Invoke();
        }

        protected override void UpdateOneState(PlayerState agent)
        {
            HandleMove(agent);
            HandleInteract(agent);
        }

        protected override void Initialize(PlayerState agent)
        {
            
        }

        protected override void Uninitialize(PlayerState agent)
        {
            
        }
    }
}
