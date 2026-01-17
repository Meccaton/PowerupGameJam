using UnityEngine;

public class PlayerBulletBehavior : MonoBehaviour
{
    public int damage;
    private float duration = 10.0f;

    void Start()
    {
        Destroy(gameObject, duration);
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        EnemyBehavior enemy = col.gameObject.GetComponent<EnemyBehavior>();

        if (enemy != null) 
        {
            //enemy.GetHit(damage);
            Debug.Log("Registering player bullet hit");
        }
    }
}
