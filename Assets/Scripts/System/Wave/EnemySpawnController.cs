using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    WaveManager wm;

    // 生成範囲
    static readonly Range rangeX = new Range { min = -5f, max = 5f };       // 敵の出現位置（X軸範囲）
    static readonly Range rangeY = new Range { min = -1.5f, max = 3.5f };   // 敵の出現位置（Y軸範囲）
    List<GameObject> enemyList = new List<GameObject>();
    public List<GameObject> EnemyList => enemyList;

    void Start() => wm = WaveManager.Instance;
    void Update() => enemyList.RemoveAll(enemyList => enemyList == null);
    public void StartSpawnEnemy(IntervalWaveData data) => StartCoroutine(SpawnEnemiesCoroutine(data));


    /// <summary>
    /// 敵の出現処理（Wave中のみ動作）
    /// 一定間隔で敵を出現させる
    /// </summary>
    IEnumerator SpawnEnemiesCoroutine(IntervalWaveData wave)
    {
        float elapsed = 0f;

        while (!wm.IsWaveEnd && elapsed < wave.waveDuration)
        {
            SpawnEnemy(wave);
            yield return new WaitForSeconds(wave.spawnInterval);
            elapsed += wave.spawnInterval;
        }
    }

    /// <summary>
    /// 敵をランダム位置に生成
    /// </summary>
    private void SpawnEnemy(IntervalWaveData wave)
    {
        if (GameSystem.Instance.IsGameOver == true) return;
        GameObject prefab = ChooseEnemy(wave.spawnOptions); // 重みによるランダム選択
        Vector2 pos = new Vector2(
            Random.Range(rangeX.min, rangeX.max),
            Random.Range(rangeY.min, rangeY.max)
        );
        GameObject enemy = Instantiate(prefab, pos, Quaternion.identity);   // 敵生成
        EnemyStatus status = enemy.GetComponent<EnemyStatus>();
        status.SetOrderInLayer(enemyList.Count);
        status.EnemySO.ResetMaxHp();
        EnemyStatusUP(status);       // HPを上昇させる

        enemyList.Add(enemy);
    }

    /// <summary>
    /// 敵の出現候補から重み（weight）に基づいてランダムに1体選ぶ
    /// </summary>
    private GameObject ChooseEnemy(IntervalWaveData.EnemySpawnOption[] options)
    {
        float total = 0f;
        foreach (var opt in options) total += opt.weight;

        float r = Random.Range(0f, total);
        float acc = 0f;

        foreach (var opt in options)
        {
            acc += opt.weight;
            if (r <= acc) return opt.enemySO.Enemy_Prefab;
        }

        return options[0].enemySO.Enemy_Prefab; // 念のためのフォールバック（確率がおかしくても何か出す）
    }

    /// <summary>
    /// 敵のステータス上昇
    /// </summary>
    /// <param name="status"></param>
    void EnemyStatusUP(EnemyStatus status)
    {
        // 100waveごとに大幅に上昇
        if (wm.WaveCount > 0 && wm.WaveCount % 100 == 0)
        {
            status.EnemySO.UpMaxHp = 1.5f * wm.WaveCount / 100;
        }

        // 毎waveごとに上昇
        float increase = status.EnemySO.DefaultMaxHp * (wm.waveRule.enemyHpUp * (wm.WaveCount - status.EnemySO.StartWave));
        status.SetHpUP(increase);
    }

    /// <summary>
    /// 敵のカウントダウンが0になる敵を選出する
    /// </summary>
    /// <returns>カウントダウンが0になる敵のTransform</returns>
    /// <remarks>敵がいない場合はnullを返す</remarks>
    public Transform CrossTo0Enemy()
    {
        GameObject lowestCountdownEnemy = null;
        float lowestCountdown = float.MaxValue;

        foreach (var enemy in enemyList)
        {
            if (enemy != null)
            {
                var enemyBombController = enemy.GetComponent<EnemyBombController>();
                if (enemyBombController != null)
                {
                    float countdownTime = enemyBombController.CountDownTime;
                    if (countdownTime < lowestCountdown)
                    {
                        lowestCountdown = countdownTime;
                        lowestCountdownEnemy = enemy;
                    }
                }
            }
        }

        if (lowestCountdownEnemy != null)
        {
            // 必要に応じてここで選出した敵に対する処理を追加
            return lowestCountdownEnemy.transform;
        }
        else
        {
            Debug.Log("敵が存在しないか、カウントダウンを持つ敵がいません。");
            return null;
        }
    }

    /// <summary>
    /// Waveのインターバル処理（スライダーでカウントダウン）
    /// </summary>
    public bool FieldOnEnemiesCheck() => enemyList.Count == 0;
}