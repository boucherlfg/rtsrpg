using UnityEngine;
using UnityEngine.Serialization;

public class WalkAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform target;

    private Vector2 _lastPosition;
    private Vector2 _lastVelocity;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var velocity = ((Vector2)transform.position - _lastPosition) / Time.deltaTime;
        _lastPosition = transform.position;
        
        if (velocity.magnitude > 1E-5)
        {
            _lastPosition = transform.position;
            _lastVelocity = velocity;
            animator.Play("Walk");
        }
        else
        {
            animator.Play("Idle");
        }

        var scale = target.localScale;
        scale.x = _lastVelocity.x >= 0.1 ? 1 : -1;
        target.localScale = scale;
    }
}
