using System.Linq;
using Events;
using Generic;
using States;
using UnityEngine;

namespace Systems
{
    public class PlayerSystem : GameSystem<PlayerState>
    {
        private Camera _mainCamera;
        private OnPositionChanged _onPositionChanged;
        private OnInteracted _onInteracted;
        
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
            _onPositionChanged.Invoke((state, state.Position));
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

            if (state.AttackTarget is not AgentState target) return;
            if (target.shouldUninitialize) return;
            if (state.IsCoolingDown) return;

            // get direction and movement quantity
            var direction = state.AttackTarget.Position - state.Position;
            var movement = state.Speed * Time.deltaTime;
            
            // if we're in range, interact with
            if (direction.magnitude + movement < state.InteractRange)
            {
                _onInteracted.Invoke((state, target));
                state.IsCoolingDown = true;
                _ = Extensions.Interpolate(state.InteractCooldown, null, () =>
                {
                    state.IsCoolingDown = false;
                });
            }
            else 
            {
                state.Position += direction.normalized * movement;
                _onPositionChanged.Invoke((state, state.Position));
            }
        }

        protected override void UpdateOneState(PlayerState agent)
        {
            HandleMove(agent);
            HandleInteract(agent);
        }

        protected override void InitializeState(PlayerState agent)
        {
            _mainCamera = Camera.main;
        }

        protected override void UninitializeState(PlayerState agent)
        {
            // nothing
        }

        protected override void InitializeSystem()
        {
            _onPositionChanged = ServiceManager.Instance.Get<OnPositionChanged>();
            _onInteracted = ServiceManager.Instance.Get<OnInteracted>();
        }

        protected override void UninitializeSystem()
        {
            // nothin
        }
    }
}
