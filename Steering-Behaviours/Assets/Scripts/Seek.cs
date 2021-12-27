using UnityEngine;

public class Seek : MonoBehaviour
{
    [SerializeField] private float Mass = 15;
    [SerializeField] private float MaxVelocity = 3;
    [SerializeField] private float MaxForce = 15;

    private Vector3 velocity;
    [SerializeField] private Transform target;

    private void Start()
    {
        velocity = Vector3.zero;
    }

    private void Update()
    {
        var desiredVelocity = target.transform.position - transform.position;
        desiredVelocity = desiredVelocity.normalized * MaxVelocity;

        var steering = desiredVelocity - velocity;
        steering = Vector3.ClampMagnitude(steering, MaxForce);
        steering /= Mass;

        velocity = Vector3.ClampMagnitude(velocity + steering, MaxVelocity);
        transform.position += velocity * Time.deltaTime;
        transform.forward = velocity.normalized;

        Debug.DrawRay(transform.position, velocity.normalized * 2, Color.green);
        Debug.DrawRay(transform.position, desiredVelocity.normalized * 2, Color.magenta);
    }
}
