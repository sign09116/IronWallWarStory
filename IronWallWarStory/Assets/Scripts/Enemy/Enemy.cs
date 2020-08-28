using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("敵人資料")]
    public EnemyData data;          //敵人資料
    /// <summary>付錢</summary>
    public int pay;

    [Header("子彈物件")]
    public GameObject bullet;
    [Header("子彈發射位置")]
    public GameObject attackOffset;

    [Header("攻擊特效")]
    public GameObject SFX;

    [Header("死亡特效")]
    public GameObject DSFX;

    protected NavMeshAgent agent;      //導覽代理器
    public Transform player;        //玩家變形
    protected Animator ani;            //動畫
    protected float timer;             //計時器
    float attack;
    [SerializeField] HpBarControl hpControl;   //血條系統

    /// <summary>個別血量</summary>
    public float hp;      //個別血量
    string Rank = "Rank";
    ///<summary>怪物經驗值</summary>
    public float enemyLv;
   




    private void Start()
    {

        enemyLv = data.EnemyLv; //怪物經驗值=data.怪物經驗值

        ani = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = data.stopDistance;
        hp = data.hpMax;

        Track();
        agent.SetDestination(player.position);
        hpControl = transform.Find("血條系統").GetComponent<HpBarControl>();
        hpControl.UpdateHpBar(data.hpMax, data.hp);   //血量控制系統.更新血條(目前血量,最大血量)
    }

    /// <summary>追蹤玩家</summary>
    public virtual void Track()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (GameObject.Find("指揮官T"))
        {
            player = GameObject.Find("指揮官T").transform;
        }


    }

    private void Update()
    {

    }


    private void FixedUpdate()
    {
        Move();
    }


    public virtual void Attack()
    {
        if (ani.GetBool("Death"))
        {   
            return;
        }
        timer = 0;                    //計時器=0
        ani.SetTrigger("Attack");    //攻擊動畫
        StartCoroutine(CreateBullet());  //啟動協程
    }

    /// <summary>產生子彈</summary>
    public IEnumerator CreateBullet()
    {
        //等待
        yield return new WaitForSeconds(data.attackDelay);
        //實例化Instantiate(生成物件,生成出來的位置,生成出來的角度值)
        GameObject tempBullet = Instantiate(bullet, attackOffset.transform.position, attackOffset.transform.rotation);
        //暫存子彈.添加元件<子彈>();

        //暫存子彈.取得元件<子彈>().攻擊力=資料.攻擊力
        tempBullet.GetComponent<Bullet>().damage = data.attack;
        //攻擊特效
        Instantiate(SFX, attackOffset.transform.position, transform.rotation);
        //tempBullet.GetComponent<Bullet>().player = false;
    }

    //virtual 虛擬:讓子類別複寫

    protected virtual void Move()
    {
        float dis = Vector3.Distance(transform.position, player.position);
        if (targer != null)
        {
            //代理器.設定目標(玩家.座標)
            agent.SetDestination(targer.transform.position);
            //區域變數 目標座標=玩家.座標
            Vector3 _posTarget = targer.transform.position;
            //目標座標.y=本身.y
            _posTarget.y = transform.position.y;
            //看著(目標座標)
            transform.LookAt(_posTarget);
        }
        else
        {
            //代理器.設定目標(玩家.座標)
            agent.SetDestination(player.position);
            //區域變數 目標座標=玩家.座標
            Vector3 posTarget = player.position;
            //目標座標.y=本身.y
            posTarget.y = transform.position.y;
            //看著(目標座標)
            transform.LookAt(posTarget);
        }

        //如果 距離<=
        if (agent.remainingDistance <= data.stopDistance)
        {
            Wait();
        }
        else
        {
            agent.isStopped = false;
            ani.SetBool("Run", true);   //開啟跑步開關(人)
            ani.SetBool("Goahead", true);   //開啟前進開關(車)
        }
    }

    protected virtual void Wait()
    {
        //base.Wait(); //使用付類別方法內容

        agent.isStopped = true;        //代理器.是否停止=是
        agent.velocity = Vector3.zero; //代理器.加速度=零
        ani.SetBool("Run", false);   //關閉跑步開關(人)
        ani.SetBool("Goahead", false);   //關閉前進開關(車)

        //如果 計時器<=冷卻時間
        if (timer <= data.CD)
        {
            //時間累加
            timer += Time.deltaTime;
        }
        else  //否則
        {
            Attack();  //攻擊
        }
    }

    public void Hit(GameObject name,float damage)
    {
        
        //血量 扣 傷害
        hp -= damage;
        //血量限制 0-目前血量
        hp = Mathf.Clamp(hp, 0, data.hp);
        //血量控制系統.更新血條(目前血量,最大血量)
        hpControl.UpdateHpBar(data.hpMax, hp);
        //如果(血量=0)死亡 
        if (hp <= 0)
        {
            if (name.CompareTag("PlayerCTF"))
            {
                payMoney();
                GameCtrl.instance.totalLV += enemyLv;
                Dead();
            }
            else
            {
                Dead();
            }
           

        }
        //血量控制器.顯示傷害值
        StartCoroutine(hpControl.ShowDamage(damage));

    }

    public virtual void Dead()
    {

       // if (ani.GetBool("Death")) return;
        //死亡特效
        //Instantiate(DSFX, transform.position, transform.rotation);
        Destroy(Instantiate(DSFX, this.transform.position + Vector3.up, this.transform.rotation), 3f);
        ani.SetBool("Death", true);
        DropItem();
        //this.enabled = false;
        Destroy(gameObject, 2f);
    }

    //如果沒有目標 ，那我的目標就是我碰到的
    private void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("NPC") || hit.CompareTag("Player"))
        {
            if (targer == null)
            {
                targer = hit.transform;
            }
        }
    }

    /// <summary>掃描周圍目標</summary>
    [SerializeField] Collider _collider;
    public Transform targer;//目標
    public void SerachTarget(Transform target)
    {
        if (target == null)
        {
            targer = target;
        }

    }
    /// <summary>怪物掉落物品</summary>
    [SerializeField] GameObject item;
    public void DropItem()
    {
        int r = Random.Range(0, 100);
        if (r < 10)
        {
            Destroy(Instantiate(item, this.transform.position+Vector3.up, this.transform.rotation), 20f);
        }
    }
    public void payMoney()
    {
        GameCtrl.instance.totalpay += pay;
    }


    
   

}
