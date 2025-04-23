using System.Collections.Generic;
using UnityEngine;


public class OtomoSkillManager : MonoBehaviour
{
    public static OtomoSkillManager Instance;

    [SerializeField] SkillSO[] SkillSO_Table;
    [SerializeField] List<SkillSO> equippedSkill = new List<SkillSO>();
    public List<SkillSO> EquippedSkill { get { return equippedSkill; } }
    public SkillSO[] SkillSoTabel => SkillSO_Table;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }




    void Update()
    {
        
    }

    

}