using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefab;                  // 全ての敵
    Range spawnRate = new Range{ min = 0.5f, max = 3f };
    Range rangeX = new Range { min = -5f, max = 5f };
    Range rangeY = new Range { min = -1.5f, max = 3.5f };

    List<GameObject> currentEnemies = new List<GameObject>();       // 出現させる敵

    [Header("UI")]
    [SerializeField] TextMeshProUGUI waveCountText;
    [SerializeField] Slider waveTimerSlider;
    Image sliderFill;
    Color32 yellow = new Color32(255, 220, 100, 255);
    Color32 red = new Color32(255, 120, 100, 255);
    [SerializeField] TextMeshProUGUI testTime;
    [Header("ステータス")]
    [SerializeField] int waveCount;
    float waveEndTimer = 30f;
    bool isWaveEnd;
    float readyTime = 10f, readyProgressTime;


    void Start()
    {
        sliderFill = waveTimerSlider.fillRect.GetComponent<Image>();
        currentEnemies.Add(enemyPrefab[0]);     // Normalエネミーだけは最初にいれておく

        StartCoroutine(WaveTimer());
        StartCoroutine(SpawnEnemy());
    }

    void Update()
    {
        
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

            // 出現位置を決める
            Vector3 spawnPos = new Vector3(Random.Range(rangeX.min, rangeX.max), Random.Range(rangeY.min, rangeY.max), 0);
            GameObject selectEnemy = new GameObject();

            // 確率が低い敵から抽選を行う
            for(int ii = currentEnemies.Count - 1; ii >= 0; ii--)
            {
                selectEnemy = currentEnemies[ii];
                bool isSelect = EnemyLottery(selectEnemy.GetComponent<EnemyStatus>().EnemySO);       // 敵個人の抽選を行う
                if(isSelect == true) break;     // 抽選が終わったらブレイク
            }

            // 敵を生成
            Instantiate(selectEnemy, spawnPos, Quaternion.identity);
        }
    }

    /// <summary>
    /// その敵を出現させるか抽選を行う
    /// </summary>
    public bool EnemyLottery(EnemySO enemySO)
    {
        int r_val = Random.Range(0, 100);
        if(r_val < enemySO.Probability)     // 個々に設定した確率よりランダムな値が小さければ
        {
            return true;
        }
        
        return false;
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
            waveCount++;
            waveCountText.text = waveCount.ToString("F0");

            sliderFill.color = yellow;      // 色を設定
            waveTimerSlider.maxValue = waveEndTimer;
            float time = waveEndTimer;
            while (time > 0)
            {
                testTime.text = "" + time;
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
        testTime.text = "" + readyProgressTime;

        readyProgressTime += Time.deltaTime;
        if(readyProgressTime > readyTime)
        {
            readyProgressTime = 0;
            return true;
        }

        return false;
    }
}
