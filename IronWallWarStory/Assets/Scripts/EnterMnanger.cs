using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnterMnanger : MonoBehaviour
{/// <summary>
/// 輸入名稱欄位
/// </summary>
    [SerializeField] InputField inputField;
    /// <summary>
    /// 儲存名稱的KEY
    /// </summary>
    string playername;
    /// <summary>
    /// 儲存名稱的值
    /// </summary>
    string myName;
    /// <summary>
    /// 登入按鈕
    /// </summary>
    [SerializeField] Button Enterbutton;
    [SerializeField] Animator door;
    /// <summary>
    /// 登入UI群組
    /// </summary>
    [SerializeField] CanvasGroup Enter_grp, playerNameinput_grp;
    [SerializeField] Text preName;
    [SerializeField] PlayerData data;
    SaveManager saveManager = SaveManager.instance;
    private void Start()
    {
      
            saveManager.Load(data);
            preName.text = saveManager.allData.name;

        if (preName.text=="")
        {

            preName.text = "請輸入名稱";
        }
     
        
        if (preName.text != "請輸入名稱")
        {
            playerNameinput_grp.alpha = 0;
            playerNameinput_grp.blocksRaycasts = false;
        }
        else
        {
            playerNameinput_grp.alpha = 1;
            playerNameinput_grp.blocksRaycasts = true;
        }
        Enter_grp.alpha = 1;
        Enter_grp.blocksRaycasts = true;

    }

    public void GetMyName(Text Entername)
    {
        if (preName.text == saveManager.allData.name)
        {
            myName = preName.text;
            ToNextscene(1);
        }
        else
        {
            myName = Entername.text;
        }

        PlayerPrefs.SetString("playername", myName);
        //如果沒有輸入名稱就不進入遊戲
        if (myName == "")
        {
            Debug.Log("請輸入你的名字");
            return;
        }
        door.SetTrigger("登入開門");
        Enter_grp.alpha = 0;
        Enter_grp.blocksRaycasts = false;
    }
    public void ToNextscene(int sceneneNameId)
    {

        SceneManager.LoadScene(sceneneNameId);

    }


}
