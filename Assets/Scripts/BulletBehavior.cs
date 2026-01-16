using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private float duration = 1.0f;
    private float lifeTimestamp;

    GameObject pc;

    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player");

        lifeTimestamp = Time.time + duration;
    }

    void Update()
    {
        if(transform.position.x > pc.transform.position.x + 50 || 
           transform.position.y > pc.transform.position.y + 50 ||
           Time.time >= lifeTimestamp)
        {
            Destroy(gameObject);
        }
    }
}
