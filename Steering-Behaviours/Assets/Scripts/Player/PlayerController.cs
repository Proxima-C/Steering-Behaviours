using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private float playerSpeed = 2.0f;
    [SerializeField] private int bulletsCount = 10;

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform shootOrigin;
    [SerializeField] private float launchVelocity = 700f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
        Shoot();
    }

    private void Move()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1") && bulletsCount > 0)
        {
            GameObject bullet = Instantiate(projectile, shootOrigin.position, shootOrigin.rotation);
            bullet.GetComponentInChildren<Rigidbody>().AddForce(shootOrigin.forward * launchVelocity);
            bulletsCount--;
        }
    }
}
