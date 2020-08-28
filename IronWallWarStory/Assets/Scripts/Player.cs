using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;                       //引用 查詢語法LinQ(Qurery)
using System.Collections.Generic;        //引用 系統.集合.一般
using UnityEngine.EventSystems;
public class Player : MonoBehaviour
{
    #region 欄位
    [Header("玩家資料")]
    public PlayerData data;

    [Header("玩家科技資料")]
    public TechnologyData tech;

    [Header("移動速度")]
    public float speed;
    [Header("玩家子彈")]
    public GameObject playerBullet;
    [Header("玩家子彈位置")]
    public GameObject BulletPos;

    [Header("子彈生成速度")]
    public float CreateBulletSpeed = 0.3f;

    [Header("玩家地雷")]
    public GameObject playerCTF;

    [Header("玩家地雷位置")]
    public GameObject CTFPos;

    [Header("地雷生成速度")]
    public float CreateCTFSpeed = 3f;

    [Header("攻擊特效")]
    public GameObject SFX;
    [Header("死亡特效")]
    public GameObject DSFX;

    ///<summary>虛擬搖桿</summary>
    private Joystick joy;
    ///<summary>移動目標</summary>

    ///<summary>/剛體</summary>
    private Rigidbody rig;
    ///<summary>動畫</summary>
    private Animator ani;

    ///<summary>血條系統</summary>
    [SerializeField] HpBarControl hpControl;

    ///<summary>掃描周圍目標</summary>
    [SerializeField] Collider _collider;
    [SerializeField] Collider[] enemys;

    ///<summary>目標</summary>
    GameObject bullet;

    ///<summary>敵人清單</summary>
    //public  List<GameObject> enemys;

    ///<summary>敵人距離</summary>
    public List<float> enemysDistance;

    public LayerMask layerMask;
    bool isMOve = false;
    public bool Shoot = false;
    [SerializeField] float timer = 0f;
    ///<summary>子彈間隔時間</summary>
    [SerializeField] float B_waitTime = 0f;
    ///<summary>經驗值</summary>
    public float lv;




    #endregion
    #region 事件

    private void Start()
    {
        //剛體欄位=取得原件<泛型>()
        rig = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        joy = GameObject.Find("虛擬搖桿").GetComponent<Joystick>();

        lv = data.exp;//經驗值=data.經驗值

        speed = data.speed;

        //變形.尋找("子物件")
        hpControl = transform.Find("血條系統").GetComponent<HpBarControl>();
        data.hp = data.hpMax;
        //InvokeRepeating("呼叫Funcuion名稱",遊戲執行後等待時間,第2次之後的呼叫時間);
        // InvokeRepeating("CreateBullet", 0, CreateBulletSpeed);
        InvokeRepeating("CTFBullet", 0, CreateCTFSpeed);

    }

    public void CanShhot()
    {
        Shoot = true;
    }
    public void CantShhot()
    {
        Shoot = false;
    }

    public void FirstCreateBullet()
    {

        GameObject bullet = Instantiate(playerBullet, BulletPos.transform.position, BulletPos.transform.rotation);

        //攻擊特效
        GameObject bulletSFX = Instantiate(SFX, BulletPos.transform.position, transform.rotation);
        //暫存子彈.添加元件<子彈>();

        //暫存子彈.取得元件<子彈>().攻擊力=資料.攻擊力
        bullet.GetComponent<Bullet>().damage = data.Attack;
        bullet.GetComponent<Bullet>().player = true;
    }
    public IEnumerator CreateBullet()
    {

        //Instantiate(要生成物件,生成出來位置,生成出來的角度)
        GameObject bullet = Instantiate(playerBullet, BulletPos.transform.position, BulletPos.transform.rotation);
        //攻擊特效
        GameObject bulletSFX = Instantiate(SFX, BulletPos.transform.position, transform.rotation);
        //暫存子彈.取得元件<子彈>().攻擊力=資料.攻擊力
        bullet.GetComponent<Bullet>().damage = data.Attack;
        bullet.GetComponent<Bullet>().player = true;
        yield return null;
    }

    ///<summary>玩家地雷</summary>
    public void FirstCTFBullet()
    {

        //Instantiate(要生成物件,生成出來位置,生成出來的角度)
        Instantiate(playerCTF, CTFPos.transform.position, CTFPos.transform.rotation);
        //暫存子彈.取得元件<子彈>().攻擊力=資料.攻擊力
        bullet.GetComponent<CTF>().damage = data.Attack;
        bullet.GetComponent<CTF>().player = true;

    }

    public IEnumerator CTFBullet()
    {

        //Instantiate(要生成物件,生成出來位置,生成出來的角度)
        Instantiate(playerCTF, CTFPos.transform.position, CTFPos.transform.rotation);
        //暫存子彈.取得元件<子彈>().攻擊力=資料.攻擊力
        bullet.GetComponent<CTF>().damage = data.Attack;
        bullet.GetComponent<CTF>().player = true;
        yield return null;

    }




