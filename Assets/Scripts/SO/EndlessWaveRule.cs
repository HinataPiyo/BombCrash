using UnityEngine;

/// <summary>
/// 無限Wave用のルール定義をScriptableObjectとして管理するクラス
/// 各Waveの長さ、敵の出現間隔、敵の種類ごとのスケーリング設定を含む
/// </summary>
[CreateAssetMenu(menuName = "Wave/Endless Wave Rule")]
public class EndlessWaveRule : ScriptableObject
{
    [Header("通常Wave進行設定")]
    public float baseWaveDuration = 15f;        // 各Wave進行時間の基準となる長さ（秒）
    public float baseReadyTime = 5f;            // wave終了時の次のwaveに行くまでの待機時間
    public float baseSpawnInterval = 2f;        // 敵の出現間隔（秒）
    // 例：0.03fならWave10で0.3秒早く出現
    public float intervalDecreasePerWave = 0.03f;       // Waveが1進むごとに敵の出現間隔をどれだけ短くするか


    /// Waveが1進むごとにWaveの長さをどれだけ短くするか
    /// 例：0.1fならWave10では1秒短くなる（最低時間制限は生成時に別で設定する）
    // public float durationDecreasePerWave = 0.1f;


    [Header("スタンピード設定")]
    public int stampedeWaveInterval = 10;       // 敵が大量に出るイベントの間隔
    public float stampedeSpawnInterval = 0.5f;  // スタンピード時, 敵のスポーン間隔
    public float stampedeWaveDuration = 10f;    // スタンピード時, Wave進行時間
    public float stampedeReadyTime = 8f;        // スタンピード時, wave終了時の次のwaveに行くまでの待機時間


    [Header("敵のHP上昇率(%)")]
    // 基礎ステータスをもとに上昇する
    public float enemyHpUp = 0.06f;            // 1waveごとに敵のステータスを上昇する係数

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