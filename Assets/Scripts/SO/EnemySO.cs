using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "SO/EnemySO")]
public class EnemySO : ScriptableObject
{
    [SerializeField] PlayerStatusSO player;
    [SerializeField] GameObject enemy_Prefab;
    [SerializeField] GameObject explosion_Prefab;
    [SerializeField] GameObject scrap_Prefab;
    [SerializeField] GameObject countzero_Prefab;
    [SerializeField] float defaultMaxHp;
    [SerializeField] float upMaxHp;
    [SerializeField] float countDown;
    [SerializeField] int dropScrapAmount;

    public GameObject Enemy_Prefab => enemy_Prefab;
    public GameObject Explosion_Prefab => explosion_Prefab;
    public GameObject Scrap_Prefab => scrap_Prefab;
    public GameObject Countzero_Prefab => countzero_Prefab;
    public float DefaultMaxHp => defaultMaxHp;
    public float CountDown => countDown;
    public float UpMaxHp { get { return upMaxHp; } set { upMaxHp = defaultMaxHp * value; } }
    public void ResetMaxHp() { upMaxHp = defaultMaxHp; }
    public int DropScrapAmount { get { return (int)(dropScrapAmount + dropScrapAmount * player.Support_RC.DropScrapAmountUp); } }
}

public enum EnemyType
{
    Normal_Ver1_0,
    Normal_Ver1_5,
    Normal_Ver2_0,
    // 今後追加予定
}