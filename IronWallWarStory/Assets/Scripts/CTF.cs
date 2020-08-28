using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTF : MonoBehaviour
{
    [Header("爆炸特效")]
    public GameObject SFX;

    /// <summary>接收遠攻敵人的攻擊力</summary>
    public float damage;
    public bool player;  //判斷武器 true玩家   false怪物
    [SerializeField] PlayerData p_Data;
    [SerializeField] EnemyData e_Data;
    public bool NPC;

    void Start()
    {
        if (!player && !NPC)
        {
            damage = e_Data.attack;
        }
        if (player)
        {
            damage = p_Data.Attack;
        }
    }


    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        //如果(不是玩家子彈)
        if (!player && !NPC)
        {
            //如果(碰到物件.標籤=="Player")
            if (other.tag == "Player")
            {
                //Instantiate(SFX, other.transform.position, transform.rotation);
                Destroy(Instantiate(SFX, this.transform.position + Vector3.up, this.transform.rotation), 3f);
                //碰到物件.取得元件<Player>().受傷(攻擊力)
                other.GetComponent<Player>().Hit(damage);
                Destroy(gameObject);

            }
            if (other.tag == "NPC")
            {
                //Instantiate(SFX, other.transform.position, transform.rotation);
                Destroy(Instantiate(SFX, this.transform.position + Vector3.up, this.transform.rotation), 3f);
                other.GetComponent<NPC>().Hit(damage);
                Destroy(gameObject);
            }

        }
        if (player)
        {
            if (other.tag == "Enemy")
            {
                //Instantiate(SFX, other.transform.position, transform.rotation);
                Destroy(Instantiate(SFX, this.transform.position + Vector3.up, this.transform.rotation), 3f);
                //碰到物件.取得元件<Player>().受傷(攻擊力)
                other.GetComponent<Enemy>().Hit(gameObject, damage);

                Destroy(gameObject);
            }
        }


    }
}
