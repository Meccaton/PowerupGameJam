using UnityEngine;

public class EnemyBulletBehavior : MonoBehaviour
{
    public float damage;
    private float duration = 5.0f;

    public void Initialize(float dmg, float dur)
    {
        damage = dmg;
        duration = dur;
    }

    void Start()
    {
        Destroy(gameObject, duration);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerController pc = col.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.GetHit(damage);
                Destroy(gameObject);
            }
        }
    }
}
