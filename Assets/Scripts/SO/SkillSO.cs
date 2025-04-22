using UnityEngine;

[CreateAssetMenu(fileName = "SkillSO", menuName = "SkillSO")]
public class SkillSO : ScriptableObject
{
    [Header("カテゴリ"), SerializeField] Category category;
    [Header("画像"), SerializeField] Sprite sprite;
    [Header("名前"), SerializeField] new string name;
    [Header("効果"), SerializeField] string effect;
    [Header("レアリティ"), SerializeField] Rarity rarity;
    [Header("レベル"), SerializeField] int level = 1;
    [Header("クールタイム(CT)"), SerializeField] float coolTime;
    [Header("熟練度"), SerializeField] float proficiency;

    public Sprite Icon => sprite;
    public string Name => name;
    public string Effect => effect;
    public Rarity Rarity => rarity;
    public int Level => level;
    public float CoolTime => coolTime;
    public float Proficiency => proficiency;
}

// カテゴリ
public enum Category
{
    Attack,
    Auxiliary,
    Disruption,
}
