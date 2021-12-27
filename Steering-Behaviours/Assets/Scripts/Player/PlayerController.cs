using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float force = 5;

    private Agent vehicle;

    private void Awake()
    {
        vehicle = GetComponent<Agent>();
    }

    private void Update()
    {
        vehicle.ApplyForce(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * force);
    }
}
