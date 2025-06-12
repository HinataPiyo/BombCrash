using UnityEngine;

public class OtomoMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite; // Otomoのスプライトを操作するためのSpriteRenderer
    [SerializeField] private float baseMoveSpeed = 3f; // 基本の移動速度
    [SerializeField] private float distanceThreshold = 2f; // 移動を開始する距離の閾値
    [SerializeField] private float closeDistanceThreshold = 0.5f; // 近距離での閾値

    private Transform playerPos; // プレイヤーの位置を参照するTransform
    private Transform movePoint; // Otomoが移動する目標地点のTransform
    private float moveSpeed; // 現在の移動速度

    private void Start()
    {
        // 初期化時に移動速度を基本移動速度に設定
        moveSpeed = baseMoveSpeed;
    }

    private void Update()
    {
        // 毎フレーム、スプライトの向きを更新
        UpdateSpriteDirection();
        // 毎フレーム、目標地点に向かって移動
        MoveTowardsPoint();
    }

    private void UpdateSpriteDirection()
    {
        // プレイヤーの位置が設定されていない場合は処理を中断
        if (playerPos == null) return;

        // プレイヤーの位置に基づいてスプライトの向きを変更
        sprite.flipX = playerPos.position.x > 0;
    }

    private void MoveTowardsPoint()
    {
        // 目標地点が設定されていない場合は処理を中断
        if (movePoint == null) return;

        // Otomoと目標地点の距離を計算
        float distance = Vector3.Distance(movePoint.position, transform.position);
        // Otomoが目標地点に向かうための方向を計算
        Vector3 direction = (movePoint.position - transform.position).normalized;

        // 距離が閾値(distanceThreshold)より大きい場合
        if (distance > distanceThreshold)
        {
            // 移動速度を徐々に増加させながら移動
            MoveInDirection(direction);
            moveSpeed += 0.01f;
        }
        // 距離が近距離閾値(closeDistanceThreshold)より大きく、かつdistanceThreshold以下の場合
        else if (distance > closeDistanceThreshold)
        {
            // 移動速度をリセットして移動
            moveSpeed = baseMoveSpeed;
            MoveInDirection(direction);
        }
    }

    private void MoveInDirection(Vector3 direction)
    {
        // 指定された方向に移動
        transform.position += new Vector3(direction.x, 0) * moveSpeed * Time.deltaTime;
    }

    public void SetOtomoMovePoint(Transform pointPos)
    {
        // Otomoの目標地点を設定
        movePoint = pointPos;
    }

    public void SetPlayerPos(Transform player)
    {
        // プレイヤーの位置を設定
        playerPos = player;
    }
}