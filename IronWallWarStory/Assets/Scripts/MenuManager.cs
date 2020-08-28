using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] PlayerData data;
    [SerializeField] Text Monytext;
    [SerializeField] Text PlayerNameText;
    [SerializeField] Image LvBar;
    /// <summary>隊伍視窗動畫控制器</summary>
    [SerializeField] Animator ani_Team;
    /// <summary>隊伍視窗開關按鈕群組</summary>
    [SerializeField] CanvasGroup showTeam_grp, hideTeam_grp;
    string playername;
    /// <summary>
    /// 經驗值總量
    /// </summary>
   public float maxLvValue=1000f;
    // Start is called before the first frame update
    SaveManager saveManager = SaveManager.instance;
    void Start()
    {
        saveManager.Load(data);
        saveManager.Save(data.playerName,data);
        Time.timeScale = 1f;
        PlayerNameText.text = PlayerPrefs.GetString("playername");
        data.playerName= PlayerPrefs.GetString("playername");
        LvBarUpdate();
        
    }
    private void Update()
    {
        Monytext.text = data.Money.ToString();
    }
    public void LvBarUpdate()
    {
        if (data.exp>= maxLvValue)
        {
            maxLvValue *= 2;
        }
        LvBar.fillAmount = (data.exp / maxLvValue);
    }
    /// <summary> 顯示隊伍</summary>
    public void ShowTeam()
    {
        ani_Team.SetBool("ShowTeam", true);
        showTeam_grp.alpha = 0;
        showTeam_grp.blocksRaycasts = false;
        hideTeam_grp.alpha = 1;
        hideTeam_grp.blocksRaycasts = true;
    }
    /// <summary>隱藏隊伍 </summary>
    public void HideTeam()
    {
        ani_Team.SetBool("ShowTeam", false);
        showTeam_grp.alpha = 1;
        showTeam_grp.blocksRaycasts = true;
        hideTeam_grp.alpha = 0;
        hideTeam_grp.blocksRaycasts = false;
    }

}   
