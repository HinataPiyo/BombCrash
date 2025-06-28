using UnityEngine;

public class BossSpawnController : MonoBehaviour
{
    public static readonly int BossWaveCount = 9;   // ボスの出現wave数
    [SerializeField] Transform spawnPoint;      // ボスの出現位置
    public bool IsBossSpawned { get; private set; }     // ボスが出現しているかどうか
    public GameObject bossPrefab;          // ボスのプレハブ

    /// <summary>
    /// ボスを生成する処理
    /// </summary>
    public GameObject SpawnBoss()
    {
        Debug.Log("kita");
        if (IsBossSpawned) return null;
        Debug.Log("rreturn");
        if (spawnPoint == null || bossPrefab == null) return null;
        Debug.Log("kokoyo");

        // ボスを出現させる処理
        GameObject boss = Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
        IsBossSpawned = true;
        Debug.Log("kokoomade");
        return boss;
    }

    /// <summary>
    /// ボスを倒したときの処理
    /// </summary>
    public void DieBoss()
    {
        IsBossSpawned = false;
    }
}