    //固定更新:每秒50次
    private void Update()
    {
        //Debug.Log(Shoot);
    }
    private void FixedUpdate()
    {

        if (Shoot == true)
        {
            timer += Time.deltaTime;
            if (timer > B_waitTime)
            {
                StartCoroutine("CreateBullet");
                timer = 0f;
            }
        }
        else
        {
            timer = 0f;
        }

        Move();

        if (isMOve == false)
        {
            Attack();
        }


    }

    #endregion
    #region 方法

    ///<summary>玩家移動方法</summary>
    private void Move()
    {   //前進
        if (Input.GetKey(KeyCode.W))
        {
            rig.AddForce(Vector3.forward * -speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rig.AddForce(Vector3.right * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rig.AddForce(Vector3.left * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rig.AddForce(Vector3.forward * speed);
        }


        isMOve = true;
        float h = joy.Horizontal;     //搖桿水平
        float v = joy.Vertical;       //搖桿垂直
        //剛體.增加推力(水平*速度,0,垂直*速度);
        rig.AddForce(-h * speed, 0, -v * speed);

        //玩家座標=取得玩家.座標  
        Vector3 posPlayer = transform.localPosition;
        //目標座標=新 三維向量(玩家.x-搖桿.x,y,玩家.z-搖桿.z)
        Vector3 posTarget = new Vector3(posPlayer.x - h, 0, posPlayer.z - v);

        //如果搖桿垂直+搖桿水平<=0就不是在移動
        if (Mathf.Abs(h) + Mathf.Abs(v) <= 0f)
        {
            isMOve = false;
        }

        //目標.y=玩家.y
        posTarget.y = posPlayer.y;
        //變形.面向(座標)
        transform.LookAt(posTarget);
        //動畫控制器.設定布林值(參數名稱,布林值)
        ani.SetBool("Goahead", h != 0 || v != 0);

    }
    RaycastHit raycastHit;
    ///<summary>玩家攻擊</summary>
    private void Attack()
    {
        //清除清單
        enemysDistance.Clear();


        //1.取得f球範圍所有敵人
        enemys = Physics.OverlapSphere(this.transform.position, _collider.GetComponent<SphereCollider>().radius, layerMask);
        for (int i = 0; i < enemys.Length; i++)
        {
            //取得距離
            float dis = Vector3.Distance(transform.position, enemys[i].transform.position);
            //清單.加入(資料)
            enemysDistance.Add(dis);
        }

        //取得最小距離
        float min = enemysDistance.Min();
        //取得索引值(編號)
        int index = enemysDistance.IndexOf(min);
        Vector3 enemyTarget = enemys[index].transform.position;
        enemyTarget.y = transform.position.y;
        transform.LookAt(enemyTarget);


        //GameObject bullet = Instantiate(playerBullet, BulletPos.transform.position, BulletPos.transform.rotation);

        //暫存子彈.添加元件<子彈>();
        //bullet.AddComponent<Bullet>();
        //暫存子彈.取得元件<子彈>().攻擊力=資料.攻擊力
        //bullet.GetComponent<Bullet>().damage = data.Attack;

    }

    ///<summary>玩家受傷方法:扣血 顯示傷害 更新血條</summary>
    ///<param name="damage">玩家受到傷害</param>
    public void Hit(float damage)
    {
        float realdamage = damage - data.Defense;
        if (realdamage <= 0)
        {
            data.hp -= 1;
        }
        else
        {
            data.hp -= realdamage;
        }
        //血量 扣 傷害
        // data.hp -= (damage - data.Defense);
        //血量限制 0-目前血量
        data.hp = Mathf.Clamp(data.hp, 0, data.hp);
        //血量控制系統.更新血條(目前血量,最大血量)
        hpControl.UpdateHpBar(data.hpMax, data.hp);
        //如果(血量=0)死亡 
        if (data.hp <= 0) Dead();
        //血量控制器.顯示傷害值
        StartCoroutine(hpControl.ShowDamage(damage));

    }
    ///<summary>玩家死亡</summary>
    private void Dead()
    {
        //死亡特效
        Instantiate(DSFX, transform.position, transform.rotation);
        GameCtrl.instance.StartCoroutine("CountDownRevial");//啟動協程
        this.enabled = false;  //this 此類別 enabled是否啟動
        GameCtrl.instance.isGameOver = true;
    }
    private void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("HPup"))
        {

            print("我回血了" + data.hpMax * 0.5f);
            data.hp += (data.hpMax * 0.5f);
            if (data.hp > data.hpMax)
            {
                data.hp = data.hpMax;
            }
            hpControl.UpdateHpBar(data.hpMax, data.hp);
            Destroy(hit.gameObject);
        }
    }

    /// <summary>接收經驗值</summary>
    public void GetExp(float value)
    {
        data.exp += value;
    }
    /// <summary>
    /// 接收錢
    /// </summary>
    /// <param name="value"></param>
    public void GetPay(int value)
    {
        data.Money += value;

    }




    #endregion
}
