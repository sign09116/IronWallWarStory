using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPC : MonoBehaviour
{
    [Header("NPC資料")]
    public NPCData data;          //NPC資料

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

    [SerializeField] HpBarControl hpControl;   //血條系統

    /// <summary>個別血量</summary>
    public float hp;      //個別血量



    private void Start()
    {
        ani = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = data.stopDistance;
        hp = data.hpMax;
        
        agent.SetDestination(player.position);
        Track();
        hpControl.UpdateHpBar(data.hpMax, data.hp);   //血量控制系統.更新血條(目前血量,最大血量)
    }

    /// <summary>追蹤敵人</summary>

    public virtual void Track()
    {
        if (GameObject.Find("指揮官DT"))
        {
            player = GameObject.Find("指揮官DT").transform;
        }
        else
        {
            player = this.gameObject.transform;
        }
        
    }

    private void Update()
    {
        Track();
    }


    private void FixedUpdate()
    {
        Move();
    }


    public virtual void Attack()
    {
        timer = 0;                    //計時器=0
        ani.SetTrigger("Attack");    //攻擊動畫
        StartCoroutine(CreateBullet());  //啟動協程
    }

    /// <summary>產生子彈</summary>
    private IEnumerator CreateBullet()
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

    public void Hit(float damage)
    {
        //血量 扣 傷害
        hp -= damage;
        //血量限制 0-目前血量
        hp = Mathf.Clamp(hp, 0, hp);
        //血量控制系統.更新血條(目前血量,最大血量)
        hpControl.UpdateHpBar(data.hpMax, hp);
        //如果(血量=0)死亡 
        if (hp <= 0) Dead();
        //血量控制器.顯示傷害值
        StartCoroutine(hpControl.ShowDamage(damage));

    }
    public virtual void Dead()
    {
        if (ani.GetBool("Death")) return;
        //死亡特效
        //Instantiate(DSFX, transform.position, transform.rotation);
        Destroy(Instantiate(DSFX, this.transform.position + Vector3.up, this.transform.rotation), 3f);
        ani.SetBool("Death", true);
        //this.enabled = false;
        Destroy(gameObject, 2f);
    }

    //如果沒有目標 ，那我的目標就是我碰到的
    private void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Enemy")
        {
            if (targer == null)
            {
                targer = hit.transform;
            }
        }
    }

    /// <summary>掃描周圍目標</summary>
    [Header("範圍")]
    [SerializeField] Collider _collider;

    [Header("目標")]
    public Transform targer;//目標
    public void SerachTarget(Transform target)
    {
        if (target == null)
        {
            targer = target;
        }

    }
}
