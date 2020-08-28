using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HpBarControl : MonoBehaviour
{
   public Image imgHp;
   public Text textDamage;
    

    ///<summary>喚醒事件</summary>
    private void Awake()
    {
        imgHp = transform.GetChild(1).GetComponent<Image>();
        textDamage = transform.GetChild(2).GetComponent<Text>();
    }

    private void Update()
    {
        AngleControl();
    }

    ///<summary>角度控制</summary>
    private void AngleControl()
    {
        //讓血條維持座標
        //變形元件.歐拉角度=新 三維向量(-90, 0, 0)
        transform.eulerAngles = new Vector3(-90, 0, 0);
    }

    ///<summary>更新血條圖片，提供目前血量</summary>
    ///<param name="hpMax">最大血量</param>
    ///<param name="hpCurrent">受傷後血量</param>
    public void UpdateHpBar(float hpMax, float hpCurrent)
    {
        //圖片.填滿數值=目前/最大
        imgHp.fillAmount = hpCurrent / hpMax;

    }

    //協程方法:顯示傷害值-接收傷害
    ///<summary><param name="damage">要顯示的傷害值</summary>
    public IEnumerator ShowDamage(float damage)
    {
        //取得原始位置
        Vector3 posOriginal = textDamage.transform.position;
        //更新傷害值=接收傷害值
        textDamage.text = "-" + damage;
        //迴圈
        for (int i = 0; i < 20; i++)
        {
            //傷害值往上移動
            textDamage.transform.position += new Vector3(0, 0.0005f, 0);
            //等待
            yield return new WaitForSeconds(0.1f);
        }
        //位置=原始位置
        textDamage.transform.position = posOriginal;
        //文字=""
        textDamage.text = "";
    }
}
