using UnityEngine;

[CreateAssetMenu(fileName = "SkillDatabase", menuName = "Skill/SkillDatabase")]
public class SkillDatabase : ScriptableObject
{
    [SerializeField] SkillSO[] skillDatabase;
    public SkillSO[] SkillDB => skillDatabase;
}