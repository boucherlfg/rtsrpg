using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Felix/Animation Config")]
    public class AnimationConfig : ScriptableObject
    {
        public float maxRotation = 8f;
        public float walkAnimationSpeed = 10f;
        public float attackAnimationDuration = 0.2f;
    }
}