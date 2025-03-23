using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "SO/PlayerStatus")]
public class PlayerStatusSO : ScriptableObject
{
    [Header("爆弾の所持数"), SerializeField] int maxHaveBomb;
    [Header("爆弾の制作時間"), SerializeField] float createBombTime;
    [Header("スクラップの所持数"), SerializeField] int scrapHaveAmount;
    [Header("WAVEポイントの所持数"), SerializeField] int wavePointHaveAmount;
    [Header("最大到達WAVE数"), SerializeField] int arrivalWave;

    public int MaxHaveBomb { get{ return maxHaveBomb; } set{ maxHaveBomb += value; } }
    public float CreateBombTime { get { return createBombTime; } }
    public int ScrapHaveAmount { get { return scrapHaveAmount; } set { scrapHaveAmount += value; } }
    public int WavePointHaveAmount { get { return wavePointHaveAmount; } set { wavePointHaveAmount = value; } }
    public int ArrivalWave { get { return arrivalWave; } set { arrivalWave = value; } }
}