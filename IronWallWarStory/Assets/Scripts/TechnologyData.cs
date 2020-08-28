using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "科技資料", menuName = "科技資料")]
public class TechnologyData : ScriptableObject
{
    [Header("科技攻擊力等級")]
    public int techAttackLv = 1;
    [Header("科技防禦力等級")]
    public int techDefenseLv = 1;
    [Header("防禦力升級金額")]
    public float Skill_Armor_M = 0f;
    [Header("攻擊力升級金額")]
    public float Skill_Gun_M = 0f;
}
