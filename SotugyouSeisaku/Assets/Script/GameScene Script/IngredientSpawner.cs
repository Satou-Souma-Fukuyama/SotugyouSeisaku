using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IngredientSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("食材を出す場所")]
    public Transform spawnPoint;

    [Header("食材Prefab")]
    public GameObject potatoPrefab;
    public GameObject tomatoPrefab;

    public GameObject ingredientsPanel;

    [Header("袋に詰め込む時間")]
    public int baggingTime;

    public Text timerText;

    public CameraScroller cameraScroller;

    private bool isFinished = false;


    //public GameObject meatPrefab;
    // public GameObject applePrefab;
    void Start()
    {
        StartCoroutine(BaggingTimer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    // ジャガイモ
    public void SpawnPotato()
    {
        SpawnIngredient(potatoPrefab);
    }

    // 人参
    public void Spawntomato()
    {
        SpawnIngredient(tomatoPrefab);
    }

    // 食材を生成
    private void SpawnIngredient(GameObject ingredient)
    {
        if (ingredient == null)
        {
            Debug.LogWarning("食材Prefabが設定されていません");
            return;
        }

        Instantiate(
            ingredient,
            spawnPoint.position,
            Quaternion.identity
        );
    }

    public void Bakkiningtime()
    {
        if (isFinished) return;
        isFinished = true;

        // 食材パネルを非表示
        if (ingredientsPanel != null)
        {
            ingredientsPanel.SetActive(false);
        }

        // タイマーを非表示にする
        if (timerText != null)
        {
            timerText.gameObject.SetActive(false);
        }

        // カメラを移動
        if (cameraScroller != null)
        {
            cameraScroller.ScrollToBattle();
        }
        else
        {
            Debug.LogWarning("CameraScrollerが設定されていません");
        }
    }

    // 袋詰めタイマー
    private IEnumerator BaggingTimer()
    {
        int remainingTime = baggingTime;

        while (remainingTime > 0)
        {
            UpdateTimerText(remainingTime);

            yield return new WaitForSeconds(1f);

            remainingTime--;
        }

        UpdateTimerText(0);

        // 60秒経過したら袋詰め終了
        Bakkiningtime();
    }

    // タイマー表示を更新
    private void UpdateTimerText(int time)
    {
        if (timerText != null)
        {
            timerText.text = "残り時間"+ time + "秒";
        }
    }
}
