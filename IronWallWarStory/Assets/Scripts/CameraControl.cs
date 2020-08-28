using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CJW
{
    public class CameraControl : MonoBehaviour
    {
        [Header("速度")]
        public float speed = 1.5f;
        [Header("上方限制")]
        public float top;
        [Header("下方限制")]
        public float bottom;

        private Transform player;

        private void Start()
        {
            player = GameObject.Find("PlayerTank").GetComponent<Transform>();
        }

        //Update執行後:做物件追蹤
        private void LateUpdate()
        {
            Track();
        }

        ///<summary>攝影機追蹤玩家方式</summary>
        private void Track()
        {
            Vector3 posPlayer = player.position;       //玩家.座標
            Vector3 posCamera = transform.position;    //變形.座標

            posPlayer.y = 12;      //限制攝影機在玩家座標y
            posPlayer.z += 15;     //限制攝影機在玩家座標z

            //玩家.z=數學.夾住(玩家.z,上限,下限)
            posPlayer.z = Mathf.Clamp(posPlayer.z, top, bottom);

            //變形.座標=三維向量.插旗(攝影機,玩家,百分比)
            transform.position = Vector3.Lerp(posCamera, posPlayer, 0.5f * Time.deltaTime * speed);
        }

    }
}
