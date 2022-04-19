using UnityEngine;

public class Agent : MonoBehaviour
{
    private Vector3 velocity;

    private Vector3 acceleration;

    [SerializeField]
    private float mass = 1;

    [SerializeField, Range(1, 20)]
    private float velocityLimit = 3;

    [SerializeField, Range(1, 50)]
    private float steeringForceLimit = 5;

    private const float Epsilon = 0.05f;

    public Vector3 Velocity => velocity;

    public float VelocityLimit { get => velocityLimit; set => velocityLimit = value; }

    public void ApplyForce(Vector3 force)
    {
        force /= mass;
        acceleration += force;
    }

    private void Update()
    {
        ApplyFriction();

        ApplySteeringForce();

        ApplyForces();

        void ApplyFriction()
        {
            var friction = -velocity.normalized * 0.5f;
            ApplyForce(friction);
        }

        void ApplySteeringForce()
        {
            var providers = GetComponents<AgentBehavior>();
            var steering = Vector3.zero;
            foreach (var provider in providers)
            {
                var desiredVelocity = provider.GetDesiredVelocity() * provider.Weight; //
                steering += desiredVelocity - velocity;

            }
            ApplyForce(Vector3.ClampMagnitude(steering - velocity, steeringForceLimit));
        }

        void ApplyForces()
        {
            velocity += acceleration * Time.deltaTime;
            //limit velocity
            velocity = Vector3.ClampMagnitude(velocity, velocityLimit);

            //on small values object might start to blink, so we considering 
            //small velocities as zeroes
            if (velocity.magnitude < Epsilon)
            {
                velocity = Vector3.zero;
                return;
            }

            transform.position += velocity * Time.deltaTime;
            acceleration = Vector3.zero;
            transform.rotation = Quaternion.LookRotation(velocity);
        }
    }
}
