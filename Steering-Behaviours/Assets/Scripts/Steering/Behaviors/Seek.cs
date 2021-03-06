using UnityEngine;

public class Seek : AgentBehavior
{
    [SerializeField]
    private Transform objectToFollow;

    [SerializeField, Range(0, 10)]
    private float arriveRadius;

    public Transform ObjectToFollow { get => objectToFollow; set => objectToFollow = value; }

    public override Vector3 GetDesiredVelocity()
    {
        var distance = (objectToFollow.position - transform.position);
        float k = 1;
        if (distance.magnitude < arriveRadius)
        {
            k = distance.magnitude / arriveRadius;
        }

        return distance.normalized * Agent.VelocityLimit * k;
    }
}
