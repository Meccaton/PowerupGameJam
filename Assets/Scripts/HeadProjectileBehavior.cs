using UnityEngine;

public class HeadProjectileBehavior : MonoBehaviour
{
    public GameObject player;
    public float duration = 2.0f;
    private float lifetime;
    private PlayerController pc;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
        lifetime = Time.time + duration;
        
    }

    void Update()
    {
        if (Time.time > lifetime)
        {
            pc.ResetHead();
            Destroy(gameObject);
        }
    }
}
