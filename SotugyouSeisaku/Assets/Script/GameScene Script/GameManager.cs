using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("袋詰め画面")]
    public GameObject ingredientsPanel;
    public GameObject timerObject;

    [Header("カメラ移動")]
    public CameraScroller cameraScroller;

    [Header("各ステージの敵")]
    public GameObject enemy;

    [Header("ヒーロー")]

    public GameObject hero;
    public GameObject heroNamePanel;

    [Header("鍋のパネル")]
    public GameObject ingredientsPanel1;
    public GameObject ingredientsPanel2;
    public GameObject ingredientsPanel3;

    [Header("敵のHPUI")]
    public GameObject hPBar;
    public GameObject enemyName;
    public GameObject enemyNamePanel;

    [Header("自動戦闘設定")]
    public int attackDamage;
    public float attackInterval;

    private bool isBaggingFinished = false;
    private bool isBattleStarted = false;
    private EnemyHP enemyHP;

    //private bool isBaggingFinished = false;


    void Start()
    {
        //ゲーム開始時非表示にするもの
        //  ---鍋パネル---
        ingredientsPanel1.SetActive(false);
        ingredientsPanel2.SetActive(false);
        ingredientsPanel3.SetActive(false);
        //  ---敵---
        enemy.SetActive(false);
        hPBar.SetActive(false);
        enemyNamePanel.SetActive(false);
        //ヒーロー
        hero.SetActive(false);
        heroNamePanel.SetActive(false); 
        //非表示ここまで

    }

    // 袋詰めが終了したときに呼び出す
    public void BaggingFinished()
    {
        // 二重実行を防止
        if (isBaggingFinished) return;
        isBaggingFinished = true;

        Debug.Log("袋詰め終了");

        // 食材パネルを非表示
        if (ingredientsPanel != null)
        {
            ingredientsPanel.SetActive(false);
        }

        // タイマーを非表示
        if (timerObject != null)
        {
            timerObject.SetActive(false);
        }

        // 戦闘画面への移動処理を開始
        StartCoroutine(BattleTransition());
        /*
        // カメラを移動
        if (cameraScroller != null)
        {
            cameraScroller.ScrollToBattle();

            //カメラ移動開始したらステージの敵描写
            Enemy.SetActive(true);
        }
        else
        {
            Debug.LogWarning(
                "CameraScrollerが設定されていません"
            );
        }*/
    }


    // カメラ移動が終わってから戦闘画面を表示
    private IEnumerator BattleTransition()
    {
        if (cameraScroller == null)
        {
            Debug.LogWarning("CameraScrollerが設定されていません");
            yield break;
        }

        // 敵を表示
        if (enemy != null)
            enemy.SetActive(true);

        // ヒーローを表示
        if (hero != null)
            hero.SetActive(true);

        // カメラ移動開始
        cameraScroller.ScrollToBattle();

        // カメラの移動が終わるまで待機
        while (cameraScroller.IsScrolling)
        {
            yield return null;
        }

        Debug.Log("カメラ移動終了");

        // 敵のHPバーを表示
        if (hPBar != null)
        {
            hPBar.SetActive(true);
        }

        //敵とヒーローの名前パネル表示
        enemyNamePanel.SetActive(true);
        heroNamePanel.SetActive(true);

        Debug.Log("HP表示");

        // 鍋のパネルを表示
        if (ingredientsPanel1 != null)
            ingredientsPanel1.SetActive(true);

        if (ingredientsPanel2 != null)
            ingredientsPanel2.SetActive(true);

        if (ingredientsPanel3 != null)
            ingredientsPanel3.SetActive(true);

        Debug.Log("戦闘開始！");

        // 鍋パネル表示後に自動戦闘を開始
        StartAutoBattle();
    }

    private void StartAutoBattle()
    {
        if (isBattleStarted) return;
        isBattleStarted = true;

        if (enemy == null)
        {
            Debug.LogWarning("Enemyが設定されていません");
            return;
        }

        // 敵本体または子オブジェクトからHPスクリプトを取得
        enemyHP = enemy.GetComponentInChildren<EnemyHP>();

        if (enemyHP == null)
        {
            Debug.LogWarning(
                "EnemyにSlimeHP.csが見つかりません"
            );
            return;
        }

        Debug.Log("自動戦闘開始！");

        StartCoroutine(AutoAttack());
    }

    // 一定時間ごとに敵へダメージを与える
    private IEnumerator AutoAttack()
    {
        while (enemyHP != null)
        {
            // 設定した秒数だけ待つ
            yield return new WaitForSeconds(attackInterval);

            // 待機中に敵が倒されていないか確認
            if (enemyHP == null)
                yield break;

            // 敵へダメージ
            enemyHP.TakeDamage(attackDamage);

            Debug.Log("勇者の攻撃！ " + attackDamage + " ダメージ");
        }
    }
}
