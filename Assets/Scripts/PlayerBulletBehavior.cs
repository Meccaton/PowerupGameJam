using UnityEngine;

public class PlayerBulletBehavior : MonoBehaviour
{
    public int damage;
    private float duration = 10.0f;

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

    private void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.TryGetComponent<EnemyBehavior>(out var enemy)) 
        {
            enemy.GetHit(damage);
            Debug.Log("Registering player bullet hit: " + damage + " damage");
            Destroy(gameObject);
        }
    }
}
