using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBGM : MonoBehaviour
{
    private void Awake()
    {
        //套場景不能刪除(物件)
       // DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

    }


    void Update()
    {
        if (Application.loadedLevelName != "Menu" && Application.loadedLevelName != "Level" && Application.loadedLevelName != "LoginAccount")
        {
            GetComponent<AudioSource>().enabled = true;

        }
        else
        {
            GetComponent<AudioSource>().enabled = false;
        }

    }
}
