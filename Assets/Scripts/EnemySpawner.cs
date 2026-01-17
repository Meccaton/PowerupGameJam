using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    private Transform pc;
    private int xPos;
    private int yPos;
    private Vector2 spawnPos;
    private float spawnCooldown;

    private float range = 10.0f;
    private float maxRange = 25.0f;
    private float shootRange = 10.0f;
    private float moveSpeed = 10.0f;
    private int maxHealth = 5;
    private float bulletSpeed = 1.0f;
    private float maxBulletSpeed = 100.0f;
    private float fireRate = 1.0f;

    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").transform;

        if (pc == null)
        {
            Debug.Log("No player found");
            pc.position = new Vector2(0, 0);
        }

        spawnCooldown = 0f;
    }

    void Update()
    {
        if(Time.time > spawnCooldown)
        {
            BuffGuys();
            SpawnGuys();
        }
    }

    private void SpawnGuys()
    {
        switch (Random.Range(0, 4))
        {
            case 0:
                xPos = 50;
                yPos = Random.Range(-50, 50);
                spawnPos = new Vector2(xPos, yPos);
                break;
            case 1:
                xPos = Random.Range(-50, 50);
                yPos = -50;
                spawnPos = new Vector2(xPos, yPos);
                break;
            case 2:
                xPos = -50;
                yPos = Random.Range(-50, 50);
                spawnPos = new Vector2(xPos, yPos);
                break;
            case 3:
                xPos = Random.Range(-50, 50);
                yPos = 50;
                spawnPos = new Vector2(xPos, yPos);
                break;
        }
        GameObject guy = Instantiate(enemy, spawnPos, Quaternion.identity);

        EnemyBehavior stats = guy.GetComponent<EnemyBehavior>();
        if (stats != null)
        {
            stats.Initialize(range, moveSpeed, maxHealth, bulletSpeed, fireRate, shootRange);
        }

        spawnCooldown = Time.time + Random.Range(1, 6);
    }

    private void BuffGuys()
    {
        int randStat = Random.Range(0, 5);
        switch (randStat)
        {
            case 0:
                range = Mathf.Min(range + 1, maxRange);
                shootRange = Mathf.Max(shootRange, range);
                break;
            case 1:
                moveSpeed += 1;
                break;
            case 2:
                maxHealth += 1;
                break;
            case 3:
                bulletSpeed = Mathf.Min(bulletSpeed + 5, maxBulletSpeed);
                break;
            case 4:
                fireRate += 1;
                break;
            case 5:
                shootRange += 1;
                break;
        }
    }
}
