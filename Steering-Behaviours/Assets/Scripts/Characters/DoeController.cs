using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoeController : MonoBehaviour, IDamageable
{
    [SerializeField] private float radius;

    private Agent agent;
    private Wander wander;
    private Flee flee;

    private float velocity;

    void Start()
    {
        agent = gameObject.GetComponent<Agent>();
        wander = gameObject.GetComponent<Wander>();
        flee = gameObject.GetComponent<Flee>();

        velocity = agent.VelocityLimit;
    }

    void Update()
    {
        HandleAnimalsAvoidance();
    }

    private void HandleAnimalsAvoidance()
    {
        if (GetClosestAnimal() != null)
        {
            flee.ObjectToFlee = GetClosestAnimal().transform;
            flee.Weight = 2;
            wander.Weight = 1;

            agent.VelocityLimit = velocity * 1.25f;
        }
        else
        {
            flee.ObjectToFlee = transform;
            flee.Weight = 0;
            wander.Weight = 2;

            agent.VelocityLimit = velocity;
        }
    }

    private Collider GetClosestAnimal()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        Collider nearestCollider = null;
        float minSqrDistance = Mathf.Infinity;

        string[] tags = { "Predator", "Player" };

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
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void Damage()
    {
        Destroy(gameObject);
    }
}
