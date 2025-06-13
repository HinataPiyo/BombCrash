using UnityEngine;

public class BossSpawnController : MonoBehaviour
{
    public static readonly int BossWaveCount = 25;   // ボスの出現wave数
    [SerializeField] Transform spawnPoint;      // ボスの出現位置
    public bool IsBossSpawned { get; private set; }     // ボスが出現しているかどうか
    GameObject bossPrefab;          // ボスのプレハブ

    public void SpawnBoss()
    {
        if (IsBossSpawned) return;      // すでにボスが出現している場合は何もしない

        // ボスを出現させる処理
        GameObject boss = Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
        IsBossSpawned = true;
    }
}