using UnityEngine;

namespace UI
{
    public class FloatingImage : MonoBehaviour
    {
        [SerializeField] private float lifeTime = 1f;
        [SerializeField] private SpriteRenderer rend;

        public Sprite Image
        {
            get => rend.sprite;
            set => rend.sprite = value;
        }

        private void Start()
        {
            _ = Extensions.Interpolate(lifeTime, progress =>
            {
                transform.position += (Vector3)Vector2.up * (Time.deltaTime);
                
                var alpha = Mathf.Lerp(0f, 1f, 1 - Mathf.Pow(progress, 10f));
                var color = rend.color;
                color.a = alpha;
                rend.color = color;
            }, () =>
            {
                Destroy(gameObject);
            }, x => Mathf.Pow(x, 0.1f));
        }
    }
}