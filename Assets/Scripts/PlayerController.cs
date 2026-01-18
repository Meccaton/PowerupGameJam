using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;

    public int maxHealth = 5;
    public int health;

    public int bulletDamage = 1;
    public float bulletSpeed = 10f;
    public float fireRate = 1.0f;
    private float shootCooldown;
    private float shootCooldownTimestamp;

    public GameObject bullet;

    private bool alive;
    private float deadTime = 5f;
    private float restartTimer;

    void Start()
    {
        alive = true;
        shootCooldown = 1.0f/fireRate;
        health = maxHealth;
    }

    void Update()
    {
        if (alive)
        {
            MovementController();
            ShooterController();
        }
        else
        {
            if(Time.time > restartTimer)
            {
                // Restart Here
            }
        }
    }

    private void MovementController()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    private void ShooterController()
    {
        if (Input.GetMouseButton(0))
        {
            if(Time.time < shootCooldownTimestamp)
            {
                return;
            }
            else
            {
                shootCooldownTimestamp = Time.time + shootCooldown;

                Vector3 shootDirection = Input.mousePosition;
                shootDirection.z = 0.0f;
                shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
                shootDirection -= transform.position;

                GameObject b = Instantiate(bullet, transform.position, Quaternion.identity);
                PlayerBulletBehavior pbb = b.GetComponent<PlayerBulletBehavior>();
                if (pbb != null)
                {
                    pbb.Initialize(bulletDamage);
                }

                Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
                Vector2 bulletDirection = (Vector2)shootDirection.normalized;
                Vector2 velocity = bulletDirection * bulletSpeed;
                rb.linearVelocity = velocity;
            }
        }
    }

    public void GetHit(int dmg)
    {
        health -= dmg;
        Debug.Log("Health: " + health);

        if(health <= 0 && alive)
        {
            alive = false;
            Debug.Log("Alive = " + alive);
            restartTimer = Time.time + deadTime;
        }
    }
}
