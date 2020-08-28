using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelRank : MonoBehaviour
{
    public static LevelRank instance;
    
    [SerializeField] Text[] rank_text;
   
    #region 單例
    private void Awake()
    {
        instance = this;
    }
    #endregion
    private void Start()
    {
        
        
    }
    
}
