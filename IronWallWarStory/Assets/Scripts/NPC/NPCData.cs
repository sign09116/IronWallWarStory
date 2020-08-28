using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC資料", menuName = "NPCData")]
public class NPCData : ScriptableObject
{
    [Header("血量")]
    public float hp;
    [Header("最大血量")]
    public float hpMax;
    [Header("攻擊力")]
    public float attack;
    [Header("移動速度")]
    public float speed;
    [Header("攻擊冷卻時間")]
    public float CD;
    [Header("攻擊射程")]
    public float Range;
    [Header("射程停止距離")]
    public float stopDistance;
    [Header("攻擊延遲時間")]
    public float attackDelay;

}
