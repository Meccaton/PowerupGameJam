using UnityEngine;

public class GroundTextureSpawner : MonoBehaviour
{
    public int textureSize = 256;
    public int tileSize = 32;

    void Start()
    {
        Texture2D t = new Texture2D(textureSize, textureSize);
        Renderer r = GetComponent<Renderer>();
        r.material.mainTexture = t;

        for (int i = 0; i < textureSize; i++)
        {
            for (int j = 0; j < textureSize; j++)
            {
                if((i / tileSize % 2) != (j / tileSize % 2))
                {
                    t.SetPixel(i, j, Color.green);
                }
                else
                {
                    t.SetPixel(i, j, Color.darkGreen);
                }
            }
        }
        t.Apply();
    }

    void Update()
    {
        
    }
}
