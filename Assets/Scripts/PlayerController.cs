using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;

    public float bulletSpeed = 10f;
    public float fireRate = 1.0f;
    private float shootCooldown;
    public float shootCooldownTimestamp;

    public GameObject bullet;

    void Start()
    {
        shootCooldown = 1.0f/fireRate;
    }

    void Update()
    {
        MovementController();
        ShooterController();
    }

    private void MovementController()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * TestVert(Vector2.up));
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * TestHor(Vector2.left));
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * TestVert(Vector2.down));
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * TestHor(Vector2.right));
        }
    }

    private float TestVert(Vector2 v)
    {
        Vector2 origin = transform.position;
        float offset;
        if (v.Equals(Vector2.up))
        {
            offset = .1f;
        } 
        else
        {
            offset = -.5f;
        }
        origin.y += offset;
        RaycastHit2D raycast = Physics2D.Raycast(origin, v, speed * Time.deltaTime);

        if(raycast.collider != null && raycast.collider.gameObject.tag == "Collidable")
        {
            float distance = Mathf.Abs(raycast.point.y - origin.y);
            return distance;
        }
        return speed * Time.deltaTime;
    }

    private float TestHor(Vector2 v)
    {
        Vector2 origin = transform.position;
        float offset;
        if (v.Equals(Vector2.left))
        {
            offset = -.125f;
        }
        else
        {
            offset = .125f;
        }
        origin.x += offset;
        RaycastHit2D raycast = Physics2D.Raycast(origin, v, speed * Time.deltaTime);
        
        if(raycast.collider != null && raycast.collider.gameObject.tag == "Collidable")
        {
            float distance = Mathf.Abs(raycast.point.x - origin.x);
            return distance;
        }
        return speed * Time.deltaTime;
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

                GameObject b = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));

                Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
                Vector2 bulletDirection = new Vector2(shootDirection.x, shootDirection.y).normalized;
                Vector2 velocity = bulletDirection * bulletSpeed;// * Time.deltaTime;
                rb.linearVelocity = velocity;
                //rb.linearVelocity = new Vector2(shootDirection.x * bulletSpeed, shootDirection.y * bulletSpeed);
                //rb.AddForce(b.transform.forward * bulletSpeed);
            }
        }
    }
}
