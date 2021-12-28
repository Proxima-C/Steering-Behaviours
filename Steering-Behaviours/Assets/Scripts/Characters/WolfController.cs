using System.Linq;
using UnityEngine;

public class WolfController : MonoBehaviour, IDamageable
{
    [SerializeField] private float radius;

    private Agent agent;
    private Wander wander;
    private Seek seek;

    private float velocity;

    void Start()
    {
        agent = gameObject.GetComponent<Agent>();
        wander = gameObject.GetComponent<Wander>();
        seek = gameObject.GetComponent<Seek>();

        velocity = agent.VelocityLimit;
    }

    void Update()
    {
        HandleAnimalsChasing();
    }

    private void HandleAnimalsChasing()
    {
        if (GetClosestAnimal() != null)
        {
            seek.ObjectToFollow = GetClosestAnimal().transform;
            seek.Weight = 3;
            wander.Weight = 0;

            agent.VelocityLimit = velocity * 1.5f;
        }
        else
        {
            seek.ObjectToFollow = transform;
            seek.Weight = 0;
            wander.Weight = 3;

            agent.VelocityLimit = velocity;
        }
    }

    private Collider GetClosestAnimal()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        Collider nearestCollider = null;
        float minSqrDistance = Mathf.Infinity;

        string[] tags = { "Prey", "Player" };

        for (int i = 0; i < colliders.Length; i++)
        {
            float sqrDistanceToCenter = (transform.position - colliders[i].transform.position).sqrMagnitude;
            if (sqrDistanceToCenter < minSqrDistance && colliders[i].transform.root != transform
                && tags.Contains(colliders[i].transform.root.tag))
            {
                minSqrDistance = sqrDistanceToCenter;
                nearestCollider = colliders[i];
            }
        }

        return nearestCollider;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void Damage()
    {
        Destroy(gameObject);
    }
}
