using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "SO/PlayerStatus")]
public class PlayerStatusSO : ScriptableObject
{
    [SerializeField] SceneName nextScene;
    [Header("移動速度")] public const float MoveSpeed = 6f;
    [Header("爆弾の所持数"), SerializeField] int maxHaveBomb;
    [Header("爆弾の制作時間"), SerializeField] float createBombTime;
    [Header("スクラップの所持数"), SerializeField] int scrapHaveAmount;
    [Header("WAVEポイントの所持数"), SerializeField] int insightPointHaveAmount;
    [Header("最大到達WAVE数"), SerializeField] int arrivalWave;

    [Header("研究ツリーの解放数(RC)")]
    [Header("爆弾"), SerializeField] BombResearchCompleteds bomb_RC;
    [Header("支援"), SerializeField] SupportResearchCompleteds support_RC;
    public BombResearchCompleteds Bomb_RC { get { return bomb_RC; } }
    public SupportResearchCompleteds Support_RC { get { return support_RC; } }

    public SceneName SceneName { set { nextScene = value; } }
    public int MaxHaveBomb { get{ return (maxHaveBomb - 1) + bomb_RC.BombStockAmountUp; } }
    public float CreateBombTime { get { return createBombTime - createBombTime * bomb_RC.BombCreateSpeedUp; } }
    public int ScrapHaveAmount { get { return scrapHaveAmount; } set { scrapHaveAmount += value; } }
    public int InsightPointHaveAmount { get { return insightPointHaveAmount; } set { insightPointHaveAmount += value; } }
    public int ArrivalWave { get { return arrivalWave; } set { arrivalWave = value; } }

    [System.Serializable]
    public class BombResearchCompleteds
    {
        [Header("爆発範囲の解放数"), SerializeField] int explosionRadiusUp;
        [Header("爆弾生成速度の解放数"), SerializeField] int bombCreateSpeedUp;
        [Header("爆弾ストック数の解放数"), SerializeField] int bombStockAmountUp;
        [Header("攻撃力の解放数"), SerializeField] int attackDamageUp;
        [Header("投擲数の解放数"), SerializeField]  int throwAmountUp;

        public float ExplosionRadiusUp { get { return explosionRadiusUp * ResearchData.explosionRadius; } set { explosionRadiusUp += (int)value; } }
        public float BombCreateSpeedUp { get { return bombCreateSpeedUp * ResearchData.bombCreateSpeed; } set { bombCreateSpeedUp += (int)value; } }
        public int BombStockAmountUp { get { return bombStockAmountUp * ResearchData.bombStockAmountUp; } set { bombStockAmountUp += (int)value; } }
        public float AttackDamageUp { get { return attackDamageUp * ResearchData.attackDamageUp; } set { attackDamageUp += (int)value; } }
        public int ThrowAmountUp { get { return throwAmountUp * ResearchData.throwAmount; } set { throwAmountUp += (int)value; }}
    }

    [System.Serializable]
    public class SupportResearchCompleteds
    {
        [Header("スクラップのドロップ量"), SerializeField] int dropScrapAmountUp;
        [Header("ウェーブポイントの貰える量"), SerializeField] int wavePointAmountUp;

        public float DropScrapAmountUp { get { return dropScrapAmountUp * ResearchData.dropScrapAmountUp; } set { dropScrapAmountUp += (int)value; } }
        public float WavePointAmountUp { get { return wavePointAmountUp * ResearchData.insightPointAmountUp; } set { wavePointAmountUp += (int)value; } }
    }

    public string NextSceneName()
    {
        switch(nextScene)
        {
            case SceneName.GameScene:
                return "GameScene";
            case SceneName.HomeScene:
                return "HomeScene";
        }

        return null;
    }
}

public enum SceneName
{
    GameScene,
    HomeScene,
}