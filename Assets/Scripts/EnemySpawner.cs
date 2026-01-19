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
    private float shootRange = 10.0f;
    private float moveSpeed = 5.0f;
    private float maxHealth = 3f;
    private float bulletSpeed = 8.0f;
    private float fireRate = 1.0f;
    private float bulletDamage = 1f;
    private float bulletDuration = 5.0f;
    private int numChanges;

    private float specialMoveSpeed = 0;
    private float specialMaxHealth = 0;
    private float specialBulletSpeed = 0;
    private float specialFireRate = 0;
    private float specialBulletDamage = 0;
    private Color c = Color.black;

    private int buffMod;
    private int prevBuffMod;

    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").transform;

        if (pc == null)
        {
            Debug.Log("Player not found");
        }

        spawnCooldown = 0f;
        numChanges = 0;
        prevBuffMod = 0;

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
            stats.Initialize(range, Mathf.Max(moveSpeed, specialMoveSpeed), Mathf.Max(maxHealth, specialMaxHealth),
                Mathf.Max(bulletSpeed, specialBulletSpeed), Mathf.Max(fireRate, specialFireRate), shootRange, 
                Mathf.Max(bulletDamage, specialBulletDamage), bulletDuration, numChanges, c);

            specialMoveSpeed = 0;
            specialMaxHealth = 0;
            specialBulletSpeed = 0;
            specialFireRate = 0;
            specialBulletDamage = 0;
            c = Color.black;
        }

        spawnCooldown = Time.time + Random.Range(1, maxSpawnTime);
    }

    private void BuffGuys()
    {
        buffMod = (int)Time.time / 15;
        if(buffMod > prevBuffMod)
        {
            prevBuffMod = buffMod;

            range += .25f;
            moveSpeed += .5f;
            maxHealth += .5f;
            bulletSpeed += .5f;
            fireRate += .5f;
            shootRange += .5f;
            bulletDamage += .25f;
            bulletDuration += .1f;
            numChanges++;
        }

        int superGuyChance = Random.Range(0, 10);
        if(superGuyChance == 9)
        {
            int statIdx = Random.Range(0, 5);
            switch (statIdx)
            {
                case 0:
                    specialMoveSpeed = moveSpeed * Random.Range(2, 4);
                    c = Color.blue;
                    break;
                case 1:
                    specialMaxHealth = maxHealth * Random.Range(2, 4);
                    c = Color.orange;
                    break;
                case 2:
                    specialBulletSpeed = bulletSpeed * Random.Range(1.1f, 2f);
                    c = Color.yellow;
                    break;
                case 3:
                    specialFireRate = fireRate * Random.Range(2, 3);
                    c = Color.purple;
                    break;
                case 4:
                    specialBulletDamage = bulletDamage * Random.Range(2, 3);
                    c = Color.red;
                    break;
            }
        }
    }
}
