using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, IDamageable
{
    private CharacterController controller;
    [SerializeField] float maxLivesCount = 3f;
    [SerializeField] float currentLivesCount;
    [SerializeField] private int bulletsCount = 10;

    private Vector3 playerVelocity;
    private float playerSpeed = 2.0f;

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform shootOrigin;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float launchVelocity = 700f;

    public int BulletsCount { get => bulletsCount; set => bulletsCount = value; }
    public float CurrentLivesCount { get => currentLivesCount; set => currentLivesCount = value; }

    public EventHandler OnBulletsCountChange;
    public EventHandler OnLivesCountChange;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        currentLivesCount = maxLivesCount;
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
            OnBulletsCountChange?.Invoke(this, EventArgs.Empty);
        }
    }

    public void Damage()
    {
        currentLivesCount--;
        GetComponent<CharacterController>().enabled = false;
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
        GetComponent<CharacterController>().enabled = true;

        OnLivesCountChange?.Invoke(this, EventArgs.Empty);

        if (currentLivesCount <= 0)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
