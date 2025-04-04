using UnityEngine;

[CreateAssetMenu(menuName = "Wave/Interval Wave Data")]
public class IntervalWaveData : ScriptableObject
{
    public float waveDuration = 15f;
    public float spawnInterval = 2.5f;

    [System.Serializable]
    public class EnemySpawnOption
    {
        public EnemySO enemySO;
        public float weight = 1.0f; // 出現率（重み）
    }

    public EnemySpawnOption[] spawnOptions;
}
