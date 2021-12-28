using UnityEngine;

public class Flee : AgentBehavior
{
    [SerializeField]
    private Transform objectToFlee;

    public Transform ObjectToFlee { get => objectToFlee; set => objectToFlee = value; }

    public override Vector3 GetDesiredVelocity()
    {
        return -(objectToFlee.position - transform.position).normalized * Agent.VelocityLimit;
    }
}
