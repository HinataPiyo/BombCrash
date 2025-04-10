using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// EndlessWaveRule に基づいて、指定された Wave 番号の出現データ（IntervalWaveData）を生成するクラス
/// 使い捨ての ScriptableObject として生成され、出現時間・出現間隔・敵出現比率を含む
/// </summary>
public static class WaveGenerator
{
    /// <summary>
    /// 指定された Wave 番号に応じて、自動的に IntervalWaveData を生成する
    /// </summary>
    /// <param name="waveNumber">生成する Wave の番号（1〜）</param>
    /// <param name="rule">使用する EndlessWaveRule の定義</param>
    /// <returns>Waveに使用できるIntervalWaveData</returns>
    public static IntervalWaveData GenerateWave(int waveNumber, EndlessWaveRule rule)
    {
        // 空の WaveData を生成
        IntervalWaveData wave = ScriptableObject.CreateInstance<IntervalWaveData>();

        float spawn;
        float waveDur;
        // スタンピードを起こす
        if(0 == (waveNumber - 1) % rule.stampedeWaveInterval)
        {
            waveDur = rule.baseWaveDuration * 1.5f;
            float val = rule.baseSpawnInterval - rule.intervalDecreasePerWave * waveNumber;
            spawn = val < rule.stampedeSpawnInterval ? val * 1.5f : rule.stampedeSpawnInterval;
            Debug.Log("スタンピード発生！！ : " + spawn);
        }
        else
        {
            // Wave 時間を減衰させて設定（最低10秒まで）
            waveDur = rule.baseWaveDuration;
            // 出現間隔も減衰（最低0.3秒まで）
            spawn = Mathf.Max(0.3f, rule.baseSpawnInterval - rule.intervalDecreasePerWave * waveNumber);
        }
        wave.waveDuration = waveDur;
        wave.spawnInterval = spawn;

        // 出現する敵のリストを作成
        List<IntervalWaveData.EnemySpawnOption> options = new();

        foreach (var pattern in rule.enemyPatterns)
        {
            // 指定Waveより前の敵は出現させない
            if (waveNumber < pattern.startWave) continue;

            // waveNumberがstartWave〜peakWaveの間にあるとき、0〜1の範囲で割合を計算
            float t = Mathf.InverseLerp(pattern.startWave, pattern.peakWave, waveNumber);

            // 出現比率（weight）を線形補間で求める
            float weight = Mathf.Lerp(0f, pattern.peakWeight, Mathf.Clamp01(t));

            // 出現率が0より大きい場合だけリストに追加
            if (weight > 0f)
            {
                options.Add(new IntervalWaveData.EnemySpawnOption
                {
                    enemySO = pattern.enemySO,
                    weight = weight
                });
            }
        }

        wave.spawnOptions = options.ToArray(); // 最終的な出現候補リストに格納
        return wave;
    }
}
