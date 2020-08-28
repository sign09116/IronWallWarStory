using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameCtrl : MonoBehaviour
{
    #region 單例
    private void Awake()
    {
        instance = this;
    }
    public static GameCtrl instance;
    #endregion
    [Header("遊戲執行後呼叫function時間")]
    public float StartTime;

    public SceneData[] sceneDataList;

    ///<summary>function間隔時間</summary>
    [Header("function間隔時間")]
    public float WaitTime;


    ///<summary>產生怪物位置</summary>
    [Header("產生怪物位置")]
    public Collider MonsterPos;
    //抓取Collider最大值
    Vector3 MaxNum;
    //抓取Collider最小值
    Vector3 MinNum;
    ///<summary>生成的敵人</summary>
    [Header("生成的敵人")]
    public GameObject[] Enemy;

    ///<summary>產生NPC位置</summary>
    [Header("產生NPC位置")]
    public Collider _MonsterPos;
    //抓取Collider最大值
    Vector3 _MaxNum;
    //抓取Collider最小值
    Vector3 _MinNum;
    ///<summary>生成的NPC</summary>
    [Header("生成的NPC")]
    public GameObject[] NPC;
    public bool isGameOver = false;

    ///<summary>遊戲音樂資料</summary>
    [SerializeField] MusicData m_data;


    /// <summary>結束畫面 </summary>
    [SerializeField] CanvasGroup gameOver;  //結束畫面
    public bool isWin = false;


    /// <summary>勝利與失敗圖 </summary>
    [Header("勝利與失敗圖")]
    public Image DesplayFinal;
    [SerializeField] Sprite[] ShowFinal;

    [SerializeField] GameObject[] BGM;
    /// <summary>關卡金錢顯示 </summary>
    [SerializeField] Text pay_text;
    ///<summary>總收入</summary>
    public int totalpay;
    public float totalLV;
    public int totalpayhalf;
    public float totalLVhalf;
    [SerializeField] Text Lv_text;

    bool playGameOver = false;

    private void Start()
    {
        for (int i = 0; i < BGM.Length; i++)
        {
            BGM[i].GetComponent<AudioSource>().volume = m_data.Volume;
        }
        if (_MonsterPos==null)
        {
            return; 
        }
        InvokeRepeating("CreateMonster", StartTime, WaitTime);

        MonsterPos = GameObject.Find("Enemy生成點").GetComponent<Collider>();

        _MonsterPos = GameObject.Find("NPC生成點").GetComponent<Collider>();

        //gameOver = GameObject.Find("結束畫面").GetComponent<CanvasGroup>();


    }



    public IEnumerator CountDownRevial()
    {

        /* gameOver.alpha = 1;                  //顯示復活畫面
         gameOver.interactable = true;          //可互動
         gameOver.blocksRaycasts = true;        //阻擋迴圈*/

        yield return new WaitForSeconds(10f); //等待10秒

    }

    public void CreateMonster()
    {
        //抓取Collider邊界
        MaxNum = MonsterPos.bounds.max;
        MinNum = MonsterPos.bounds.min;
        //x值隨機
        Vector3 RandomNum = new Vector3(MinNum.x, MinNum.y, Random.Range(MinNum.z, MaxNum.z));

        //動態生成
        Instantiate(Enemy[0], RandomNum, MonsterPos.transform.rotation);
        Instantiate(Enemy[1], RandomNum, MonsterPos.transform.rotation);
        Instantiate(Enemy[2], RandomNum, MonsterPos.transform.rotation);
        Instantiate(Enemy[3], RandomNum, MonsterPos.transform.rotation);
        Instantiate(Enemy[4], RandomNum, MonsterPos.transform.rotation);


        //抓取Collider邊界
        _MaxNum = _MonsterPos.bounds.max;
        _MinNum = _MonsterPos.bounds.min;
        //x值隨機
        Vector3 _RandomNum = new Vector3(_MinNum.x, _MinNum.y, Random.Range(_MinNum.z, _MaxNum.z));

        //動態生成
        Instantiate(NPC[0], _RandomNum, _MonsterPos.transform.rotation);
        Instantiate(NPC[1], _RandomNum, _MonsterPos.transform.rotation);
        Instantiate(NPC[2], _RandomNum, _MonsterPos.transform.rotation);
        Instantiate(NPC[3], _RandomNum, _MonsterPos.transform.rotation);
        Instantiate(NPC[4], _RandomNum, _MonsterPos.transform.rotation);

    }

    SaveManager saveManager=SaveManager.instance;
    public void MenuButton()
    {
        ContinueGame();
        if (isGameOver)
        {
            GameObject.Find("PlayerTank").GetComponent<Player>().GetExp(totalLVhalf);
            GameObject.Find("PlayerTank").GetComponent<Player>().GetPay(totalpayhalf);
        }
        else
        {

            GameObject.Find("PlayerTank").GetComponent<Player>().GetExp(totalLV);
            GameObject.Find("PlayerTank").GetComponent<Player>().GetPay(totalpay);
        }
        //切換介面("Menu場景")
        saveManager.Save(sceneDataList[0].playerData.playerName, sceneDataList[0].playerData);
        Application.LoadLevel("Menu");
    }

    public void PauseGame()
    {
        //暫停遊戲
        Time.timeScale = 0;
    }
    ///<summary>恢復遊戲時間</summary>
    public void ContinueGame()
    {
        //恢復遊戲
        Time.timeScale = 1;
    }
    private void Update()
    {
        totalLVhalf = totalLV / 2;
        totalpayhalf = totalpay / 2;
        if (isGameOver)
        {
            // Time.timeScale = 0f;
            GameOver();
            pay_text.text = (totalpayhalf).ToString();
            Lv_text.text = (totalLVhalf).ToString();

        }
        if (isWin)
        {
            GameWin();
            pay_text.text = totalpay.ToString();
            Lv_text.text = totalLV.ToString();
        }
    }
    [SerializeField] Animator gameoverAni;
    ///<summary>結算畫面</summary>
    public void GameOver()
    {
        
        DesplayFinal.sprite = ShowFinal[1];
        gameoverAni.SetBool("End", true);
        //透明=1
        gameOver.alpha = 1f;
        //阻擋 =是
        gameOver.blocksRaycasts = true;
        Time.timeScale = 0;

    }
    public void GameWin()
    {
        DesplayFinal.sprite = ShowFinal[0];
        gameoverAni.SetBool("End", true);
        //透明=1
        gameOver.alpha = 1f;
        //阻擋 =是
        gameOver.blocksRaycasts = true;
        Time.timeScale = 0;
    }

}
