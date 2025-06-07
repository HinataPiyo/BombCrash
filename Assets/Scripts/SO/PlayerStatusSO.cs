using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "SO/PlayerStatus")]
public class PlayerStatusSO : ScriptableObject
{
    [SerializeField] SceneName nextScene;
    [SerializeField] bool isReleaseOtomo;
    [SerializeField] int maxWaveReleaseOtomo = 10;

    // 基礎値の設定(デフォルトの値-固定値)　基礎研究とは別
    public static readonly float MoveSpeed = 6f;          // 移動速度
    public static readonly int defaultHaveBomb = 3;       // 爆弾最大所持数
    public static readonly float createBombTime = 2f;     // 爆弾の制作時間
    public static readonly float criticalDamage = 1f;     // クリティカルダメージ
    public static readonly float criticalChance = 0;      // クリティカル率

    [Header("スクラップの所持数"), SerializeField] int scrapHaveAmount;
    [Header("知見ポイントの所持数"), SerializeField] int insightPointHaveAmount;
    [Header("最大到達WAVE数"), SerializeField] int arrivalWave;

    [Header("装備中のアタッチメント"), SerializeField] List<AttachmentDataSO> attachments;        // 装備中のアタッチメント
    public List<AttachmentDataSO> EquipAttachments { get { return attachments; } set { attachments = value; } }
    public SceneName SceneName { set { nextScene = value; } }
    public bool IsReleaseOtomo { get { return isReleaseOtomo; } }

    /// <summary>
    /// オトモが解放できるかチェックする
    /// </summary>
    public void CheckIsReleaseOtomo()
    {
        isReleaseOtomo = arrivalWave > maxWaveReleaseOtomo;
    }

    // 爆弾所持数
    public int MaxHaveBomb
    {
        get
        {
            return defaultHaveBomb + (int)CheckAttachmentStatusName(StatusName.BombStockAmountUp);
        }
    }
    
    // 爆弾生成時間
    public float CreateBombTime
    {
        get
        {
            return createBombTime + CheckAttachmentStatusName(StatusName.BombCreateSpeedUp);
        }
    }

    // クリティカルダメージ
    public float CriticalDamage
    {
        get
        {
            return criticalDamage + CheckAttachmentStatusName(StatusName.CriticalDamageUp);
        }
    }

    // クリティカル率
    public float CriticalChance
    {
        get
        {
            return criticalChance + CheckAttachmentStatusName(StatusName.CriticalChanceUp);
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
        // 前回の到達地点と比較して大きい大きければ上書き
        set { if (arrivalWave < value) arrivalWave = value; }
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

    /// <summary>
    /// 装備されたアタッチメントがアップグレード内容と一致しているか確認
    /// 一致していたら合算して返す
    /// </summary>
    /// <param name="upgradName">アップグレードしたいステータスネーム</param>
    public float CheckAttachmentStatusName(StatusName upgradName)
    {
        float total = 0;
        foreach (AttachmentDataSO data in attachments)
        {
            if (data == null) continue;
            float _value = data.GetUpgradeValue(upgradName);
            if (_value != 0) total += _value;
            else continue;
        }

        return total;
    }

    // ステータスごとに対応するStatusNameを用意
    public static StatusName[] bombStatusNames = new StatusName[]
    {
        StatusName.BombAttackDamageUp,
        StatusName.CriticalDamageUp,
        StatusName.CriticalChanceUp,
        StatusName.BombStockAmountUp,
        StatusName.BombCreateSpeedUp,
        StatusName.ExplosionRadiusUp
    };

     /// <summary>
    /// StatusNameから日本語に変換した文字列を返す
    /// </summary>
    public static string StatusNameToName(StatusName statusName)
    {
        switch (statusName)
        {
            case StatusName.BombAttackDamageUp:
                return "冷却ダメージ";
            case StatusName.BombCreateSpeedUp:
                return "爆弾生成速度";
            case StatusName.BombStockAmountUp:
                return "最大爆弾所持数";
            case StatusName.CriticalDamageUp:
                return "クリティカルダメージ";
            case StatusName.CriticalChanceUp:
                return "クリティカル率";
            case StatusName.ExplosionRadiusUp:
                return "爆発範囲";
        }

        return "";
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