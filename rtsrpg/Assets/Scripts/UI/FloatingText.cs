using UnityEngine;

namespace UI
{
    public class FloatingText : MonoBehaviour
    {
        [SerializeField] private float lifeTime = 1f;
        [SerializeField] private TMPro.TMP_Text label;

        public string Text
        {
            get => label.text;
            set => label.text = value;
        }

        private void Start()
        {
            _ = Extensions.Interpolate(lifeTime, progress =>
            {
                transform.position += (Vector3)Vector2.up * (Time.deltaTime);
                
                var alpha = Mathf.Lerp(0f, 1f, 1 - Mathf.Pow(progress, 10f));
                var color = label.color;
                color.a = alpha;
                label.color = color;
            }, () =>
            {
                Destroy(gameObject);
            }, x => Mathf.Pow(x, 0.1f));
        }
    }
}
