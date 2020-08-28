using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager
{
   
    public static SaveManager instance
    {
        get
        {  
         return _instance= new SaveManager();
        } 
       
    }
    static  SaveManager _instance;
   
    public AllData allData;
    /// <summary>讀檔 </summary> 
    public void Load(PlayerData data = null)
    {
        allData = JsonUtility.FromJson<AllData>( PlayerPrefs.GetString("AllSaveData", "") );
        //data.playerName = allData.name;
        //data.playerLV = allData.level;
        //data.exp = allData.exp;
    }
    /// <summary>存檔 </summary> 
    public void Save (string playerName,PlayerData data=null)
    {
        allData.name = playerName;
        if (data == null)
        {
            allData.level = 1;
            allData.exp = 0f;
        }
        else
        {
            allData.level = data.playerLV;
            allData.exp = data.exp;
        }
        PlayerPrefs.SetString("AllSaveData",JsonUtility.ToJson(allData));
    }
}
/// <summary>玩家所有資料 </summary>
[System.Serializable]
public struct AllData
{
    [SerializeField]
    public string name;
    [SerializeField]
    public int level;
    [SerializeField]
    public float exp;
    
   
}
