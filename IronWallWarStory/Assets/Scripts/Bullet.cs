using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("物件生成後刪除時間")]
    public float DeleteTime = 1f;
    [Header("子彈飛行速度")]
    public float bulletSpeed = 0.01f;
    [SerializeField] MusicData m_data;
    [Header("爆炸特效")]
    public GameObject SFX;
    [SerializeField] AudioSource aud;
   

    /// <summary>接收遠攻敵人的攻擊力</summary>
    public float damage; //接收遠攻敵人的攻擊力

    /// <summary>判斷武器 true玩家   false怪物</summary>
    public bool player;  //判斷武器 true玩家   false怪物

    public bool NPC;

    
    void Start()
    {
        
        aud.volume = m_data.S_Volume;
        //刪除時間
        Destroy(gameObject, DeleteTime);
    }

    void Update()
    {
        //子彈飛行速度
        transform.Translate(0, 0, bulletSpeed * Time.deltaTime);
    }

    /// <summary>子彈判定</summary>
    private void OnTriggerEnter(Collider other)
    {
        //如果(不是玩家子彈)
        if (!player || !NPC)
        {
            //如果(碰到物件.標籤=="Player")
            if (other.tag=="Player")
            {
               
                //Instantiate(SFX, other.transform.position, transform.rotation);
                Destroy(Instantiate(SFX, this.transform.position + Vector3.up, this.transform.rotation), 3f);
                //碰到物件.取得元件<Player>().受傷(攻擊力)
                other.GetComponent<Player>().Hit(damage);
                Destroy(gameObject);

            }
            if (other.tag=="NPC")
            {
                Debug.Log(damage);
                //Instantiate(SFX, other.transform.position, transform.rotation);
                Destroy(Instantiate(SFX, this.transform.position + Vector3.up, this.transform.rotation), 3f);
                other.GetComponent<NPC>().Hit(damage);
                Destroy(gameObject);
            }

              
        }

        if (player || NPC)
        {
            if (other.tag=="Enemy")
            {
                Debug.Log(damage);
                //Instantiate(SFX, other.transform.position, transform.rotation);
                Destroy(Instantiate(SFX, this.transform.position + Vector3.up, this.transform.rotation), 3f);
                //碰到物件.取得元件<Player>().受傷(攻擊力)
                other.GetComponent<Enemy>().Hit(gameObject, damage);  
                Destroy(gameObject);
            }
        }


    }
}
