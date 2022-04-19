using UnityEngine;

public class Wander : AgentBehavior
{
    [SerializeField, Range(0.5f, 5)]
    private float circleDistance = 1;

    [SerializeField, Range(0.5f, 5)]
    private float circleRadius = 2;

    [SerializeField, Range(1, 80)]
    private int angleChangeStep = 15;

    private int angle = 0;

    public override Vector3 GetDesiredVelocity()
    {
        var rnd = Random.value;
        if (rnd < 0.5)
        {
            angle += angleChangeStep;
        }
        else if (rnd < 1)
        {
            angle -= angleChangeStep;
        }

        var futurePos = Agent.transform.position + Agent.Velocity.normalized * circleDistance;
        var vector = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * circleRadius;

        return (futurePos + vector - transform.position).normalized * Agent.VelocityLimit;
    }
}
