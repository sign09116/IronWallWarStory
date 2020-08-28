using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBGM : MonoBehaviour
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
        if (Application.loadedLevelName == "Menu" && Application.loadedLevelName == "Level")
        {
            GetComponent<AudioSource>().enabled = false;
        }
        else
        {
            if (Application.loadedLevelName != "Menu" && Application.loadedLevelName != "Level")
            {
                GetComponent<AudioSource>().enabled = false;
            }
        }

    }
}
