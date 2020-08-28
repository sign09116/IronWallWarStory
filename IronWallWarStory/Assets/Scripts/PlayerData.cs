
using UnityEngine;


[CreateAssetMenu(fileName = "玩家資料", menuName = "玩家資料")]
public class PlayerData : ScriptableObject
{
    [Header("玩家名稱")]
    public string playerName;
    [Header("玩家等級")]
    public int playerLV;
    [Header("血量")]
    public float hp = 500;
    [Header("最大血量:不會改變")]
    public float hpMax = 500;

    [Header("攻擊力")]
    public float Attack = 10f;

    [Header("防禦力")]
    public float Defense = 0f;
    [Header("攻擊速度")]
    public float cd;

    [Header("移動速度")]
    public float speed;

    /// <summary>持有金錢</summary>
    [Header("持有金錢")]
    public int Money;

    [Header("經驗值")]
    public float exp;


}
