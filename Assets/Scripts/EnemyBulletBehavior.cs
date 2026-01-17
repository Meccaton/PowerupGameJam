using UnityEngine;

public class EnemyBulletBehavior : MonoBehaviour
{
    private float duration = 10.0f;

    void Start()
    {
        Destroy(gameObject, duration);
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
