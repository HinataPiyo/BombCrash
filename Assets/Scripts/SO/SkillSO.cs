using UnityEngine;

[CreateAssetMenu(fileName = "SkillSO", menuName = "SkillSO")]
public class SkillSO : ScriptableObject
{
    [Header("カテゴリ"), SerializeField] Category category;
    [Header("名前"), SerializeField] new string name;
    [Header("効果"), SerializeField] string effect;
    [Header("レアリティ"), SerializeField] Rarity rarity;
    [Header("レベル"), SerializeField] int level;
    [Header("クールタイム(CT)"), SerializeField] float coolTime;
    [Header("熟練度"), SerializeField] float proficiency;
}

public enum Category
{
    Attack,
    Auxiliary,
    Disruption,
}
