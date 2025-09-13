using UnityEngine;

namespace States
{
    public class TestState : AgentState, IInteractable
    {
        public Vector2 Position { get; set; }
        public GenericEvent PositionChanged { get; } = new();
        public GenericEvent<AgentState> Interact { get; } = new();
        
        public bool ShouldContinueInteracting { get; set; }
    }
}
