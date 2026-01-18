using UnityEngine;

public class PlayerBulletBehavior : MonoBehaviour
{
    public int damage;
    private float duration = 5.0f;

    public void Initialize(int dmg)
    {
        damage = dmg;
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
        if (col.CompareTag("Enemy"))
        {
            EnemyBehavior enemy = col.GetComponent<EnemyBehavior>();
            if (enemy != null) 
            {
                enemy.GetHit(damage);
                Destroy(gameObject);
            }
        }
    }
}
