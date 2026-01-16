using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        
    }

    void Update()
    {
        gameObject.transform.position = new Vector2(player.transform.position.x, player.transform.position.y);
    }
}
