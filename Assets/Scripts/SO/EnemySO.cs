using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "SO/EnemySO")]
public class EnemySO : ScriptableObject
{
    [SerializeField] PlayerStatusSO player;
    [SerializeField] GameObject enemy_Prefab;
    [SerializeField] float maxHp;
    [SerializeField] float countDown;
    [SerializeField] int dropScrapAmount;

    public GameObject Prefab => enemy_Prefab;
    public float MaxHp => maxHp;
    public float CountDown => countDown;
    public int DropScrapAmount { get { return (int)(dropScrapAmount + dropScrapAmount * player.Support_RC.DropScrapAmountUp); } }
}

public enum EnemyType
{
    Normal_Ver1_0,
    Normal_Ver1_5,
    Normal_Ver2_0,
    // 今後追加予定
}