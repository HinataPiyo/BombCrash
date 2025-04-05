using UnityEngine;

public class DragNode : MonoBehaviour
{
    [SerializeField] Transform nodeParent;
    [SerializeField] RectTransform nodeParent_rectTransform;
    RectTransform rectTransform;
    private Vector3 dragOrigin; // ドラッグ開始位置
    private bool isDragging = false;
    Vector3[] corners;


    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))    // 左クリック
        {
            if(CheckOnCursor() == false) return;
            isDragging = true;      // ドラッグ開始の合図
        }

        // ドラッグ中
        if (Input.GetMouseButton(0) && isDragging)
        {
            // クリックした位置を"isDragging"が"true"の時、スクリーン座標からワールド座標に変換
            Vector3 currentPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // 最初にクリックした位置と現在のカーソルの位置の距離を計算
            Vector3 difference = dragOrigin - currentPoint;

            // ターゲットオブジェクトを移動
            nodeParent.position -= difference;

            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0)) // マウスボタンを離したとき
        {
            isDragging = false;
        }
    }

    public void PosisionReset()
    {
        nodeParent_rectTransform.anchoredPosition = Vector2.zero;
    }

    /// <summary>
    /// 画面内に入っているか確認する
    /// </summary>
    bool CheckOnCursor()
    {
        // クリックした位置をスクリーン座標からワールド座標に変換
        dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return dragOrigin.x >= corners[1].x && dragOrigin.x <= corners[2].x
        && dragOrigin.y <= corners[1].y && dragOrigin.y >= corners[0].y;
    }
}
