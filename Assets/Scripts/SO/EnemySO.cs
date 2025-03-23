using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "SO/EnemySO")]
public class EnemySO : ScriptableObject {
    
    [SerializeField] string enemyName;
    [SerializeField] float maxHp;
    [SerializeField] float countDown;
    [SerializeField] int dropScrapAmount;
    [Header("出現確率"), SerializeField]
    int probability;

    public string EnemyName => enemyName;
    public float MaxHp => maxHp;
    public float CountDown => countDown;
    public int DropScrapAmount => dropScrapAmount;
    public int Probability => probability;
}