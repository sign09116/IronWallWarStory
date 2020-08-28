using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMachine : NPC
{
    [SerializeField] int id;
    //是否可以攻擊
    bool canAttack = false;

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

}
