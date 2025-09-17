
namespace States
{
    public interface IInteracting
    {
        public bool IsCoolingDown { get; set; }
        public IInteractable AttackTarget { get; set; }
        
        public float AttackDuration { get; }
        public float InteractRange { get; }
        public float InteractCooldown { get; }
    }
}