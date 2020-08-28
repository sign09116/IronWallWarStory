using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Technology : MonoBehaviour
{  /// <summary>
/// 玩家資料
/// </summary>
    [SerializeField] PlayerData data;
    /// <summary>
    /// 科技資料
    /// </summary>
    [SerializeField] TechnologyData t_data;
    /// <summary> 科技攻擊</summary>
    [SerializeField] Button TechButton;
    /// <summary> 科技防禦</summary>
    [SerializeField] Button DechButton;
    /// <summary> 科技攻擊力等級</summary>
    [Header("科技攻擊力等級")]
    int techAttackLv = 1;
    [SerializeField] Text techAttackLvText;
    /// <summary> 科技攻擊力</summary>
    [Header("科技攻擊力")]
    [SerializeField] Text techAttackText;
    /// <summary>科技防禦力等級</summary>
    [Header("科技防禦力等級")]
    int techDefenseLv = 1;
    [SerializeField] Text techDefenseLvText;
    /// <summary> 科技防禦力</summary>
    [Header("科技防禦力")]
    [SerializeField] Text techDefenseText;
    /// <summary> 升級等級(攻擊)</summary>
    int techAttackLvUpgrade;
    [SerializeField] Text techAttackLvUpgradeText;

    /// <summary> 升級攻擊力</summary>
    float techAttackUpgrade;
    [SerializeField] Text techAttackUpgradeText;

    /// <summary> 升級等級(防禦)</summary>
    int techDefenseLvUpgrade;
    [SerializeField] Text techDefenseLvUpgradeText;

    /// <summary> 升級防禦力</summary>
    float techDefenseUpgrade;
    [SerializeField] Text techDefenseUpgradeText;


    /// <summary> 攻擊力升級金額</summary>
    [Header("攻擊力升級金額")]
    float Skill_Gun_M = 0f;
    [SerializeField] Text Skill_Gun_MT;
    /// <summary> 攻擊力</summary>
    string skii_Gun;

    /// <summary> 防禦力升級金額</summary>
    [Header("防禦力升級金額")]
    float Skill_Armor_M = 0f;
    /// <summary> 防禦力</summary>
    string skii_Armor;
    [SerializeField] Text Skill_Armor_MT;
    private void Awake()
    {
        if (Skill_Armor_M == 0 && Skill_Gun_M == 0)
        {
            PlayerPrefs.SetFloat("skii_Armor", 60);
            PlayerPrefs.SetFloat("skii_Gun", 150);
        }
    }
    private void Start()
    {

        techDefenseLv = t_data.techDefenseLv;
        techAttackLv = t_data.techAttackLv;
        techAttackUpgrade = data.Attack + 10f;
        techDefenseUpgrade = data.Defense + 3f;
        techDefenseLvUpgrade = t_data.techDefenseLv + 1;
        techAttackLvUpgrade = t_data.techAttackLv + 1;
        Skill_Armor_M = t_data.Skill_Armor_M;
        Skill_Gun_M = t_data.Skill_Gun_M;



    }



    public void TechAttack()
    {
        if (data.Money >= Skill_Gun_M)
        {
            TechButton.interactable = true;
            data.Attack += 10f;
            techAttackLv++;
            t_data.techAttackLv = techAttackLv;
            techAttackLvUpgrade = techAttackLv + 1;
            techAttackUpgrade = data.Attack + 10f;
            Skill_Gun_M *= 1.3f;
            t_data.Skill_Gun_M = Skill_Gun_M;
            data.Money -= (int)Skill_Gun_M;
        }
        else
        {
            TechButton.interactable = false;
        }
    }


    public void TechDefense()
    {

        if (data.Money >= Skill_Armor_M)
        {
            DechButton.interactable = true;
            data.Defense += 3f;
            techDefenseLv++;
            t_data.techDefenseLv = techDefenseLv;
            techDefenseLvUpgrade = techDefenseLv + 1;
            techDefenseUpgrade = data.Defense + 3f;
            Skill_Armor_M *= 1.3f;
            t_data.Skill_Armor_M = Skill_Armor_M;
        }
        else
        {

            DechButton.interactable = false;
        }

    }

    private void LateUpdate()
    {
        if (techAttackLv >= 999)
        {
            Skill_Gun_MT.text = "等級已滿";
            techAttackLvUpgrade = 999;
            TechButton.interactable = false;
        }
        else
        {
            TechButton.interactable = true;
            techAttackLvUpgradeText.text = techAttackLvUpgrade.ToString();
            Skill_Gun_MT.text = Skill_Gun_M.ToString("0");
        }

        if (techDefenseLv >= 999)
        {
            Skill_Armor_MT.text = "等級已滿";
            techDefenseLvUpgrade = 999;
            DechButton.interactable = false;
        }
        else
        {
            DechButton.interactable = true;
            techDefenseLvUpgradeText.text = techDefenseLvUpgrade.ToString();
            Skill_Armor_MT.text = Skill_Armor_M.ToString("0");
        }
        techAttackLvText.text = techAttackLv.ToString();
        techAttackText.text = data.Attack.ToString();
        techDefenseLvText.text = techDefenseLv.ToString();
        techDefenseText.text = data.Defense.ToString();
        techAttackUpgradeText.text = techAttackUpgrade.ToString();
        techDefenseUpgradeText.text = techDefenseUpgrade.ToString();

    }
}
