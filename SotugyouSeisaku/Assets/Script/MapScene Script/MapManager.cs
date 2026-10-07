using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MapManager : MonoBehaviour
{
    [Header("勇者")]
    public Transform hero;

    [Header("初期地点")]
    public Transform startPoint;

    [Header("ステージ地点")]
    public Transform[] stagePoints;

    [Header("ステージScene")]
    public string[] stageScenes;

    [Header("移動設定")]
    public float moveTime = 2f;

    [Header("開始までの待ち時間")]
    public float startWaitTime = 0.5f;

    [Header("フェード")]
    public VerticalFade verticalFade;


    void Start()
    {
        StartCoroutine(MapStartCoroutine());
    }

    private void Update()
    {
        //ステージリセット
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteKey("MapProgress");
            PlayerPrefs.Save();

            Debug.Log("【テスト】進行度をリセットしました");
        }
    }


    IEnumerator MapStartCoroutine()
    {
        // 現在の進行度を取得
        int progress = PlayerPrefs.GetInt("MapProgress", 0);

        Debug.Log("現在の進行度：" + progress);


        // 初回プレイ

        if (progress == 0)
        {
            Debug.Log("初回プレイ：初期地点からStage1へ");

            // 勇者を初期地点へ配置
            hero.position = startPoint.position;

            // 少し待つ
            yield return new WaitForSeconds(startWaitTime);

            Debug.Log("【Map】FadeIn開始");

            // フェードイン
            yield return StartCoroutine(
                verticalFade.FadeInCoroutine()
            );

            // 初期地点からStage1
            yield return StartCoroutine(
                MoveHero(stagePoints[0].position)
            );

            Debug.Log("【Map】Stage1Pointに到着");

            Debug.Log("【Map】FadeOut開始");

            // フェードアウト
            yield return StartCoroutine(
                verticalFade.FadeOutCoroutine()
            );

            // Stage1へ
            SceneManager.LoadScene(stageScenes[0]);

            yield break;
        }


        // Stage1以降
        // 次のステージへ

        int nextStage = progress;

        Debug.Log("【Map】クリア済み：Stage" + progress);
        Debug.Log("【Map】次の目的地：Stage" + (nextStage + 1));


        // 全ステージクリア

        if (nextStage >= stagePoints.Length)
        {
            Debug.Log("全ステージクリア！");

            yield break;
        }


        // 前のステージ地点に勇者を配置

        hero.position = stagePoints[progress - 1].position;


        yield return new WaitForSeconds(startWaitTime);


        // フェードイン
        yield return StartCoroutine(
            verticalFade.FadeInCoroutine()
        );


        // 次のステージへ移動

        yield return StartCoroutine(
            MoveHero(stagePoints[nextStage].position)
        );


        // フェードアウト
        yield return StartCoroutine(
            verticalFade.FadeOutCoroutine()
        );


        // 次のステージSceneを読み込む
        if (nextStage < stageScenes.Length)
        {
            SceneManager.LoadScene(stageScenes[nextStage]);
        }
    }


    // 勇者を目的地まで移動

    IEnumerator MoveHero(Vector3 targetPosition)
    {
        Vector3 startPosition = hero.position;

        float time = 0f;


        while (time < moveTime)
        {
            time += Time.deltaTime;

            float t = Mathf.Clamp01(time / moveTime);

            hero.position = Vector3.Lerp(
                startPosition,
                targetPosition,
                t
            );

            yield return null;
        }


        // 最終位置を合わせる
        hero.position = targetPosition;


        while (Vector3.Distance(
           hero.position,
           targetPosition
       ) > 0f)
        {
            hero.position = targetPosition;

            yield return null;
        }

        Debug.Log("【勇者】目的地に到着");
    }
}