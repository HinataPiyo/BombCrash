using UnityEngine;

[CreateAssetMenu(fileName = "AttachmentDataSO", menuName = "Attachment/AttachmentDataSO")]
public class AttachmentDataSO : ScriptableObject
{
    [SerializeField] new string name;           // 名前
    [SerializeField] string discription;        // 説明
    [SerializeField] Sprite icon;             // 画像
    [SerializeField] StatusName statusName;     // 強化したいステータスネーム
    [SerializeField] float upgradeValue;       // 強化率
    [SerializeField] int resourceValue;

    [Header("購入済みか否か"), SerializeField] bool isDeveloped;

    public StatusName StatusName => statusName;
    public Sprite Icon => icon;
    public string Name => name;
    public string Discription => discription;
    public StatusName UseSutatusName => statusName;
    public float UpgreadeValue => upgradeValue;
    public int ResourceValue => resourceValue;
    public bool IsDeveloped { get { return isDeveloped; } set { isDeveloped = value; } }
    public bool IsEquiped { get; set; }

    /// <summary>
    /// 装備一覧で使用
    /// </summary>
    public float GetUpgradeValue(StatusName upgradName)
    {
        // 強化内容が同じだったら
        if (statusName == upgradName) return upgradeValue;
        return 0;
    }

    /// <summary>
    /// StatusNameから日本語に変換した文字列を返す
    /// </summary>
    public string StatusNameToName(StatusName statusName)
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