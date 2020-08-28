using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaterMVCGame : MonoBehaviour
{
    private void Start()
    {
        GameObject mvcGame = GameObject.FindGameObjectWithTag("MVCGame");
        if (mvcGame == null)
            Instantiate(Resources.Load("MVCGame"));
    }
}
