using UnityEngine;

public abstract class AgentBehavior : MonoBehaviour
{
    [SerializeField, Range(0, 3)]
    private float weight = 1f;

    protected Agent Agent;

    public float Weight { get => weight; set => weight = value; }

    private void Awake()
    {
        Agent = GetComponent<Agent>();
    }

    public abstract Vector3 GetDesiredVelocity();
}
