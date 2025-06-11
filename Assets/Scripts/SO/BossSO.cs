using UnityEngine;

[CreateAssetMenu(fileName = "BossSO", menuName = "SO/BossSO")]
public class BossSO : ScriptableObject
{
    [SerializeField,Header("ボスのBasicUpgradeData")] BasicUpgradeData b_UpDataSO;
    [SerializeField,Header("ボスのプレハブ")] GameObject boss_Prefab;
    [SerializeField,Header("爆破のプレハブ")] GameObject explosion_Prefab;
    [SerializeField,Header("スクラッププレハブ")] GameObject scrap_Prefab;
    [SerializeField,Header("カウントのプレハブ")] GameObject countzero_Prefab;
    [SerializeField,Header("ボスの名前")] string bossName;
    [SerializeField,Header("ボスのアイコン")] Sprite bossIcon;
    [SerializeField,Header("ボスのMaxHP")] float defaultMaxHp;
    [SerializeField,Header("ボスのUpMaxHP")] float upMaxHp;
    [SerializeField,Header("カウントダウン")] float countDown;
    [SerializeField,Header("ドロップスクラップ")] int dropScrapAmount;
    int startWave;      // 自身の出現waveを保持

    public int StartWave { get { return startWave; } set { startWave = value; } }
    public GameObject Enemy_Prefab => boss_Prefab;
    public GameObject Explosion_Prefab => explosion_Prefab;
    public GameObject Scrap_Prefab => scrap_Prefab;
    public GameObject Countzero_Prefab => countzero_Prefab;
    public string EnemyName => bossName;
    public Sprite EnemyIcon => bossIcon;
    public float DefaultMaxHp => defaultMaxHp;
    public float CountDown => countDown;
    public float UpMaxHp { get { return upMaxHp; } set { upMaxHp = defaultMaxHp * value; } }
    public void ResetMaxHp() { upMaxHp = defaultMaxHp; }

    /// <summary>
    /// ボスがやられたらスクラップが落ちる
    /// </summary>
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
public enum BossType
{
    boss_Ver1_0,
    boss_Ver1_5,
    boss_Ver2_0,
}
