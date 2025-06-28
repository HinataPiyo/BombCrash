using UnityEngine;

public class BossSpawnController : MonoBehaviour
{
    public static readonly int BossWaveCount = 9;   // ボスの出現wave数
    [SerializeField] Transform spawnPoint;      // ボスの出現位置
    public bool IsBossSpawned { get; private set; }     // ボスが出現しているかどうか
    public GameObject bossPrefab;          // ボスのプレハブ

    public void SpawnBoss()
    {
        Debug.Log("kita");
        if (IsBossSpawned) return;      // 
        Debug.Log("rreturn");
        if (spawnPoint == null || bossPrefab == null) return;
        Debug.Log("kokoyo");

        // ボスを出現させる処理
        GameObject boss = Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
        IsBossSpawned = true;
        Debug.Log("kokoomade");
    }
}