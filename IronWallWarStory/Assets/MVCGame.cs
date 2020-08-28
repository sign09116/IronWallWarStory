using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVCGame : MonoBehaviour
{
    public static MVCGame instance;
    [SerializeField] SceneData data;
    public int rank = 1;
    void Start()
    {
        data.level = rank;
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public int currentchapter = 0;
    public int currentLevel = 0;


}
