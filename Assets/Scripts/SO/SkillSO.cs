using UnityEngine;

[CreateAssetMenu(fileName = "SkillSO", menuName = "SkillSO")]
public class SkillSO : ScriptableObject
{
    [Header("スキル処理"), SerializeField] SkillLogicBase skillLogicBase;
    [Header("手動(false)or自動(true)"), SerializeField] bool isAuto;       // 手動（false）か自動（true）か
    [Header("カテゴリ"), SerializeField] Category category;
    [Header("画像"), SerializeField] Sprite sprite;
    [Header("名前"), SerializeField] new string name;
    [Header("効果"), SerializeField] string effect;
    [Header("レアリティ"), SerializeField] Rarity rarity;
    [Header("レベル"), SerializeField] int level = 0;
    [Header("クールタイム(CT)"), SerializeField] float coolTime;
    bool isEndCoolTime = true; // クールタイムが終了しているかどうか
    [Header("現在の熟練度"), SerializeField] int currentProficiency;
    [Header("次の最大熟練度"), SerializeField] int maxProficiency;
    [Header("熟練度の上昇率"), SerializeField] float proficiencyCostUpRate = 1.5f;
    [Header("IPコストの上昇率"), SerializeField] float ipCostUpRate = 1.7f;
    
    
    static readonly int[] ProficiencyBase = { 50, 75, 100, 150 };   // 熟練度のコスト
    static readonly int[] IPBaseCost = { 50, 75, 100, 125 };        // 知見ポイントのコスト
    public Category Category => category;
    public Sprite Icon => sprite;
    public string Name => name;
    public string Effect => effect;
    public Rarity Rarity => rarity;
    public int Level => level;
    public float CoolTime => coolTime;
    public bool IsEndCoolTime { get { return isEndCoolTime; } set { isEndCoolTime = value; } }
    public bool IsAuto { get { return isAuto; } set { isAuto = value; } }
    public float CurrentProficiency => currentProficiency;
    public float MaxProficiency => maxProficiency;
    public SkillLogicBase SkillLogicBase => skillLogicBase;

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Initialize()
    {
        maxProficiency = ProficiencyFetchCost();        // 熟練度の最大値を取得
    }

    /// <summary>
    /// 知見ポイントをレアリティごとに決まった値を返す
    /// </summary>
    public int InsightPointFetchCost()
    {
        if(level == 0)
        {
            return IPBaseCost[(int)rarity];
        }
        // レアリティごとに決まった値を返す
        // 0:N, 1:R, 2:SR, 3:SSR
        int cost = Mathf.FloorToInt(IPBaseCost[(int)rarity] * (ipCostUpRate * level));
        return cost;
    }


    /// <summary>
    /// 熟練度をレアリティごとに決まった値を返す
    /// </summary>
    public int ProficiencyFetchCost()
    {
        if(level == 0)
        {
            return IPBaseCost[(int)rarity];
        }
        // レアリティごとに決まった値を返す
        // 0:N, 1:R, 2:SR, 3:SSR
        int cost = Mathf.FloorToInt(ProficiencyBase[(int)rarity] * (proficiencyCostUpRate * level));
        return cost;
    }

    /// <summary>
    /// 熟練度を上げる(スキルのレベルアップ)
    /// </summary>
    public void ProficiencyLevelUp()
    {
        
        level++;
        currentProficiency = 0;
        maxProficiency = ProficiencyFetchCost();

        // スキルのレベルが上がった時の処理
    }

    /// <summary>
    /// 熟練度のコストをチェックする
    /// </summary>
    public bool CheckProficiencyCost()
    {
        return currentProficiency >= maxProficiency;
    }

    /// <summary>
    /// スキルのクールタイムをチェックする
    /// </summary>
    public void AddProficiency()
    {
        currentProficiency ++;      // 熟練度を上げる
        // 熟練度が最大値を超えた場合
        if (currentProficiency > maxProficiency)
        {
            // 熟練度を最大値にする
            currentProficiency = maxProficiency;
        }
    }
}

// カテゴリ
public enum Category
{
    Attack,
    Auxiliary,
    Disruption,
}
