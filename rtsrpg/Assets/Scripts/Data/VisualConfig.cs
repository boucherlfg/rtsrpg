using UI;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "VisualConfig", menuName = "Felix/VisualConfig")]
    public class VisualConfig : ScriptableObject
    {
        [SerializeField] private FloatingImage floatingImage;
        [SerializeField] private FloatingText floatingText;
        [SerializeField] private GameObject destroyParticle;
        
        public FloatingImage FloatingImage => floatingImage;
        public FloatingText FloatingText => floatingText;
        public GameObject DestroyParticle => destroyParticle;
        public float floatingTextDelay = 0.25f;
    }
}