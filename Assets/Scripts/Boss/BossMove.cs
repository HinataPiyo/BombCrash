using UnityEngine;
using DG.Tweening;


public class BossMove : MonoBehaviour
{
    // ボスユニットが通過する座標
    [SerializeField] Vector3[] wayPointA;
    [SerializeField] Vector3[] wayPointB;

    // 移動時間
    [SerializeField] float moveTime;

    Sequence sequence1;
    Sequence sequence2;

    void Start()
    {
        sequence1 = DOTween.Sequence()
            // パスに沿って移動
            .Append(transform.DOPath(wayPointA, moveTime, PathType.Linear).SetOptions(true))
            // パスを１周したら少し待機
            .AppendInterval(0.5f)
            // 無限ループ
            .SetLoops(-1)
            // 自動で破棄しない
            .SetAutoKill(false)
            // オブジェクトが削除されたタイミングで破棄
            .SetLink(gameObject);

        // ゲーム開始時に自動で動かす
        sequence1.Play();
    }

    public void MovePath1()
    {
        // sequence2がnullでなければ停止
        if (sequence2 != null)
        {
            sequence2.Pause();
        }

        var path1 = DOTween.Sequence();

        // パスのスタート地点に移動
        path1.Append(transform.DOMove(wayPointA[0], 1.0f))
            // アニメーションを再生
            .AppendCallback(() => sequence1.Restart())
            .SetLink(gameObject);
    }
}