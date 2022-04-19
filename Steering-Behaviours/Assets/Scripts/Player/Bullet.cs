using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var animal = other.transform.root.GetComponent<IDamageable>();
        if (animal != null)
        {
            animal.Damage();
        }
        Debug.Log(other.transform.root.name);
        Destroy(gameObject);
    }

    void Start()
    {
        Destroy(gameObject, 5);
    }
}
