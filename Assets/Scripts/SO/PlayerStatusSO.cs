using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "SO/PlayerStatus")]
public class PlayerStatusSO : ScriptableObject
{
    [Header("爆弾の所持数"), SerializeField] int maxHaveBomb;
    [Header("爆弾の制作時間"), SerializeField] float createBombTime;
    [Header("スクラップの数"), SerializeField] int scrapHaveAmount;

    public int MaxHaveBomb { get{ return maxHaveBomb; } set{ maxHaveBomb += value; } }
    public int ScrapHaveAmount { get { return scrapHaveAmount; } set { scrapHaveAmount += value; } }
    public float CreateBombTime { get { return createBombTime; } }
}