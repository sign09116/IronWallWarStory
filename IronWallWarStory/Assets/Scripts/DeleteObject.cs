using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteObject : MonoBehaviour
{
    [Header("物件生成後刪除時間")]
    public float DeleteTime = 2f;

    [SerializeField] MusicData m_data;
    [SerializeField] AudioSource aud;

    void Start()
    {
        //刪除時間
        Destroy(gameObject, DeleteTime);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
