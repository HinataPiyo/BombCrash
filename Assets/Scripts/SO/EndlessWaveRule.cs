using UnityEngine;

/// <summary>
/// 無限Wave用のルール定義をScriptableObjectとして管理するクラス
/// 各Waveの長さ、敵の出現間隔、敵の種類ごとのスケーリング設定を含む
/// </summary>
[CreateAssetMenu(menuName = "Wave/Endless Wave Rule")]
public class EndlessWaveRule : ScriptableObject
{
    /// <summary>
    /// 各Waveの基準となる長さ（秒）
    /// Wave数に応じて短くなる（durationDecreasePerWaveで調整）
    /// </summary>
    public float baseWaveDuration = 15f;

    /// <summary>
    /// 敵の出現間隔（秒）
    /// Waveが進むごとに短くなり、難易度が上がる
    /// </summary>
    public float baseSpawnInterval = 2f;

    /// <summary>
    /// Waveが1進むごとにWaveの長さをどれだけ短くするか
    /// 例：0.1fならWave10では1秒短くなる（最低時間制限は生成時に別で設定する）
    /// </summary>
    // public float durationDecreasePerWave = 0.1f;

    /// <summary>
    /// Waveが1進むごとに敵の出現間隔をどれだけ短くするか
    /// 例：0.03fならWave10で0.3秒早く出現
    /// </summary>
    public float intervalDecreasePerWave = 0.03f;

    /// <summary>
    /// 敵が大量に出るイベントの間隔
    /// 例: 10waveごとに大量発生する
    /// </summary>
    public int stampedeWaveInterval = 10;

    /// <summary>
    /// スタンピード時の敵のスポーン間隔
    /// </summary>
    public float stampedeSpawnInterval = 0.5f;

    /// <summary>
    /// 1waveごとに敵のステータスを上昇する係数
    /// </summary>
    public float enemyHpUp = 0.075f;

    /// <summary>
    /// 出現する敵の種類ごとのスケーリング設定リスト
    /// 各敵が「何Wave目から登場するか」「どのWaveでピークに達するか」「最大出現比率」などを記述
    /// </summary>
    public EnemyScalingPattern[] enemyPatterns;
}


[System.Serializable]
public class EnemyScalingPattern
{
    public EnemySO enemySO;
    public int startWave = 1;
    public int peakWave = 50;      // いつ最もよく出現するか
    public float peakWeight = 5f;  // 最大出現比重
}