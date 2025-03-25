using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "SO/EnemySO")]
public class EnemySO : ScriptableObject
{
    [SerializeField] GameObject enemy_Prefab;
    [SerializeField] string enemyName;
    [SerializeField] float maxHp;
    [SerializeField] float countDown;
    [SerializeField] int dropScrapAmount;
    [Header("出現確率")]
    [SerializeField] float initialSpawnProbability;
    [SerializeField] float currentSpawnProbability;

    public GameObject Prefab { get { return enemy_Prefab; } }
    public string EnemyName => enemyName;
    public float MaxHp => maxHp;
    public float CountDown => countDown;
    public int DropScrapAmount => dropScrapAmount;
    public float InitialProbability => initialSpawnProbability;
    public float CurrentSpawnProbability { get { return currentSpawnProbability; } set { currentSpawnProbability = value; } }
}