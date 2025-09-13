namespace States
{
    public interface IInteractable : IPosition
    {
        GenericEvent<AgentState> Interact { get; }
        bool ShouldContinueInteracting { get; set; }
    }
}