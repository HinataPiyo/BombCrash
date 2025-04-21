using UnityEngine;


public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;
    [SerializeField] SkillSO[] skillSOs;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
}