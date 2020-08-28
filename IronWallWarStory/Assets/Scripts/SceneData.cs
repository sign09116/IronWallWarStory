using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "關卡資料")]
public class SceneData : ScriptableObject
{
    public int level = 0;
    public List<EnemyData> enemyDataList = new List<EnemyData>();
    public List<NPCData>npcDataList = new List<NPCData>();
    public PlayerData playerData;
}
