using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    #region 單例
    private void Awake()
    {
        instance = this;
    }
    #endregion
    SaveManager saveManager=SaveManager.instance;
    [Header("是否自動轉場")]
    //[SerializeField] Button testbutton;
    public bool autoOpenDoor;
    [SerializeField] GameObject Transitionsobj;
    [SerializeField] PlayerData playerData;
    private Animator door;  //動畫 轉場門


    // private GameObject panelResult;

    //[SerializeField] CanvasGroup gameOver;  //結算畫面

    [HideInInspector] public Scene scene;
    void Start()
    {
       
        door = Transitionsobj.GetComponent<Animator>();
        scene = SceneManager.GetActiveScene();
        if (scene.name != "Level")
        {
            door.SetTrigger("開門");
        }

        //   panelResult = GameObject.Find("結算畫面");

    }

    ///<summary>結算畫面</summary>
    /* public void PanelResult()
     {
         //透明=1
         panelResult.GetComponent<CanvasGroup>().alpha = 1;
         //互動=可
         panelResult.GetComponent<CanvasGroup>().interactable = true;
         //阻擋 =是
         panelResult.GetComponent<CanvasGroup>().blocksRaycasts = true;
         panelResult.GetComponent<Animator>().SetTrigger("結算畫面");

     }*/



    ///<summary>指定場景ID</summary>
    int id;
    ///<summary>獲得按鈕指定場景ID</summary>
    public void GetID(int id)
    {
        this.id = id;
    }
    /*public void SetNowChapter(int chapter)
    {
        MVCGame.instance.currentchapter = chapter;
    }*/
   /* public void SetNowLevel(int level)
    {
        MVCGame.instance.currentLevel = level;
    }*/
    public IEnumerator CloseDoor()
    {
        //動畫控制器.設定觸發("參數名稱")
        door.SetTrigger("關門");
        yield return new WaitForSeconds(1.5f);
        LoadScene(id);
    }
    /// <summary>載入指定場景</summary>
    void LoadScene(int sceneneNameId)
    {
        SceneManager.LoadScene(sceneneNameId);
    }
    /// <summary>點擊關卡按鈕</summary>
    public void OnButton()
    {

        StartCoroutine("CloseDoor");
    }
    /// <summary>回首頁按鈕</summary>
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
   
  



}
