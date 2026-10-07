using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StageManager : MonoBehaviour
{
    [Header("このステージ番号")]
    public int stageNumber = 1;

    [Header("戻るマップScene")]
    public string mapSceneName = "MapLoadScene";

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StageClear();
        }
    }

    public void StageClear()
    {
        Debug.Log("【Stage" + stageNumber + "】クリア！");

        PlayerPrefs.SetInt(
            "MapProgress",
            stageNumber
        );

        PlayerPrefs.Save();

        Debug.Log(
            "【Stage】MapProgress = " +
            PlayerPrefs.GetInt("MapProgress")
        );

        SceneManager.LoadScene(mapSceneName);
    }
}