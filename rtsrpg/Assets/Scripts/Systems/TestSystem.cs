using States;
using UnityEngine;

namespace Systems
{
    public class TestSystem : GameSystem<TestState>
    {
        protected override void UpdateOneState(TestState agent)
        {
        }

        protected override void Initialize(TestState agent)
        {
            if (agent is IInteractable interactable)
            {
                interactable.Interact.Subscribe(HandleInteract);
            }
        }

        private void HandleInteract(AgentState obj)
        {
            Debug.Log("interacted with " + nameof(TestSystem));
        }

        protected override void Uninitialize(TestState agent)
        {
        }
    }
}
