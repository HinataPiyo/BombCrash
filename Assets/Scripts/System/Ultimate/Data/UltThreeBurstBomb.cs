using System.Collections;
using UnityEngine;

/// <summary>
/// 必殺技用爆弾にアタッチする処理
/// 三点バースト爆弾
/// </summary>
public class UltThreeBurstBomb : BombBase
{
    int burstCount;
    float nextBurstTime;

    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="_burstCount">バースト回数</param>
    /// <param name="_nextBurstTime">次の爆発までの時間</param>
    public void SetInit(int _burstCount, float _nextBurstTime, Vector2 explosionPos)
    {
        isUlt = true;
        ExplosionPoint = explosionPos;
        burstCount = _burstCount;
        nextBurstTime = _nextBurstTime;
        StartCoroutine(Execute());
    }

    /// <summary>
    /// 処理を実行
    /// </summary>
    IEnumerator Execute()
    {
        yield return new WaitUntil(() => isFirstBoom);

        for (int ii = 0; ii < burstCount; ii++)
        {
            yield return new WaitForSeconds(nextBurstTime);
            base.BOOM();        // 爆発
        }

        Destroy(gameObject);
    }

}