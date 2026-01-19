using UnityEngine;

public class DeadBodyCleanup : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 20f);
    }

    void Update()
    {
        
    }
}
