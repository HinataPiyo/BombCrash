using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "SO/PlayerStatus")]
public class PlayerStatusSO : ScriptableObject
{
    [Header("移動速度")] public const float MoveSpeed = 5f;
    [Header("爆弾の所持数"), SerializeField] int maxHaveBomb;
    [Header("爆弾の制作時間"), SerializeField] float createBombTime;
    [Header("スクラップの所持数"), SerializeField] int scrapHaveAmount;
    [Header("WAVEポイントの所持数"), SerializeField] int wavePointHaveAmount;
    [Header("最大到達WAVE数"), SerializeField] int arrivalWave;

    [Header("研究ツリーの解放数(RC)")]
    [Header("連射型"), SerializeField] RapidFireResearchCompleteds rapidFire_RC;
    public RapidFireResearchCompleteds RapidFire_RC { get { return rapidFire_RC; } }

    public int MaxHaveBomb { get{ return (maxHaveBomb - 1) + rapidFire_RC.BombStockAmountUp; } }
    public float CreateBombTime { get { return createBombTime - createBombTime * rapidFire_RC.BombCreateSpeedUp; } }
    public int ScrapHaveAmount { get { return scrapHaveAmount; } set { scrapHaveAmount += value; } }
    public int WavePointHaveAmount { get { return wavePointHaveAmount; } set { wavePointHaveAmount = value; } }
    public int ArrivalWave { get { return arrivalWave; } set { arrivalWave = value; } }

    [System.Serializable]
    public class RapidFireResearchCompleteds
    {
        [Header("爆発範囲の解放数"), SerializeField] int explosionRadiusUp;
        [Header("爆弾生成速度の解放数"), SerializeField] int bombCreateSpeedUp;
        [Header("爆弾ストック数の解放数"), SerializeField] int bombStockAmountUp;
        [Header("攻撃力の解放数"), SerializeField] int attackDamageUp;
        [Header("投擲数の解放数"), SerializeField]  int throwAmountUp;

        public float ExplosionRadiusUp { get { return explosionRadiusUp * ResearchData.explosionRadius; } }
        public float BombCreateSpeedUp { get { return bombCreateSpeedUp * ResearchData.bombCreateSpeed; } }
        public int BombStockAmountUp { get { return bombStockAmountUp * ResearchData.bombStockAmountUp; } }
        public float AttackDamageUp { get { return attackDamageUp * ResearchData.attackDamageUp; } }
        public int ThrowAmountUp { get { return throwAmountUp * ResearchData.throwAmount; } }
    }
}