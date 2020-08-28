using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBoss : NPC
{
    [SerializeField] int id;
    //是否可以攻擊
    bool canAttack = false;

    public override void Track()
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
    protected override void Move()
    {
        if (targer != null)
        {
            float dis = Vector3.Distance(transform.position, targer.position);
            // Debug.Log(dis);
            if (dis > 5f)
            {
                canAttack = false;
                //代理器.設定目標(玩家.座標)
                agent.SetDestination(player.transform.position);
                //區域變數 目標座標=玩家.座標
                Vector3 m_posTarget = player.transform.position;
                //目標座標.y=本身.y
                m_posTarget.y = transform.position.y;
                //看著(目標座標)
                transform.LookAt(m_posTarget);
            }
            else
            {
                //代理器.設定目標(玩家.座標)
                agent.SetDestination(targer.transform.position);
                //區域變數 目標座標=玩家.座標
                Vector3 _posTarget = targer.transform.position;
                //目標座標.y=本身.y
                _posTarget.y = transform.position.y;
                //看著(目標座標)
                transform.LookAt(_posTarget);
                canAttack = true;
            }
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
    public override void Attack()
    {

        if (canAttack)
        {
            base.Attack();
        }

    }
    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(data.attackDelay);

        RaycastHit hit;    //射線碰撞資訊 存放射線碰到內容

        //物理.射線碰撞(中心點,方向,長度)
        if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.forward, out hit, data.Range))
        {
            //取得玩家元件.受傷方式(怪物.攻擊力)
            // hit.collider.GetComponent<Player>().Hit(data.attack);
        }
    }

    //事件:繪製圖示
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position + new Vector3(0, 1, 0), transform.forward * data.Range);
    }
    public override void Dead()
    {
        GameCtrl.instance.isGameOver = true;
        //MVCGame.instance.rank++;
        //死亡特效
        Instantiate(DSFX, transform.position, transform.rotation);
        if (ani.GetBool("Death")) return;
        ani.SetBool("Death", true);
        //GameManager.instance.totalpay += pay;
        //this.enabled = false;
        Destroy(gameObject, 2f);
    }
    private void Update()
    {


    }
}
