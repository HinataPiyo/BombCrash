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
}

// カテゴリ
public enum Category
{
    Attack,
    Auxiliary,
    Disruption,
}
/*
public class Level : MonoBehaviour
{
    public SkillSO skillSO;
    public int currentProficiency = 1;
    public int requiredproficiency = 10;
    public float proficiencymultiplier = 1.2f;
    public int maxProficiency = 30;

    public void LevlUp()
    {
        if(requiredproficiency >= maxProficiency)
        {
            float _maxProficiency = (float)maxProficiency * proficiencymultiplier;
            _maxProficiency = maxProficiency;

            currentProficiency++;
            requiredproficiency = 0;
        }
    }
}
*/
