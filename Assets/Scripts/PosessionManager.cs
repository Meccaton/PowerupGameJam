using UnityEngine;

public class PosessionManager : MonoBehaviour
{
    public static PosessionManager pm;

    public GameObject player;
    //public GameObject playerPrefab;
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

    public void TriggerPosession(Vector2 enemyPos, float ms, int mh, float bs, float fr, int bd, Vector3 s)
    {
        if (pm != null)
        {
            //GameObject player = Instantiate(playerPrefab, enemyPos, Quaternion.identity);
            Instantiate(deadBodyPrefab, enemyPos, Quaternion.identity);
            Instantiate(deadBodyPrefab, player.transform.position, Quaternion.identity);

            player.transform.position = enemyPos;
            PlayerController pc = player.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.Initialize(ms, mh, bs, fr, bd, s);
            }
            
        }
    }
}
