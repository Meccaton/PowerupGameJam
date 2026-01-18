using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float range = 10.0f;
    public float moveSpeed = 10.0f;

    public int maxHealth = 5;
    public int health;
    private Vector3 size;

    public float bulletSpeed = 1.0f;
    public float fireRate = 1.0f;
    private float shootCooldown;
    private float shootCooldownTimestamp;
    public float shootRange = 10.0f;
    public int bulletDamage = 1;
    public float bulletDuration = 5.0f;

    private int numChanges;
    private float scaleModifier;

    public GameObject bullet;

    private Transform pc;
    private GameObject player;

    public void Initialize(float r, float ms, int mh, float bs, float fr, float sr, int bd, float bdur, int nc)
    {
        range = r;
        moveSpeed = ms;
        maxHealth = mh;
        bulletSpeed = bs;
        fireRate = fr;
        shootRange = sr;
        bulletDamage = bd;
        bulletDuration = bdur;
        numChanges = nc;
    }
    
    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").transform;

        if(pc == null)
        {
            Destroy(gameObject);
        }

        health = maxHealth;
        shootCooldown = 1.0f / fireRate;

        float scaleModifierModifier = Random.Range(75, 150);
        scaleModifier = numChanges / scaleModifierModifier;
        gameObject.transform.localScale = new Vector3(1 + scaleModifier, 1 + scaleModifier, 0);
        size = gameObject.transform.localScale;
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

                GameObject b = Instantiate(bullet, transform.position, Quaternion.identity);

                EnemyBulletBehavior ebb = b.GetComponent<EnemyBulletBehavior>();
                if (ebb != null)
                {
                    ebb.Initialize(bulletDamage, bulletDuration);
                }

                Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
                Vector2 bulletDirection = aimDirection.normalized;
                Vector2 velocity = bulletDirection * bulletSpeed;
                rb.linearVelocity = velocity;
            }
        }
    }

    public void TriggerPosession()
    {
        PosessionManager.pm.TriggerPosession(gameObject.transform.position, moveSpeed, maxHealth, 
            bulletSpeed, fireRate, bulletDamage, size);
        Destroy(gameObject);
    }

    public void GetHit(int dmg)
    {
        health -= dmg;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
