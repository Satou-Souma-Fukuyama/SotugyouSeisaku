
using System.Collections;
using UnityEngine;

public class CameraScroller : MonoBehaviour
{
    [Header("スクロールする距離")]
    public int scrollDistance;

    [Header("スクロールにかかる時間")]
    public float scrollTime;

    private bool isScrolling = false;

    // カメラが移動中かどうか
    public bool IsScrolling { get; private set; }

    // カメラを右に移動する
    public void ScrollToBattle()
    {
        if (isScrolling) return;

        StartCoroutine(ScrollCoroutine());
    }

    private IEnumerator ScrollCoroutine()
    {
        IsScrolling = true;
        isScrolling = true;

        Vector3 startPosition = transform.position;

        // X方向に移動
        Vector3 endPosition = startPosition
            + new Vector3(scrollDistance, 0f, 0f);

        float elapsedTime = 0f;

        while (elapsedTime < scrollTime)
        {
            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp01(elapsedTime / scrollTime);

            transform.position = Vector3.Lerp(
                startPosition,
                endPosition,
                t
            );

            yield return null;
        }

        transform.position = endPosition;
        isScrolling = false;
        IsScrolling = false;
    }
}
