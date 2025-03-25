using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnController : MonoBehaviour
{
    [Header("全ての敵情報"), SerializeField] EnemySO[] allEnemy;        // 全ての敵
    Range spawnRate = new Range{ min = 0.8f, max = 3f };                    // 初期生成間隔
    Range hundredsRateDec = new Range { min = 0.025f, max = 0.025f };       // 100waveごとに減少する値が滑らかになる
    const float minSpawnRateFactor = 0.05f;     // ランダム関数の最小値の減少具合
    const float MAX_MinSpawnRate = 0.025f;      // 減少できる最小値
    const float maxSpawnRateFactor = 0.075f;    // ランダム関数の最大値の減少具合
    const float MAX_MaxSpawnRate = 0.05f;       // 減少できる最小値
    Range rangeX = new Range { min = -5f, max = 5f };       // 敵の生成範囲 X
    Range rangeY = new Range { min = -1.5f, max = 3.5f };   // 敵の生成範囲 Y

    [Header("現在出現できる敵"), SerializeField] 
    List<EnemySO> currentEnemies = new List<EnemySO>();

    [Header("UI")]
    [SerializeField] TextMeshProUGUI waveCountText;
    [SerializeField] Slider waveTimerSlider;
    Image sliderFill;
    Color32 yellow = new Color32(255, 220, 100, 255);   // wave減少時のスライダーの色
    Color32 red = new Color32(255, 120, 100, 255);      // wave待機時のスライダーの色

    [Header("ステータス")]
    [SerializeField] int currentWaveCount;
    int accordingCount, hundredsWaveCount = 1;  // waveCountの代わり, 100waveごとにカウントする
    float waveEndTimer = 10f;       // 初期30
    bool isWaveEnd;     // true : wave終了, false : wave開始
    float readyTime = 10f, readyProgressTime;       // 次のwaveまでの待機時間, 経過時間


    void Start()
    {
        sliderFill = waveTimerSlider.fillRect.GetComponent<Image>();
        currentEnemies.Add(allEnemy[0]);        // 初期敵を最初にいれておく
        accordingCount = 0;

        // 最初にwaveスキップできるようにするため
        for(int ii = 0; ii < currentWaveCount; ii++)
        {
            // Waveに応じて追加要素を入れていく処理
            AddAccordingToWave();
        }

        InitializeSpawnRates();
        StartCoroutine(WaveTimer());
        StartCoroutine(SpawnEnemy());
    }

    /// <summary>
    /// 生成確立の初期化
    /// </summary>
    void InitializeSpawnRates()
    {
        float totalInitialRate = 0f;
        foreach (EnemySO enemy in currentEnemies)
        {
            totalInitialRate += enemy.InitialProbability;
        }

        // 初期生成確率を設定
        foreach (EnemySO enemy in currentEnemies)
        {
            enemy.CurrentSpawnProbability = enemy.InitialProbability / totalInitialRate;
        }
    }

    /// <summary>
    /// 敵を生成する
    /// </summary>
    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitUntil(() => isWaveEnd == false);       // 次のWaveが始まるまで待機

            // 一定時間待つ
            yield return new WaitForSeconds(Random.Range(spawnRate.min, spawnRate.max));

            if(GameSystem.Instance.IsGameOver == true) yield break;     // ゲームオーバーになったらコルーチンを抜ける
            if(isWaveEnd == true) continue;
            

            // 出現位置を決める
            Vector3 spawnPos = new Vector3(Random.Range(rangeX.min, rangeX.max), Random.Range(rangeY.min, rangeY.max), 0);

            AdjustSpawnRatesBasedOnWave();      // 現在のスポーン確率を調整数る
            GameObject selectEnemy = GetRandomEnemy();
            // 敵を生成
            Instantiate(selectEnemy, spawnPos, Quaternion.identity);
        }
    }

    /// <summary>
    /// ウェーブに基づくスポーン率の調整
    /// </summary>
    void AdjustSpawnRatesBasedOnWave()
    {
        float waveFactor = 1.0f + (currentWaveCount - 1) * 0.1f;        // ウェーブ数に応じた増減率
        float totalRate = 0f;

        foreach (EnemySO enemy in currentEnemies)
        {
            enemy.CurrentSpawnProbability = enemy.InitialProbability * waveFactor;
            totalRate += enemy.CurrentSpawnProbability;
        }

        // 確率が1になるように正規化
        foreach (EnemySO enemy in currentEnemies)
        {
            enemy.CurrentSpawnProbability /= totalRate;
        }
    }

    /// <summary>
    /// ランダムな敵を取得
    /// </summary>
    public GameObject GetRandomEnemy()
    {
        float randomValue = Random.value;       // 0.0 ~ 1.0の間のランダムな値を取得
        float cumulativeProbability = 0f;       // 累積確率

        foreach (EnemySO enemy in currentEnemies)
        {
            cumulativeProbability += enemy.CurrentSpawnProbability;
            if (randomValue < cumulativeProbability)
            {
                return enemy.Prefab;
            }
        }

        return currentEnemies[0].Prefab;    // デフォルトで初期敵を返す
    }

    /// <summary>
    /// Waveに応じて追加要素を入れていく処理
    /// </summary>
    void AddAccordingToWave()
    {
        accordingCount++;

        // 5waveごとに追加要素
        if(0 == accordingCount % 5)
        {
            // 100waveごとに減少する値が滑らかになる
            if(accordingCount > 100 * hundredsWaveCount)
            {
                hundredsWaveCount ++;
                hundredsRateDec.min *= 1.5f;
                hundredsRateDec.max *= 1.5f;
            }

            // スポーン頻度を上昇する
            spawnRate.min = spawnRate.min - (minSpawnRateFactor - hundredsRateDec.min);     // 最小値を減少させる
            spawnRate.max = spawnRate.max - (maxSpawnRateFactor - hundredsRateDec.max);     // 最大値値を減少させる
        }

        // 10waveごとに追加要素
        if(0 == accordingCount % 10)
        {
            int enemy = Mathf.Clamp(currentEnemies.Count, 0, allEnemy.Length - 1);
            if(currentEnemies.Contains(allEnemy[enemy]) == false)       // 同じ敵じゃなければ
            {
                // 敵を追加
                currentEnemies.Add(allEnemy[enemy]);
            }
        }

        // 50waveごとに0番目の敵を除外していく
        if(0 == accordingCount % 50)
        {
            if(currentEnemies.Count > 1)
            {
                // 0番目の敵を除外していく
                currentEnemies.Remove(currentEnemies[0]);
            }
        }

        // 限界値を確認する
        if(spawnRate.min < MAX_MinSpawnRate) { spawnRate.min = MAX_MinSpawnRate; }
        if(spawnRate.max < MAX_MaxSpawnRate) { spawnRate.max = MAX_MaxSpawnRate; }

        DebugManager.Instance.SpawnProbability = spawnRate;     // ログ
    }

    /// <summary>
    /// Waveが終了するまでの時間管理
    /// </summary>
    IEnumerator WaveTimer()
    {
        while(true)
        {
            // Wave開始
            isWaveEnd = false;
            currentWaveCount++;
            waveCountText.text = currentWaveCount.ToString("F0");
            AddAccordingToWave();

            sliderFill.color = yellow;      // 色を設定
            waveTimerSlider.maxValue = waveEndTimer;
            float time = waveEndTimer;
            while (time > 0)
            {
                DebugManager.Instance.WaveTime = time;
                waveTimerSlider.value = time;
                time -= Time.deltaTime;
                yield return null;
            }

            // Waveが終了
            isWaveEnd = true;

            // 次のWaveが始まるまでの準備時間を設ける
            yield return new WaitUntil(() => WaveTimeReady());
        }
    }

    /// <summary>
    /// 次のWaveに移るときの準備時間
    /// </summary>
    bool WaveTimeReady()
    {
        sliderFill.color = red;      // 色を設定
        waveTimerSlider.maxValue = readyTime;
        waveTimerSlider.value = readyProgressTime;
        DebugManager.Instance.WaveTime = readyProgressTime;

        readyProgressTime += Time.deltaTime;
        if(readyProgressTime > readyTime)
        {
            readyProgressTime = 0;
            return true;
        }

        return false;
    }
}
