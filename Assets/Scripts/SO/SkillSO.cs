using UnityEditor.EditorTools;
using UnityEngine;

public abstract class SkillSO : ScriptableObject
{
    [Header("基本情報")]
    [Tooltip("手動(false)or自動(true)"), SerializeField] bool isAuto;       // 手動（false）か自動（true）か
    [Tooltip("カテゴリ"), SerializeField] Category category;
    [Tooltip("画像"), SerializeField] Sprite sprite;
    [Tooltip("名前"), SerializeField] new string name;
    [Tooltip("効果"), SerializeField] string effect;
    [Tooltip("レアリティ"), SerializeField] Rarity rarity;
    [Tooltip("レベル"), SerializeField] int level = 0;
    [Tooltip("クールタイム(CT)"), SerializeField] float coolTime;
    bool isEndCoolTime = true;      // クールタイムが終了しているかどうか
    
    [Header("リソース情報")]
    [Tooltip("IPコストの上昇率"), SerializeField] float ipCostUpRate = 1.7f;
    [Tooltip("Skillのストック数"), SerializeField] int skillStock;
    [Tooltip("覚醒回数"), SerializeField] int awakeningCount;

    static readonly int[] BaseNeedSkillStock = { 25, 40, 65, 80, 100 };    // スキルの所持数(同種のスキル)
    static readonly int[] IPBaseCost = { 50, 75, 100, 125 };        // 知見ポイントのコスト
    public Category Category => category;
    public Sprite Icon => sprite;
    public string Name => name;
    public string Effect => effect;
    public Rarity Rarity => rarity;
    public int Level => level;
    public float CoolTime => coolTime;
    public int SkillStock => skillStock;
    public int AwakingCount => awakeningCount;
    public bool IsEndCoolTime { get { return isEndCoolTime; } set { isEndCoolTime = value; } }
    public bool IsAuto { get { return isAuto; } set { isAuto = value; } }

    /// <summary>
    /// 知見ポイントをレアリティごとに決まった値を返す
    /// </summary>
    public int InsightPointFetchCost()
    {
        if (level == 0)
        {
            return IPBaseCost[(int)rarity];
        }
        // レアリティごとに決まった値を返す
        // 0:N, 1:R, 2:SR, 3:SSR
        int cost = Mathf.FloorToInt(IPBaseCost[(int)rarity] * (ipCostUpRate * level));
        return cost;
    }


    public int GetNeedStockCount() { return BaseNeedSkillStock[awakeningCount]; }
    public bool CanAwaking() { return skillStock >= GetNeedStockCount(); }

    public void CountUpStock() => skillStock++;
    public void CountUpAwaking() => awakeningCount++;
    
    public abstract void Execute();     // スキルの実行処理
}


