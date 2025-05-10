using UnityEditor.PackageManager.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillSO", menuName = "SkillSO")]
public class SkillSO : ScriptableObject
{
    [Header("スキル処理"), SerializeField] SkillLogicBase skillLogicBase;
    [Header("カテゴリ"), SerializeField] Category category;
    [Header("画像"), SerializeField] Sprite sprite;
    [Header("名前"), SerializeField] new string name;
    [Header("効果"), SerializeField] string effect;
    [Header("レアリティ"), SerializeField] Rarity rarity;
    [Header("レベル"), SerializeField] int level = 1;
    [Header("クールタイム(CT)"), SerializeField] float coolTime;
    [Header("現在の熟練度"), SerializeField] float currentProficiency;
    [Header("次の最大熟練度"), SerializeField] float maxProficiency;
    
    static readonly int[] IPBaseCost = { 50, 75, 100, 125 };        // 知見ポイントのコスト
    public Category Category => category;
    public Sprite Icon => sprite;
    public string Name => name;
    public string Effect => effect;
    public Rarity Rarity => rarity;
    public int Level => level;
    public float CoolTime => coolTime;
    public float CurrentProficiency => currentProficiency;
    public float MaxProficiency => maxProficiency;
    public SkillLogicBase SkillLogicBase => skillLogicBase;

    /// <summary>
    /// 知見ポイントをレアリティごとに決まった値を返す
    /// </summary>
    public int InsightPointFetchCost()
    {
        // レアリティごとに決まった値を返す
        // 0:N, 1:R, 2:SR, 3:SSR
        return IPBaseCost[(int)rarity];
    }
}

// カテゴリ
public enum Category
{
    Attack,
    Auxiliary,
    Disruption,
}
