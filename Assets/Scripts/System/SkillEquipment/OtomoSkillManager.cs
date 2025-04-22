using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class OtomoSkillManager : MonoBehaviour
{
    public static OtomoSkillManager Instance;

    [SerializeField] List<SkillSO> equippedSkill = new List<SkillSO>();

    public List<SkillSO> EquippedSkill { get { return equippedSkill; } }

    [SerializeField] bool test;

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
        if (test == true)
        {
            test = false;
        }
    }

    

}