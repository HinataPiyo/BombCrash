using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// EndlessWaveRule に基づいて Wave を生成・進行させるマネージャ
/// Waveの時間・敵出現・UI更新などを一括で管理
/// </summary>
public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;
    [SerializeField] int currentWaveIndex;

    [Header("生成範囲")]
    [SerializeField] Range rangeX = new Range { min = -5f, max = 5f };       // 敵の出現位置（X軸範囲）
    [SerializeField] Range rangeY = new Range { min = -1.5f, max = 3.5f };   // 敵の出現位置（Y軸範囲）

    [Header("UI")]
    [SerializeField] TextMeshProUGUI waveCountText;  // 現在のWave数を表示するテキスト
    [SerializeField] Slider waveTimerSlider;         // Waveの残り時間を示すスライダー
    Image sliderFill;                                // スライダーの塗り部分の色制御用
    Color32 yellow = new Color32(255, 220, 100, 255); // Wave中のスライダー色
    Color32 red = new Color32(255, 120, 100, 255);    // Wave待機中のスライダー色

    [Header("ステータス")]
    bool isWaveEnd;                   // true: Wave終了中 / false: Wave進行中

    [Header("Wave設定")]
    public EndlessWaveRule waveRule; // Wave生成ルール（ScriptableObjectで定義）

    private IntervalWaveData currentWaveData; // 現在進行中のWaveデータ（毎回生成される）
    List<GameObject> enemyList = new List<GameObject>();
    public List<GameObject> EnemyList { get { return enemyList; } }
    public int WaveCount { get { return currentWaveIndex; } }

    [Header("カットインの設定")]
    public CutInFlowController cutInFlowCont;

    void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // スライダーのFill部分を取得
        sliderFill = waveTimerSlider.fillRect.GetComponentInChildren<Image>();

        // Wave進行開始
        StartCoroutine(WaveTimer());

        // 自身の出現waveを保持する
        foreach(var enemy in waveRule.enemyPatterns) { enemy.enemySO.StartWave = enemy.startWave; }
    }

    void Update()
    {
        enemyList.RemoveAll(enemyList => enemyList == null);
    }

    /// <summary>
    /// Waveのタイマー・進行を管理するコルーチン
    /// </summary>
    IEnumerator WaveTimer()
    {
        while (true)
        {
            // Wave開始
            isWaveEnd = false;
            StartNextWave(); // 最初のウェーブを開始
            waveCountText.text = currentWaveIndex.ToString("F0");
            // Waveデータをルールに基づいて生成
            currentWaveData = WaveGenerator.GenerateWave(currentWaveIndex + 1, waveRule);

            float waveDuration = currentWaveData.waveDuration;

            // スライダーUI設定
            sliderFill.color = red;
            waveTimerSlider.maxValue = waveDuration;
            waveTimerSlider.value = waveDuration;
            float time = waveDuration;

            // カットインが終了するまで待機
            if(cutInFlowCont != null)
            {
                yield return new WaitUntil(() => cutInFlowCont.CutInDirectorState());
            }
            yield return new WaitForSeconds(1f);
            // 敵出現処理スタート
            StartCoroutine(SpawnEnemiesCoroutine(currentWaveData));

            // Waveタイマーの進行
            while (time > 0 && !isWaveEnd)
            {
                if(GameSystem.Instance.IsGameOver == true) yield break;
                DebugManager.Instance.WaveTime = time;
                waveTimerSlider.value = time;
                time -= Time.deltaTime;
                yield return null;
            }

            // Wave終了
            isWaveEnd = true;
           
            // 最後の敵が倒されるまで待機
            if(cutInFlowCont != null)
            {
                yield return new WaitUntil(() => FieldOnEnemiesCheck());
            }
            yield return new WaitForSeconds(1f);
        }
    }

    /// <summary>
    /// 敵の出現処理（Wave中のみ動作）
    /// 一定間隔で敵を出現させる
    /// </summary>
    IEnumerator SpawnEnemiesCoroutine(IntervalWaveData wave)
    {
        float elapsed = 0f;

        while (!isWaveEnd && elapsed < wave.waveDuration)
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
        if(GameSystem.Instance.IsGameOver == true) return;
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
    /// Waveのインターバル処理（スライダーでカウントダウン）
    /// </summary>
    bool FieldOnEnemiesCheck()
    {
        if(enemyList.Count == 0)
        {
            return true;
        }

        return false;
    }

    void EnemyStatusUP(EnemyStatus status)
    {
        // 100waveごとに大幅に上昇
        if(currentWaveIndex > 0 && currentWaveIndex % 100 == 0)
        {
            status.EnemySO.UpMaxHp = 1.5f * currentWaveIndex / 100;
        }

        // 毎waveごとに上昇
        float increase = status.EnemySO.DefaultMaxHp * (waveRule.enemyHpUp * (currentWaveIndex - status.EnemySO.StartWave));
        status.SetHpUP(increase);
    }
    
    //古西のコード
    void StartNextWave()
    {
        currentWaveIndex++;
        Debug.Log("ウェーブ " + currentWaveIndex + " 開始！");
        cutInFlowCont?.StartCutin();     // カットインの再生
    }
    //ここまで

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
}
