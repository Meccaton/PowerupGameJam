using UnityEngine;

public class DeadBodyInitializer : MonoBehaviour
{
    public GameObject torso;

    public void Initialize(Vector2 size, Color color)
    {
        gameObject.transform.localScale = size;
        torso.GetComponent<Renderer>().material.color = color;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
