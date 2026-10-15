using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// EndlessWaveRule に基づいて Wave を生成・進行させるマネージャ
/// Waveの時間・敵出現・UI更新などを一括で管理
/// </summary>
public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }
    [SerializeField] int currentWaveIndex;
    [Header("スクリプト")]
    [SerializeField] EnemySpawnController eSpawnC;
    [SerializeField] StanpedeTape stanpedeTape;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI waveCountText;  // 現在のWave数を表示するテキスト
    [SerializeField] Slider waveTimerSlider;         // Waveの残り時間を示すスライダー
    Image sliderFill;                                // スライダーの塗り部分の色制御用
    // Color32 yellow = new Color32(255, 220, 100, 255); // Wave中のスライダー色
    Color32 red = new Color32(255, 120, 100, 255);   // Wave待機中のスライダー色

    [Header("Wave設定")]
    public EndlessWaveRule waveRule;                // Wave生成ルール（ScriptableObjectで定義）
    public bool IsWaveEnd { get; private set; }     // true: Wave終了中 / false: Wave進行中
    IntervalWaveData currentWaveData;               // 現在進行中のWaveデータ（毎回生成される）
    
    [Header("カットインの設定")]
    public CutInFlowController cutInFlowCont;

    [Header("ボス")]
    [SerializeField] BossSpawnController bsCtrl;
    GameObject nowBoss;
    
    public int WaveCount => currentWaveIndex;
    public Transform GetCrossTo0Enemy() => eSpawnC.CrossTo0Enemy();
    public List<GameObject> GetEnemyList() => eSpawnC.EnemyList;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        if(bsCtrl == null) Debug.LogError("BossSpawnController が設定されていません。");
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

    /// <summary>
    /// Waveのタイマー・進行を管理するコルーチン
    /// </summary>
    IEnumerator WaveTimer()
    {
        while (true)
        {
            // Wave開始
            IsWaveEnd = false;
            StartNextWave(); // 最初のウェーブを開始
            waveCountText.text = currentWaveIndex.ToString("F0");

            // Waveデータをルールに基づいて生成
            currentWaveData = WaveGenerator.GenerateWave(currentWaveIndex + 1, waveRule);

            // ボスをスポーン
            if (0 == currentWaveIndex % BossSpawnController.BossWaveCount)
            {
                nowBoss = bsCtrl.SpawnBoss();
            }


            // スタンピード時にtapeを表示する
            if (currentWaveData.isStanpede && !bsCtrl.IsBossSpawned) stanpedeTape.Play();

            float waveDuration = currentWaveData.waveDuration;

            // スライダーUI設定
            sliderFill.color = red;
            waveTimerSlider.maxValue = waveDuration;
            waveTimerSlider.value = waveDuration;
            float time = waveDuration;

            // カットインが終了するまで待機
            if (cutInFlowCont != null)
            {
                yield return new WaitUntil(() => cutInFlowCont.CutInDirectorState());
            }

            yield return new WaitForSeconds(1f);

            if (!bsCtrl.IsBossSpawned)
            {
                // 敵出現処理スタート
                eSpawnC.StartSpawnEnemy(currentWaveData);
                // Waveタイマーの進行
                while (time > 0)
                {
                    if (GameSystem.Instance.IsGameOver == true) yield break;
                    DebugManager.Instance.WaveTime = time;  // デバッグ用
                    waveTimerSlider.value = time;
                    time -= Time.deltaTime;
                    yield return null;
                }
            }
            else
            {
                yield return new WaitWhile(() => nowBoss != null);
                bsCtrl.DieBoss();       // ボスが死んだときの処理を実行
            }
            yield return new WaitUntil(() => eSpawnC.FieldOnEnemiesCheck());    // 最後の敵が倒されるまで待機
            if (currentWaveData.isStanpede && !bsCtrl.IsBossSpawned) stanpedeTape.End();                 // 終了モーションを再生

            // Wave終了
            IsWaveEnd = true;
            yield return new WaitForSeconds(1f);
        }
    }
    
    //古西のコード
    void StartNextWave()
    {
        currentWaveIndex++;
        Debug.Log("ウェーブ " + currentWaveIndex + " 開始！");
        cutInFlowCont?.StartCutin();     // カットインの再生
    }
    //ここまで
}
