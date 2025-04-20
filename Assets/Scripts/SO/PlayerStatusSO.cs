using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "SO/PlayerStatus")]
public class PlayerStatusSO : ScriptableObject
{
    [SerializeField] BasicUpgradeData b_UpDataSO;
    [SerializeField] SceneName nextScene;

    // 基礎値の設定(デフォルトの値-固定値)　基礎研究とは別
    public const float MoveSpeed = 6f;          // 移動速度
    public const int defaultHaveBomb = 3;       // 爆弾最大所持数
    public const float createBombTime = 2f;     // 爆弾の制作時間
    public const float criticalDamage = 1f;     // クリティカルダメージ
    public const float criticalChance = 0;      // クリティカル率


    [Header("スクラップの所持数"), SerializeField] int scrapHaveAmount;
    [Header("知見ポイントの所持数"), SerializeField] int insightPointHaveAmount;
    [Header("最大到達WAVE数"), SerializeField] int arrivalWave;

    [Header("研究ツリーの解放数(RC)")]
    [Header("爆弾"), SerializeField] BombResearchCompleteds bomb_RC;
    [Header("支援"), SerializeField] SupportResearchCompleteds support_RC;
    public BombResearchCompleteds Bomb_RC { get { return bomb_RC; } }
    public SupportResearchCompleteds Support_RC { get { return support_RC; } }

    public SceneName SceneName { set { nextScene = value; } }
    public int MaxHaveBomb      // 爆弾所持数
    {
        get
        {
            int basic = (int)b_UpDataSO.GetPlayerData(StatusName.BombStockAmountUp).increaseValue;
            return bomb_RC.BombStockAmountUp + basic + (defaultHaveBomb - 1);
        }
    }
    public float CreateBombTime     // 爆弾生成時間
    {
        get
        {
            float basic = b_UpDataSO.GetPlayerData(StatusName.BombCreateSpeedUp).increaseValue;
            return createBombTime * (1 - (bomb_RC.BombCreateSpeedUp + basic));
        }
    }
    public float CriticalDamage     // クリティカルダメージ
    {
        get
        {
            float basic = b_UpDataSO.GetPlayerData(StatusName.CriticalDamageUp).increaseValue;
            return criticalDamage + bomb_RC.CriticalDamageUp + basic;
        }
    }
    public float CriticalChance     // クリティカル率
    {
        get
        {
            float basic = b_UpDataSO.GetPlayerData(StatusName.CriticalChanceUp).increaseValue;
            return criticalChance + bomb_RC.CriticalChanceUp + basic;
        }
    }
    // スクラップの所持数
    public int ScrapHaveAmount { get { return scrapHaveAmount; } set { scrapHaveAmount += value; } }
    // 知見ポイントの所持数
    public int InsightPointHaveAmount { get { return insightPointHaveAmount; } set { insightPointHaveAmount += value; } }
    // WAVE最高到達地点
    public int ArrivalWave
    {
        get { return arrivalWave; }
        set { if (arrivalWave < value) arrivalWave = value; }
    }  // 前回の到達地点と比較して大きい大きければ上書き

    [System.Serializable]
    public class BombResearchCompleteds
    {
        [Header("攻撃力の解放数"), SerializeField] int attackDamageUp;
        [Header("クリティカルダメージの解放数"), SerializeField] int criticalDamageUp;
        [Header("クリティカル率の解放数"), SerializeField] int criticalChanceUp;
        [Header("爆発範囲の解放数"), SerializeField] int explosionRadiusUp;
        [Header("爆弾生成速度の解放数"), SerializeField] int bombCreateSpeedUp;
        [Header("爆弾ストック数の解放数"), SerializeField] int bombStockAmountUp;
        [Header("投擲数の解放数"), SerializeField] int throwAmountUp;

        public float ExplosionRadiusUp { get { return explosionRadiusUp * ResearchData.explosionRadius; } set { explosionRadiusUp += (int)value; } }
        public float BombCreateSpeedUp { get { return bombCreateSpeedUp * ResearchData.bombCreateSpeed; } set { bombCreateSpeedUp += (int)value; } }
        public int BombStockAmountUp { get { return bombStockAmountUp * ResearchData.bombStockUp; } set { bombStockAmountUp += (int)value; } }
        public float AttackDamageUp { get { return attackDamageUp * ResearchData.attackDamageUp; } set { attackDamageUp += (int)value; } }
        public float CriticalDamageUp { get { return criticalDamageUp * ResearchData.criticalDamageUp; } }
        public float CriticalChanceUp { get { return criticalChanceUp * ResearchData.criticalChanceUp; } }
        public int ThrowAmountUp { get { return throwAmountUp * ResearchData.throwAmount; } set { throwAmountUp += (int)value; } }
    }

    [System.Serializable]
    public class SupportResearchCompleteds
    {
        [Header("スクラップのボーナス"), SerializeField] int scrapBonusUp;
        [Header("知見ポイントのボーナス"), SerializeField] int insightBonusUp;

        public float ScrapBonusUp { get { return scrapBonusUp * ResearchData.scrapBonusUp; } set { scrapBonusUp += (int)value; } }
        public float InsightBonusUp { get { return insightBonusUp * ResearchData.insightPointUp; } set { insightBonusUp += (int)value; } }
    }

    public string NextSceneName()
    {
        switch (nextScene)
        {
            case SceneName.GameScene:
                return "GameScene";
            case SceneName.HomeScene:
                return "HomeScene";
        }

        return null;
    }
}

public enum Rarity { N, R, SR, SSR }

public enum SceneName
{
    GameScene,
    HomeScene,
}

public enum StatusName
{
    BombAttackDamageUp,
    CriticalDamageUp,
    CriticalChanceUp,
    BombStockAmountUp,
    BombCreateSpeedUp,
    ExplosionRadiusUp,
    ThrowAmountUp,

    DropScrapUp,
    GetInsightPointUp,
}