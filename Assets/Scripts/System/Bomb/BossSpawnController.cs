using UnityEngine;

public class BossSpawnController : MonoBehaviour
{
    bool isBossSpawned = false;     // ボスが出現しているかどうか
    GameObject bossPrefab;          // ボスのプレハブ
    [SerializeField] Transform spawnPoint;      // ボスの出現位置

    public void SpawnBoss()
    {
        if (isBossSpawned) return;      // すでにボスが出現している場合は何もしない

        // ボスを出現させる処理
        GameObject boss = Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
        isBossSpawned = true;
    }

    /// <summary>
    /// ボスが出現しているかどうかを確認するメソッド
    /// </summary>
    /// <returns>true: ボスが出現している / false: ボスが出現していない</returns>
    public bool IsBossSpawned()
    {
        return isBossSpawned;
    }
}