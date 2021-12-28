using System.Collections;
using System.Linq;
using UnityEngine;

public class WolfController : MonoBehaviour, IDamageable
{
    [SerializeField] float hungerMax = 100f;
    [SerializeField] float hungerCurrent;
    [SerializeField] private float hungerDecrease = 1f;

    [SerializeField] private float radius;
    [SerializeField] private float attackRadius;

    private Agent agent;
    private Wander wander;
    private Seek seek;

    private float velocity;

    void Start()
    {
        agent = gameObject.GetComponent<Agent>();
        wander = gameObject.GetComponent<Wander>();
        seek = gameObject.GetComponent<Seek>();

        hungerCurrent = hungerMax;

        velocity = agent.VelocityLimit;
    }

    void Update()
    {
        HandleAnimalsChasing();
        HandleHunger();
    }

    private void HandleHunger()
    {
        hungerCurrent = Mathf.Clamp(hungerCurrent - (hungerDecrease * Time.deltaTime), 0.0f, hungerMax);

        if (hungerCurrent <= 0)
        {
            Damage();
        }
    }

    private void HandleAnimalsChasing()
    {
        if (GetClosestAnimal() != null)
        {
            seek.ObjectToFollow = GetClosestAnimal().transform;
            seek.Weight = 3;
            wander.Weight = 0;

            agent.VelocityLimit = velocity * 1.5f;

            if ((GetClosestAnimal().transform.position - transform.position).magnitude < attackRadius)
            {
                GetClosestAnimal().transform.root.GetComponent<IDamageable>()?.Damage();
                hungerCurrent = hungerMax;
            }
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

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    public void Damage()
    {
        Destroy(gameObject);
    }
}
