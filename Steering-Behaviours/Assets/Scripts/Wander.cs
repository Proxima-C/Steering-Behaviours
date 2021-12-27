using UnityEngine;

public class Wander : MonoBehaviour
{
    [SerializeField] private float Mass = 15;
    [SerializeField] private float MaxVelocity = 3;
    [SerializeField] private float MaxForce = 15;

    [SerializeField] private float CircleRadius = 1;
    [SerializeField] private float TurnChance = 0.05f;
    [SerializeField] private float MaxRadius = 5;

    private Vector3 velocity;
    private Vector3 target;
    private Vector3 wanderForce;

    private void Start()
    {
        velocity = Random.onUnitSphere;
        wanderForce = GetRandomWanderForce();
    }

    private void Update()
    {
        var desiredVelocity = GetWanderForce();
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

    private Vector3 GetWanderForce()
    {
        if (transform.position.magnitude > MaxRadius)
        {
            var directionToCenter = (target - transform.position).normalized;
            wanderForce = velocity.normalized + directionToCenter;
        }
        else if (Random.value < TurnChance)
        {
            wanderForce = GetRandomWanderForce();
        }

        return wanderForce;
    }

    private Vector3 GetRandomWanderForce()
    {
        var circleCenter = velocity.normalized;
        var randomPoint = Random.insideUnitCircle;

        var displacement = new Vector3(randomPoint.x, randomPoint.y) * CircleRadius;
        displacement = Quaternion.LookRotation(velocity) * displacement;

        var wanderForce = circleCenter + displacement;
        return wanderForce;
    }
}
