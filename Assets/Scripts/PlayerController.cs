using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;

    public int maxHealth = 5;
    public int health;

    public int bulletDamage = 1;
    public float bulletSpeed = 10.0f;
    public float fireRate = 1.0f;
    private float shootCooldown;
    private float shootCooldownTimestamp;

    public float headShootSpeed = 15.0f;

    public GameObject bullet;
    public GameObject headProjectile;
    public GameObject headModel;
    public GameObject deadBody;

    private bool canFireHead;
    private bool alive;
    private float deadTime = 5.0f;
    private float restartTimer;

    public void Initialize(float ms, int mh, float bs, float fr, int bd, Vector3 s)
    {
        speed = ms + 1;
        maxHealth = mh * 2;
        //health = maxHealth;
        bulletSpeed = bs * 3;
        fireRate = fr * 2;
        bulletDamage = bd + 1;
        gameObject.transform.localScale = s;
    }

    void Start()
    {
        alive = true;
        shootCooldown = 1.0f/fireRate;
        health = maxHealth;
        canFireHead = true;
    }

    void Update()
    {
        if (alive)
        {
            MovementController();
            ShooterController();
            PosessionController();
        }
        else
        {
            if(Time.time > restartTimer)
            {
                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName);
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

                GameObject b = Instantiate(bullet, transform.position, Quaternion.identity);

                PlayerBulletBehavior pbb = b.GetComponent<PlayerBulletBehavior>();
                if (pbb != null)
                {
                    pbb.Initialize(bulletDamage);
                }

                Rigidbody2D rb = b.GetComponent<Rigidbody2D>();

                Vector3 shootDirection = Input.mousePosition;
                shootDirection.z = 0.0f;
                shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
                shootDirection = shootDirection - transform.position;
                shootDirection = shootDirection.normalized;
                
                Vector2 velocity = shootDirection * bulletSpeed;
                rb.linearVelocity = velocity;
            }
        }
    }

    private void PosessionController()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (canFireHead)
            {
                canFireHead = false;
                headModel.SetActive(false);
                GameObject hp = Instantiate(headProjectile, headModel.transform.position, Quaternion.identity);
                Rigidbody2D rb = hp.GetComponent<Rigidbody2D>();
                Vector3 shootDirection = Input.mousePosition;
                shootDirection.z = 0.0f;
                shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
                shootDirection = shootDirection - headModel.transform.position;
                shootDirection = shootDirection.normalized;
                Vector2 vel = shootDirection * headShootSpeed;
                rb.linearVelocity = vel;
            }
        }
    }

    public void ResetHead()
    {
        canFireHead = true;
        headModel.SetActive(true);
    }

    public void TriggerPosession()
    {
        Instantiate(deadBody, transform.position, Quaternion.identity);
        ResetHead();
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
