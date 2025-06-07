using UnityEngine;

/// <summary>
/// 三点バーストを行う必殺技を管理するデータクラス
/// </summary>
[CreateAssetMenu(fileName = "UltThreeBurst", menuName = "UltimateSO/ThreeBurst")]
public class UltThreeBurst : UltimateSO
{
    [SerializeField] GameObject bomb_Prefab;
    static readonly int burstCount = 2;
    static readonly float nextBurstTime = 1f;

    /// <summary>
    /// 実行処理
    /// </summary>
    /// <param name="player">プレイヤーの位置情報</param>
    /// <param name="explosionPos">爆発位置、照準</param>
    public override void ExecuteUltimate(Transform player, Vector2 explosionPos)
    {
        GameObject bomb = Instantiate(bomb_Prefab, player.position, Quaternion.identity);
        bomb.GetComponent<UltThreeBurstBomb>().SetInit(burstCount, nextBurstTime, explosionPos);
    }
}