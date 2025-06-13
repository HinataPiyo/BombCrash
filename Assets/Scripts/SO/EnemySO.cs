using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "SO/EnemySO")]
public class EnemySO : ScriptableObject
{
    [SerializeField, Tooltip("ボスのプレハブ")] GameObject enemy_Prefab;
    [SerializeField, Tooltip("爆破のプレハブ")] GameObject explosion_Prefab;
    [SerializeField, Tooltip("スクラッププレハブ")] GameObject scrap_Prefab;
    [SerializeField, Tooltip("カウントのプレハブ")] GameObject countzero_Prefab;
    [SerializeField, Tooltip("ボスの名前")] string enemyName;
    [SerializeField, Tooltip("ボスのアイコン")] Sprite enemyIcon;
    [SerializeField, Tooltip("ボスのMaxHP")] float defaultMaxHp;
    [SerializeField, Tooltip("ボスのUpMaxHP")] float upMaxHp;
    [SerializeField, Tooltip("カウントダウン")] float countDown;
    [SerializeField, Tooltip("ドロップスクラップ")] int dropScrapAmount;
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
    public int DropScrapAmount { get { return dropScrapAmount; } }
}