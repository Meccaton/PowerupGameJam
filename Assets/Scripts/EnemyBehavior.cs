using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float range = 10.0f;
    public float moveSpeed = 10.0f;

    public float bulletSpeed = 1.0f;
    public float fireRate = 1.0f;
    private float shootCooldown;
    private float shootCooldownTimestamp;
    private float shootRange;

    public GameObject bullet;

    private Transform pc;

    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").transform;

        if(pc == null)
        {
            Destroy(gameObject);
        }

        shootCooldown = 1.0f / fireRate;
        shootRange = range * 2.0f;
    }

    void Update()
    {
        MoveController();
        ShootController();
    }

    private void MoveController()
    {
        if (Vector2.Distance(pc.transform.position, gameObject.transform.position) > range)
        {
            Vector2 testMove = Vector2.MoveTowards(gameObject.transform.position, pc.transform.position, moveSpeed * Time.deltaTime);
            gameObject.transform.position = testMove;
        }
    }

    private void ShootController()
    {
        if(Vector2.Distance(pc.transform.position, gameObject.transform.position) <= shootRange)
        {
            if (Time.time < shootCooldownTimestamp)
            {
                return;
            }
            else
            {
                shootCooldownTimestamp = Time.time + shootCooldown;

                //Vector3 shootDirection = pc.transform.position;
                //shootDirection.z = 0.0f;
                //shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
                //shootDirection -= transform.position;

                Vector2 aimDirection = (pc.position - transform.position).normalized;

                GameObject b = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));

                Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
                Vector2 bulletDirection = aimDirection.normalized;
                Vector2 velocity = bulletDirection * bulletSpeed;
                rb.linearVelocity = velocity;
            }
        }
    }
}
