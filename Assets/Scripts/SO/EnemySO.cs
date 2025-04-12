using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "SO/EnemySO")]
public class EnemySO : ScriptableObject
{
    [SerializeField] BasicUpgradeData b_UpDataSO;
    [SerializeField] GameObject enemy_Prefab;
    [SerializeField] GameObject explosion_Prefab;
    [SerializeField] GameObject scrap_Prefab;
    [SerializeField] GameObject countzero_Prefab;
    [SerializeField] string enemyName;
    [SerializeField] Sprite enemyIcon;
    [SerializeField] float defaultMaxHp;
    [SerializeField] float upMaxHp;
    [SerializeField] float countDown;
    [SerializeField] int dropScrapAmount;
    int startWave;      // 自身の出現waveを保持

    public int StartWave { get { return startWave; } set { startWave = value; } }
    public GameObject Enemy_Prefab => enemy_Prefab;
    public GameObject Explosion_Prefab => explosion_Prefab;
    public GameObject Scrap_Prefab => scrap_Prefab;
    public GameObject Countzero_Prefab => countzero_Prefab;
    public string EnemyName => enemyName;
    public Sprite EnemyIcon => enemyIcon;
    public float DefaultMaxHp => defaultMaxHp;
    public float CountDown => countDown;
    public float UpMaxHp { get { return upMaxHp; } set { upMaxHp = defaultMaxHp * value; } }
    public void ResetMaxHp() { upMaxHp = defaultMaxHp; }
    public int DropScrapAmount 
    {
        get
        {
            float increaseValue = b_UpDataSO.GetSupportData(StatusName.DropScrapUp).increaseValue;
            float x = dropScrapAmount + dropScrapAmount * increaseValue;
            return (int)x;
        }
    }
}

public enum EnemyType
{
    Normal_Ver1_0,
    Normal_Ver1_5,
    Normal_Ver2_0,
    // 今後追加予定
}