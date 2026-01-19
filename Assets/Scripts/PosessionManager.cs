using UnityEngine;

public class PosessionManager : MonoBehaviour
{
    public static PosessionManager pm;

    public GameObject player;
    public GameObject deadBodyPrefab;

    private void Awake()
    {
        if(pm != null)
        {
            Destroy(pm);
        }
        else
        {
            pm = this;
        }
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TriggerPosession(Vector2 enemyPos, float ms, float mh, float bs, float fr, float bd, Vector3 s, Color c)
    {
        if (pm != null)
        {
            PlayerController playerInfo = player.GetComponent<PlayerController>();
            GameObject db = Instantiate(deadBodyPrefab, player.transform.position, Quaternion.identity);
            DeadBodyInitializer dbinit = db.GetComponent<DeadBodyInitializer>();
            dbinit.Initialize(player.transform.localScale, playerInfo.torso.GetComponent<Renderer>().material.color);

            player.transform.position = enemyPos;
            PlayerController pc = player.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.Initialize(ms, mh, bs, fr, bd, s, c);
            }
            
        }
    }
}
