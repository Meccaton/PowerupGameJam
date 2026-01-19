using UnityEngine;

public class PlayerBulletBehavior : MonoBehaviour
{
    public float damage;
    public GameObject deadBodyPrefab;
    private float duration = 5.0f;

    public void Initialize(float dmg)
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
                bool isenemyDead = enemy.GetHit(damage);
                if (isenemyDead) 
                {
                    GameObject db = Instantiate(deadBodyPrefab, enemy.transform.position, Quaternion.identity);
                    DeadBodyInitializer dbinit = db.GetComponent<DeadBodyInitializer>();
                    dbinit.Initialize(enemy.size, enemy.torso.GetComponent<Renderer>().material.color);
                }
                Destroy(gameObject);
            }
        }
    }
}
