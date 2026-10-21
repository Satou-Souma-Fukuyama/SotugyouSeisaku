
using System.Collections;
using UnityEngine;

public class CameraScroller : MonoBehaviour
{
    [Header("スクロールする距離")]
    public int scrollDistance;

    [Header("スクロールにかかる時間")]
    public float scrollTime;

    public GameObject ingredientsPanel1;
    public GameObject ingredientsPanel2;
    public GameObject ingredientsPanel3;

    

    private bool isScrolling = false;

    // カメラが移動中かどうか
    public bool IsScrolling { get; private set; }

    // カメラを右に移動する
    public void ScrollToBattle()
    {

        Debug.Log("ScrollToBattleが呼ばれました");

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
        ingredientsPanel1.SetActive(true);
        ingredientsPanel2.SetActive(true);
        ingredientsPanel3.SetActive(true);
    }
}
