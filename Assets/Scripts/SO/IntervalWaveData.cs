using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave/Interval Wave Data")]
public class IntervalWaveData : ScriptableObject
{
    public float waveDuration;
    public float readyTime;
    public float spawnInterval;
    public bool isStanpede;

    [System.Serializable]
    public class EnemySpawnOption
    {
        public EnemySO enemySO;
        public float weight;        // 出現率（重み）
    }

    public EnemySpawnOption[] spawnOptions;
}
