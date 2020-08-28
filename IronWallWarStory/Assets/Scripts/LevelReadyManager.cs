using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelReadyManager : MonoBehaviour
{
    //門動畫
    private Animator door;
    //開門
    [SerializeField] GameObject Transitionsobj;


    public Scene scene;
    void Start()
    {
        door = Transitionsobj.GetComponent<Animator>();
        scene = SceneManager.GetActiveScene();
        if (scene.name != "Level")
        {
            door.SetTrigger("開門");
        }

    }

    ///<summary>開始戰鬥按鈕</summary>
    public void Startfighting()
    {
        door.SetTrigger("關門");
        // SceneManager.LoadScene("Game1_1");
    }


}
