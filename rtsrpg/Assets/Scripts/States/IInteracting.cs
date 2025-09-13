
namespace States
{
    public interface IInteracting
    {
        public GenericEvent AttackingStateChanged { get; }
        public IInteractable AttackTarget { get; set; }
        public float AttackDuration { get; }
        public float InteractRange { get; }
    }
}