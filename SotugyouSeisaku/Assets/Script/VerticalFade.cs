using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;

public class VerticalFade : MonoBehaviour
{
    public RectTransform fadeTop;
    public RectTransform fadeBottom;

    public float fadeTime = 1f;

    //勇者
    public Transform hero;

    public float heroMove;

    public float moveDistance;

    void Start()
    {
        StartCoroutine(GameStartCoroutine());
    }

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
           GameStart();
        }

       
    }

    IEnumerator GameStartCoroutine()
    {
        // FadeIn
        yield return StartCoroutine(FadeInCoroutine());

        // FadeInが終わったら勇者を移動
        Vector3 heroStart = hero.position;

        Vector3 heroEnd = new Vector3(
            heroStart.x + moveDistance,
            heroStart.y,
            heroStart.z
        );

        float time = 0f;

        while (time < heroMove)
        {
            time += Time.deltaTime;

            float t = Mathf.Clamp01(time / heroMove);

            hero.position = Vector3.Lerp(
                heroStart,
                heroEnd,
                t
            );

            yield return null;
        }

        // 最終位置を確実に設定
        hero.position = heroEnd;

        // 勇者の移動が終わったらFadeOut
        yield return StartCoroutine(FadeOutCoroutine());
    }

    //画面が出てくる
    IEnumerator FadeInCoroutine()
    {
        float time = 0f;

        // 現在の中央位置
        Vector2 topStart = fadeTop.anchoredPosition;
        Vector2 bottomStart = fadeBottom.anchoredPosition;

        // 画面外へ移動
        Vector2 topEnd = new Vector2(
            topStart.x,
            topStart.y + fadeTop.rect.height
        );

        Vector2 bottomEnd = new Vector2(
            bottomStart.x,
            bottomStart.y - fadeBottom.rect.height
        );

        while (time < fadeTime)
        {
            time += Time.deltaTime;

            float t = Mathf.Clamp01(time / fadeTime);

            fadeTop.anchoredPosition =
                Vector2.Lerp(topStart, topEnd, t);

            fadeBottom.anchoredPosition =
                Vector2.Lerp(bottomStart, bottomEnd, t);

            yield return null;
        }

        fadeTop.anchoredPosition = topEnd;
        fadeBottom.anchoredPosition = bottomEnd;
    }

    //画面が黒くなる
    IEnumerator FadeOutCoroutine()
    {
        float time = 0f;

        // 現在の位置
        Vector2 topStart = fadeTop.anchoredPosition;
        Vector2 bottomStart = fadeBottom.anchoredPosition;

        // 画面中央まで移動
        Vector2 topEnd = new Vector2(
           topStart.x,
           541f
       );

        Vector2 bottomEnd = new Vector2(
          bottomStart.x,
          -540f
      );

        while (time < fadeTime)
        {
            time += Time.deltaTime;

            float t = Mathf.Clamp01(time / fadeTime);

            fadeTop.anchoredPosition =
                Vector2.Lerp(topStart, topEnd, t);

            fadeBottom.anchoredPosition =
                Vector2.Lerp(bottomStart, bottomEnd, t);

            yield return null;
        }

        fadeTop.anchoredPosition = topEnd;
        fadeBottom.anchoredPosition = bottomEnd;
    }



    public void GameStart()
    {
        StartCoroutine(GameStartCoroutine());
    }

   
}