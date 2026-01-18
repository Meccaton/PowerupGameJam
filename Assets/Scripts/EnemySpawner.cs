using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float maxSpawnTime = 6.0f;

    private Transform pc;
    private float xPos;
    private float yPos;
    private Vector2 spawnPos;
    private float spawnCooldown;

    private float range = 10.0f;
    private float maxRange = 25.0f;
    private float shootRange = 10.0f;
    private float moveSpeed = 3.0f;
    private int maxHealth = 3;
    private float bulletSpeed = 4.0f;
    private float maxBulletSpeed = 100.0f;
    private float fireRate = 1.0f;
    private int bulletDamage = 1;
    private float bulletDuration = 5.0f;
    private int numChanges;

    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").transform;

        if (pc == null)
        {
            Debug.Log("Player not found");
        }

        spawnCooldown = 0f;
        numChanges = 0;

        Instantiate(enemy, new Vector2(30, Random.Range(-20, 20)), Quaternion.identity);
        Instantiate(enemy, new Vector2(-30, Random.Range(-20, 20)), Quaternion.identity);
        Instantiate(enemy, new Vector2(Random.Range(-20, 20), 30), Quaternion.identity);
        Instantiate(enemy, new Vector2(Random.Range(-20, 20), -30), Quaternion.identity);
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
                xPos = pc.position.x + 50;
                yPos = pc.position.y + Random.Range(-50, 50);
                spawnPos = new Vector2(xPos, yPos);
                break;
            case 1:
                xPos = pc.position.x + Random.Range(-50, 50);
                yPos = pc.position.y - 50;
                spawnPos = new Vector2(xPos, yPos);
                break;
            case 2:
                xPos = pc.position.x - 50;
                yPos = pc.position.y + Random.Range(-50, 50);
                spawnPos = new Vector2(xPos, yPos);
                break;
            case 3:
                xPos = pc.position.x + Random.Range(-50, 50);
                yPos = pc.position.y + 50;
                spawnPos = new Vector2(xPos, yPos);
                break;
        }

        numChanges++;

        GameObject guy = Instantiate(enemy, spawnPos, Quaternion.identity);

        EnemyBehavior stats = guy.GetComponent<EnemyBehavior>();
        if (stats != null)
        {
            stats.Initialize(range, moveSpeed, maxHealth, bulletSpeed, fireRate, shootRange, bulletDamage, bulletDuration, numChanges);
        }

        spawnCooldown = Time.time + Random.Range(1, maxSpawnTime);
    }

    private void BuffGuys()
    {
        int randStat = Random.Range(0, 8);
        switch (randStat)
        {
            case 0:
                range = Mathf.Min(range + 1, maxRange);
                shootRange = Mathf.Max(shootRange, range);
                break;
            case 1:
                moveSpeed += .5f;
                break;
            case 2:
                maxHealth += 1;
                break;
            case 3:
                bulletSpeed = Mathf.Min(bulletSpeed + 1, maxBulletSpeed);
                break;
            case 4:
                fireRate += .25f;
                break;
            case 5:
                shootRange += 1;
                break;
            case 6:
                bulletDamage += 1;
                break;
            case 7:
                bulletDuration += 2.5f;
                break;
        }
    }
}
