using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    ///<summary>背景音量開關</summary>
    [Header("背景音量開關")]
    public bool backControlSound;
    ///<summary>背景音量滑桿</summary>
    [Header("背景音量滑桿")]
    public Slider BackControlVolume;
    ///<summary>背景音量關圖</summary>
    [Header("背景音量關圖")]
    public Sprite BackOpenSprite;
    ///<summary>背景音量開圖</summary>
    [Header("背景音量開圖")]
    public Sprite BackControlSprite;
    ///<summary>背景音量按鍵</summary>
    [Header("背景音量按鍵")]
    public Image BackButtonImage;






    ///<summary>遊戲音效開關</summary>
    [Header("遊戲音效開關")]
    public bool gameControlSound;
    ///<summary>遊戲音效滑桿</summary>
    [Header("音效滑桿")]
    public Slider GameControlVolume;
    ///<summary>遊戲音效關圖</summary>
    [Header("遊戲音效關圖")]
    public Sprite GameOpenSprite;
    ///<summary>遊戲音效開圖</summary>
    [Header("遊戲音效開圖")]
    public Sprite GameControlSprite;
    ///<summary>遊戲音效按鍵</summary>
    [Header("遊戲音效按鍵")]
    public Image GameButtonImage;

    [SerializeField] AudioSource[] backaud;



    ///<summary>遊戲音樂資料</summary>
    [SerializeField] MusicData m_data;
    private void Start()
    {
        for (int i = 0; i < backaud.Length; i++)
        {
            backaud[i].volume = m_data.Volume;
        }

        BackControlVolume.value = m_data.Volume;
        GameControlVolume.value = m_data.S_Volume;

    }

    private void Update()
    {
        //背景整體音量=Slider的value

        //音效整體音量=Slider的value
        //AudioListener.volume = GameControlVolume.value;

    }

    ///<summary>聲音按鈕開關</summary>
    private void FixedUpdate()
    {
        //背景音按鈕

        if (BackControlVolume.value > 0f)
        {
            BackButtonImage.sprite = BackControlSprite;
            backControlSound = false;
            for (int i = 0; i < backaud.Length; i++)
            {
                backaud[i].volume = m_data.Volume;
            }

        }
        else
        {
            BackButtonImage.sprite = BackOpenSprite;
            backControlSound = true;
            for (int i = 0; i < backaud.Length; i++)
            {
                backaud[i].volume = m_data.Volume;
            }
        }
        m_data.Volume = BackControlVolume.value;

        //音效按鈕

        if (GameControlVolume.value > 0f)
        {
            GameButtonImage.sprite = GameControlSprite;
            gameControlSound = false;


        }
        else
        {
            GameButtonImage.sprite = GameOpenSprite;
            gameControlSound = true;

        }
        m_data.S_Volume = GameControlVolume.value;


    }
    ///<summary>背景聲音開關</summary>
    public void BackControlSound()
    {

        backControlSound = !backControlSound;


        if (backControlSound)
        {
            BackButtonImage.sprite = BackOpenSprite;
            BackControlVolume.value = 0;
        }
        else
        {

            BackButtonImage.sprite = BackControlSprite;
            BackControlVolume.value = 0.1f;
        }

    }

    public void SFXControlSound()
    {

        gameControlSound = !gameControlSound;


        if (gameControlSound)
        {
            GameButtonImage.sprite = GameOpenSprite;
            GameControlVolume.value = 0;
        }
        else
        {

            GameButtonImage.sprite = GameControlSprite;
            GameControlVolume.value = 0.1f;
        }

    }







    public void PveButton()
    {
        //切換介面("下一個場景名稱")
        Application.LoadLevel("Level");
    }

    public void PvpButton()
    {
        //切換介面("下一個場景名稱")
        Application.LoadLevel("GamePVP");

    }
}
