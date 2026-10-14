
using UnityEngine;

public class DraggableIngredient : MonoBehaviour
{
    private Camera mainCamera;
    private CameraScroller cameraScroller;
    private Rigidbody2D rb;

    private Vector2 dragOffset;
    private bool isDragging = false;
    private float originalGravityScale;

    void Start()
    {
        mainCamera = Camera.main;

        cameraScroller = FindFirstObjectByType<CameraScroller>();

        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            originalGravityScale = rb.gravityScale;
        }
        else
        {
            Debug.LogWarning(
                "Rigidbody2Dが設定されていません：" + gameObject.name
            );
        }
    }

    // マウスを押したとき
    void OnMouseDown()
    {
        // カメラ移動中はつかめない
        if (cameraScroller != null && cameraScroller.IsScrolling)
            return;

        if (mainCamera == null)
            mainCamera = Camera.main;

        if (mainCamera == null || rb == null)
            return;

        Vector2 mousePosition = GetMouseWorldPosition();

        // 食材とマウスの位置のずれを保存
        dragOffset = rb.position - mousePosition;

        isDragging = true;

        // ドラッグ中は重力を止める
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;
    }

    // マウスを押したまま動かしたとき
    void OnMouseDrag()
    {
        if (!isDragging || mainCamera == null || rb == null)
            return;

        // カメラ移動開始時はドラッグを中断
        if (cameraScroller != null && cameraScroller.IsScrolling)
        {
            StopDragging();
            return;
        }

        Vector2 mousePosition = GetMouseWorldPosition();

        // 物理演算を使って移動する
        Vector2 targetPosition = mousePosition + dragOffset;

        rb.MovePosition(targetPosition);
    }

    // マウスを離したとき
    void OnMouseUp()
    {
        StopDragging();
    }

    // ドラッグ終了処理
    private void StopDragging()
    {
        isDragging = false;

        if (rb != null)
        {
            rb.gravityScale = originalGravityScale;
        }
    }

    // マウス座標をワールド座標に変換
    private Vector2 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;

        mousePosition.z =
            transform.position.z - mainCamera.transform.position.z;

        return mainCamera.ScreenToWorldPoint(mousePosition);
    }
}
